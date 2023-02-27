#nullable enable

using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
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

internal readonly struct Either<T, U>
{
    private readonly T? first;
    private readonly U? second;

    private readonly bool hasT = false,
                          hasU = false;

    public Either(T type)
    {
        hasT = true;
        first = type;
        second = default;
    }

    public Either(U type)
    {
        hasU = true;
        first = default;
        second = type;
    }

    public bool TryGet([NotNullWhen(true)] out T? type)
    {
        if (!hasT)
        {
            type = default;
            return false;
        }
        type = first!;
        return true;
    }

    public bool TryGet([NotNullWhen(true)] out U? type)
    {
        if (!hasU)
        {
            type = default;
            return false;
        }
        type = second!;
        return true;
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

    public ImmutableDictionary<string, List<Either<ParsedType, ParsedEnum>>> Build()
    {
        // TODO this is not in order
        var typeBuilder = ImmutableDictionary.CreateBuilder<string, List<Either<ParsedType, ParsedEnum>>>();

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
                    typeBuilder[file].Add(new Either<ParsedType, ParsedEnum>(parser));
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
                typeBuilder[file].Add(new Either<ParsedType, ParsedEnum>(e));
                break;
            }
        }

        void AddIfNotExists(string file)
        {
            if (!typeBuilder.ContainsKey(file))
            {
                typeBuilder.Add(file, new List<Either<ParsedType, ParsedEnum>>());
            }
        }

        foreach (var (file, pair) in typeBuilder)
        {
            var orderList = FilesToCreate[file];
            pair.Sort(Comparison);

            int Comparison(Either<ParsedType, ParsedEnum> a, Either<ParsedType, ParsedEnum> b)
            {
                int aIndex = orderList.IndexOf(GetTypeName(a));
                int bIndex = orderList.IndexOf(GetTypeName(b));

                return aIndex.CompareTo(bIndex);

                static string GetTypeName(Either<ParsedType, ParsedEnum> either) =>
                    either.TryGet(out ParsedType? t) ? t.Name :
                    either.TryGet(out ParsedEnum e) ? e.Name : throw new Exception();
            }
        }

        return typeBuilder.ToImmutable();
    }
}