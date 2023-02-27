#nullable enable

using System.Collections;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc.SyntaxWalkers;

internal readonly record struct TypeCollection(ImmutableArray<TypeDeclarationSyntax> Types,
                                               ImmutableArray<EnumDeclarationSyntax> Enums,
                                               ClassParsingOptions Options)
{
    public ImmutableArray<BaseTypeDeclarationSyntax> All => Types.Cast<BaseTypeDeclarationSyntax>().Concat(Enums).ToImmutableArray();
}

internal sealed class ListPair<T, U>
{
    public readonly List<T> First = new();
    public readonly List<U> Second = new();

    public void Deconstruct(out List<T> first, out List<U> second)
    {
        first = First;
        second = Second;
    }
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

internal class GenericRipper : CSharpSyntaxWalker
{
    private readonly SyntaxTree syntaxTree;
    private readonly ImmutableArray<BaseTypeDeclarationSyntax>.Builder builder = ImmutableArray.CreateBuilder<BaseTypeDeclarationSyntax>();

    public GenericRipper(string file) => syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(file));

    public ImmutableArray<BaseTypeDeclarationSyntax> Run()
    {
        Visit(syntaxTree.GetRoot());
        return builder.ToImmutable();
    }

    public override void VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        builder.Add(node);
        base.VisitClassDeclaration(node);
    }

    public override void VisitStructDeclaration(StructDeclarationSyntax node)
    {
        builder.Add(node);
        base.VisitStructDeclaration(node);
    }

    public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
    {
        builder.Add(node);
        base.VisitEnumDeclaration(node);
    }

}

internal sealed class SyntaxRipperBuilder
{
    internal sealed class AddedFile
    {
        private readonly List<TypeAndFile<TypeDeclarationSyntax>> types = new();
        private readonly List<TypeAndFile<EnumDeclarationSyntax>> enums = new();
        private readonly List<FileMap> mappings = new();
        private ClassParsingOptions parsingOptions = new();

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
                var parsedTypes = new GenericRipper(file).Run();
                foreach (TypeDeclarationSyntax t in parsedTypes.OfType<TypeDeclarationSyntax>())
                {
                    types.Add(new TypeAndFile<TypeDeclarationSyntax>(file, t));
                }

                foreach (EnumDeclarationSyntax e in parsedTypes.OfType<EnumDeclarationSyntax>())
                {
                    enums.Add(new TypeAndFile<EnumDeclarationSyntax>(file, e));
                }
            }
        }

        public AddedFile WithOptions(ClassParsingOptions options)
        {
            parsingOptions = options;
            return this;
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
                Types: types.Select(static file => file.Type).ToImmutableArray(),
                Enums: enums.Select(static file => file.Type).ToImmutableArray(),
                Options: parsingOptions);

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

    public ImmutableDictionary<string, ListPair<ParsedType, ParsedEnum>> Build()
    {
        // TODO this is not in order
        var typeBuilder = ImmutableDictionary.CreateBuilder<string, ListPair<ParsedType, ParsedEnum>>();

        foreach (TypeCollection collection in Types.Values)
        {
            var typeDict = collection.Types
                                     .GroupBy(static t => t.Identifier.ValueText)
                                     .ToDictionary(static g => g.Key, static g => g.ToImmutableArray());

            HashSet<string> alreadyDeclaredEnums = new();

            foreach (EnumDeclarationSyntax e in collection.Enums)
            {
                ParsedEnum parsedEnum = ParsedType.ParseEnum(e);
                alreadyDeclaredEnums.Add(parsedEnum.Name);
                AddEnum(parsedEnum);
            }

            foreach (var (identifier, types) in typeDict)
            {
                ParsedType? parser = null;
                foreach (TypeDeclarationSyntax type in types)
                {
                    parser ??= ParsedType.CreateParser(type, collection.Options);
                    parser.ParseType(type);
                }

                if (parser is null) { continue; }

                foreach (ParsedEnum extraEnum in parser.Enums)
                {
                    if (alreadyDeclaredEnums.Contains(extraEnum.Name)) { continue; }
                    AddEnum(extraEnum);
                }

                foreach (var (file, identifiers) in FilesToCreate)
                {
                    if (!identifiers.Contains(identifier)) { continue; }
                    AddIfNotExists(file);
                    typeBuilder[file].First.Add(parser);
                    break;
                }
            }
        }

        void AddEnum(ParsedEnum e)
        {
            foreach (var (file, identifiers) in FilesToCreate)
            {
                if (!identifiers.Contains(e.Name)) { continue; }
                AddIfNotExists(file);
                typeBuilder[file].Second.Add(e);
                break;
            }
        }

        void AddIfNotExists(string file)
        {
            if (!typeBuilder.ContainsKey(file))
            {
                typeBuilder.Add(file, new ListPair<ParsedType, ParsedEnum>());
            }
        }

        // sort typeBuilder based on the order they are in FilesToCreate
        foreach (var (file, pair) in typeBuilder)
        {
            var orderList = FilesToCreate[file];
            pair.First.Sort((a, b) => orderList.IndexOf(a.Name).CompareTo(orderList.IndexOf(b.Name)));
            // TODO enums are not sorted
            pair.Second.Sort((a, b) => orderList.IndexOf(a.Name).CompareTo(orderList.IndexOf(b.Name)));
        }

        return typeBuilder.ToImmutable();
    }
}