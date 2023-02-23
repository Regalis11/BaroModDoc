#nullable enable

using System.Collections;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc.SyntaxWalkers;

internal readonly record struct TypeCollection(ImmutableArray<TypeDeclarationSyntax> Types, ImmutableArray<EnumDeclarationSyntax> Enums)
{
    public ImmutableArray<BaseTypeDeclarationSyntax> All => Types.Cast<BaseTypeDeclarationSyntax>().Concat(Enums).ToImmutableArray();
}

internal readonly record struct TypeAndFile<T>(string File, T Type);

internal sealed class FileMap : IEnumerable<string>
{
    public readonly string File;
    public readonly List<string> Types = new();

    public FileMap(string file)
    {
        File = file;
    }

    public void Add(string type) { Types.Add(type); }

    public IEnumerator<string> GetEnumerator() => Types.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}


internal sealed class SyntaxRipperBuilder
{
    internal sealed class AddedFile
    {
        private readonly List<TypeAndFile<TypeDeclarationSyntax>> types = new();
        private readonly List<TypeAndFile<EnumDeclarationSyntax>> enums = new();
        private readonly List<FileMap> mappings = new();

        private readonly SyntaxRipperBuilder builder;

        private readonly string key;

        private string currentDirectory = string.Empty;

        public AddedFile(SyntaxRipperBuilder builder, string key)
        {
            this.builder = builder;
            this.key = key;
        }

        public AddedFile AddFile(string filePath, string[]? fmt = null)
        {
            filePath = Path.Combine(currentDirectory, filePath);
            if (!File.Exists(filePath)) { return this; }

            if (fmt is not null)
            {
                foreach (string s in fmt)
                {
                    string filePathFmt = string.Format(filePath, s);
                    AddFilePath(filePathFmt);
                }
            }
            else
            {
                AddFilePath(filePath);
            }

            return this;

            void AddFilePath(string file)
            {
                SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(file));
                foreach (TypeDeclarationSyntax t in syntaxTree.NodesOfType<TypeDeclarationSyntax>())
                {
                    types.Add(new TypeAndFile<TypeDeclarationSyntax>(file, t));
                }

                foreach (EnumDeclarationSyntax e in syntaxTree.NodesOfType<EnumDeclarationSyntax>())
                {
                    enums.Add(new TypeAndFile<EnumDeclarationSyntax>(file, e));
                }
            }
        }

        public AddedFile SetDirectory(string dirPath)
        {
            currentDirectory = dirPath;
            return this;
        }

        public AddedFile AddFiles(params string[] names)
        {
            foreach (string s in names)
            {
                AddFile(Path.Combine(currentDirectory, s));
            }

            return this;
        }

        public AddedFile AddDirectory(string dirPath, string fileNames, string[]? fmt = null)
        {
            if (fmt is not null)
            {
                foreach (string s in fmt)
                {
                    string dirPathFmt = string.Format(dirPath, s);
                    AddDirPath(dirPathFmt);
                }
            }
            else
            {
                AddDirPath(dirPath);
            }

            return this;

            void AddDirPath(string dir)
            {
                if (!Directory.Exists(dir)) { return; }

                foreach (string f in Directory.GetFiles(dir, fileNames, SearchOption.AllDirectories))
                {
                    AddFile(f);
                }
            }
        }

        public AddedFile WhereNameIs(params string[] names)
        {
            types.RemoveAll(t => !names.All(n => t.Type.Identifier.MatchesIgnoreCaseWithWildcards(n)));
            enums.RemoveAll(t => !names.All(n => t.Type.Identifier.MatchesIgnoreCaseWithWildcards(n)));
            return this;
        }

        public AddedFile ExcludeNames(params string[] names)
        {
            types.RemoveAll(t => names.Any(n => t.Type.Identifier.MatchesIgnoreCaseWithWildcards(n)));
            enums.RemoveAll(e => names.Any(n => e.Type.Identifier.MatchesIgnoreCaseWithWildcards(n)));
            return this;
        }

        public AddedFile WhereFileNameIs(params string[] names)
        {
            types.RemoveAll(t => !names.All(n => Path.GetFileName(t.File).MatchesIgnoreCaseWithWildcards(n)));
            enums.RemoveAll(e => !names.All(n => Path.GetFileName(e.File).MatchesIgnoreCaseWithWildcards(n)));
            return this;
        }

        public AddedFile ExcludeFileNames(params string[] names)
        {
            types.RemoveAll(t => names.Any(n => Path.GetFileName(t.File).MatchesIgnoreCaseWithWildcards(n)));
            enums.RemoveAll(e => names.Any(n => Path.GetFileName(e.File).MatchesIgnoreCaseWithWildcards(n)));
            return this;
        }

        public AddedFile Map(params FileMap[] maps)
        {
            mappings.AddRange(maps);
            return this;
        }

        public SyntaxRipperBuilder Submit()
        {
            builder.Types[key] = new TypeCollection(
                types.Select(static file => file.Type).ToImmutableArray(),
                enums.Select(static file => file.Type).ToImmutableArray());

            foreach (FileMap map in mappings)
            {
                if (!builder.FilesToCreate.ContainsKey(map.File))
                {
                    builder.FilesToCreate.Add(map.File, new List<string>());
                }

                builder.FilesToCreate[map.File].AddRange(map.Types);
            }
            return builder;
        }
    }

    public readonly Dictionary<string, List<string>> FilesToCreate = new();

    public readonly Dictionary<string, TypeCollection> Types = new();

    public AddedFile Prepare(string key) => new AddedFile(this, key);

    public ImmutableDictionary<string, List<ParsedType>> Build()
    {
        // TODO this is not in order
        var builder = ImmutableDictionary.CreateBuilder<string, List<ParsedType>>();

        foreach (TypeCollection collection in Types.Values)
        {
            var typeDict = collection.Types
                                     .GroupBy(static t => t.Identifier.ValueText)
                                     .ToDictionary(static g => g.Key, static g => g.ToImmutableArray());

            foreach (var (identifier, types) in typeDict)
            {
                ParsedType? parser = null;
                foreach (TypeDeclarationSyntax type in types)
                {
                    parser ??= ParsedType.CreateParser(type, new ClassParsingOptions());
                    parser.ParseType(type);
                }

                if (parser is null) { continue; }

                foreach (var (file, identifiers) in FilesToCreate)
                {
                    if (!identifiers.Contains(identifier)) { continue; }

                    if (!builder.ContainsKey(file))
                    {
                        builder.Add(file, new List<ParsedType>());
                    }

                    builder[file].Add(parser);
                    break;
                }
            }

            // TODO add enums
        }

        return builder.ToImmutable();
    }
}