using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc.PluginDocs;

internal enum Availability
{
    Both,
    ClientOnly,
    ServerOnly
}

internal enum MemberKind
{
    Method,
    Property,
    Event,
    Constructor,
    Field,
    Indexer
}

internal sealed class MemberDoc
{
    public MemberKind Kind;
    public string Name = "";
    public string Signature = "";
    public MemberDeclarationSyntax Syntax = null!;
    public DocComment Doc = DocComment.Empty;
    public Availability Availability;
    public string Key => $"{Kind}:{Signature}";
}

internal sealed record EnumValueDoc(string Name, string? Value, DocComment Doc);

internal sealed class TypeDoc
{
    public string Name = "";
    public string Keyword = "";
    public string Declaration = "";
    public MemberDeclarationSyntax Syntax = null!;
    public DocComment Doc = DocComment.Empty;
    public Availability Availability;
    public bool IsPluginService;
    public List<MemberDoc> Members = new();
    public List<EnumValueDoc> EnumValues = new();
    public BaseParameterListSyntax? DelegateParameters;
}

/// <summary>
/// Extracts the public API of a source file as <see cref="TypeDoc"/>s. Code inside <c>#if CLIENT</c> and
/// <c>#if SERVER</c> is handled by parsing the file once for each and merging the results.
/// </summary>
internal static class SourceModel
{
    public static List<TypeDoc> ParseFile(string path)
    {
        string text = File.ReadAllText(path);
        List<TypeDoc> client = Collect(text, "CLIENT");
        List<TypeDoc> server = Collect(text, "SERVER");
        return Merge(client, server);
    }

    private static List<TypeDoc> Collect(string text, string symbol)
    {
        CSharpParseOptions options = new(LanguageVersion.Latest, DocumentationMode.Parse, SourceCodeKind.Regular, new[] { symbol });
        SyntaxNode root = CSharpSyntaxTree.ParseText(text, options).GetRoot();

        List<TypeDoc> types = new();
        foreach (MemberDeclarationSyntax declaration in root.DescendantNodes().OfType<MemberDeclarationSyntax>())
        {
            TypeDoc? type = declaration switch
            {
                TypeDeclarationSyntax t => FromType(t),
                EnumDeclarationSyntax e => FromEnum(e),
                DelegateDeclarationSyntax d => FromDelegate(d),
                _ => null
            };

            if (type is null || !IsPublicType(declaration)) { continue; }

            type.Availability = symbol == "CLIENT" ? Availability.ClientOnly : Availability.ServerOnly;
            foreach (MemberDoc member in type.Members) { member.Availability = type.Availability; }
            types.Add(type);
        }

        return types;
    }

    private static List<TypeDoc> Merge(List<TypeDoc> client, List<TypeDoc> server)
    {
        List<TypeDoc> result = new();
        foreach (TypeDoc c in client)
        {
            TypeDoc? s = server.FirstOrDefault(t => t.Name == c.Name);
            if (s is null)
            {
                result.Add(c);
                continue;
            }

            c.Availability = Availability.Both;
            foreach (MemberDoc member in c.Members)
            {
                member.Availability = s.Members.Any(m => m.Key == member.Key) ? Availability.Both : Availability.ClientOnly;
            }
            foreach (MemberDoc member in s.Members)
            {
                if (c.Members.Any(m => m.Key == member.Key)) { continue; }

                member.Availability = Availability.ServerOnly;
                c.Members.Add(member);
            }

            result.Add(c);
        }

        result.AddRange(server.Where(s => client.All(c => c.Name != s.Name)));
        return result;
    }

    private static bool IsPublicType(MemberDeclarationSyntax declaration)
    {
        if (declaration.Modifiers.Any(SyntaxKind.PublicKeyword)) { return true; }

        // Types nested in interfaces are public even without a modifier.
        return declaration.Parent is InterfaceDeclarationSyntax
               && !declaration.Modifiers.Any(m => m.IsKind(SyntaxKind.PrivateKeyword) || m.IsKind(SyntaxKind.InternalKeyword));
    }

    private static TypeDoc FromType(TypeDeclarationSyntax syntax)
    {
        string keyword = syntax.Keyword.Text;
        if (syntax is RecordDeclarationSyntax { ClassOrStructKeyword.Text: { Length: > 0 } classOrStruct })
        {
            keyword += " " + classOrStruct;
        }

        string declaration = string.Join(" ", new[]
        {
            "public",
            TypeModifiers(syntax.Modifiers),
            keyword,
            syntax.Identifier.Text + syntax.TypeParameterList + (syntax as RecordDeclarationSyntax)?.ParameterList,
            syntax.BaseList is null ? "" : syntax.BaseList.ToString(),
            string.Join(" ", syntax.ConstraintClauses.Select(static c => c.ToString()))
        }.Where(static s => s.Length > 0));

        TypeDoc type = new()
        {
            Name = syntax.Identifier.Text,
            Keyword = keyword,
            Declaration = Collapse(declaration),
            Syntax = syntax,
            Doc = DocComment.From(syntax),
            IsPluginService = syntax.BaseList?.Types.Any(static t => t.Type.ToString() == "IPluginService") ?? false
        };

        bool isInterface = syntax is InterfaceDeclarationSyntax;
        foreach (MemberDeclarationSyntax member in syntax.Members)
        {
            if (!IsPublicMember(member, isInterface)) { continue; }

            switch (member)
            {
                case MethodDeclarationSyntax { ExplicitInterfaceSpecifier: null } m:
                    type.Members.Add(new MemberDoc
                    {
                        Kind = MemberKind.Method,
                        Name = m.Identifier.Text,
                        Signature = Collapse($"{MemberModifiers(m.Modifiers)}{m.ReturnType} {m.Identifier}{m.TypeParameterList}{m.ParameterList}{Constraints(m.ConstraintClauses)}"),
                        Syntax = m,
                        Doc = DocComment.From(m)
                    });
                    break;
                case PropertyDeclarationSyntax p:
                    type.Members.Add(new MemberDoc
                    {
                        Kind = MemberKind.Property,
                        Name = p.Identifier.Text,
                        Signature = Collapse($"{MemberModifiers(p.Modifiers)}{p.Type} {p.Identifier} {Accessors(p.AccessorList, p.ExpressionBody)}"),
                        Syntax = p,
                        Doc = DocComment.From(p)
                    });
                    break;
                case IndexerDeclarationSyntax i:
                    type.Members.Add(new MemberDoc
                    {
                        Kind = MemberKind.Indexer,
                        Name = "this[]",
                        Signature = Collapse($"{MemberModifiers(i.Modifiers)}{i.Type} this{i.ParameterList} {Accessors(i.AccessorList, i.ExpressionBody)}"),
                        Syntax = i,
                        Doc = DocComment.From(i)
                    });
                    break;
                case ConstructorDeclarationSyntax c:
                    type.Members.Add(new MemberDoc
                    {
                        Kind = MemberKind.Constructor,
                        Name = c.Identifier.Text,
                        Signature = Collapse($"{c.Identifier}{c.ParameterList}"),
                        Syntax = c,
                        Doc = DocComment.From(c)
                    });
                    break;
                case EventFieldDeclarationSyntax e:
                    foreach (VariableDeclaratorSyntax variable in e.Declaration.Variables)
                    {
                        type.Members.Add(new MemberDoc
                        {
                            Kind = MemberKind.Event,
                            Name = variable.Identifier.Text,
                            Signature = Collapse($"{MemberModifiers(e.Modifiers)}event {e.Declaration.Type} {variable.Identifier}"),
                            Syntax = e,
                            Doc = DocComment.From(e)
                        });
                    }
                    break;
                case EventDeclarationSyntax e:
                    type.Members.Add(new MemberDoc
                    {
                        Kind = MemberKind.Event,
                        Name = e.Identifier.Text,
                        Signature = Collapse($"{MemberModifiers(e.Modifiers)}event {e.Type} {e.Identifier}"),
                        Syntax = e,
                        Doc = DocComment.From(e)
                    });
                    break;
                case FieldDeclarationSyntax f:
                    foreach (VariableDeclaratorSyntax variable in f.Declaration.Variables)
                    {
                        type.Members.Add(new MemberDoc
                        {
                            Kind = MemberKind.Field,
                            Name = variable.Identifier.Text,
                            Signature = Collapse($"{MemberModifiers(f.Modifiers)}{f.Declaration.Type} {variable.Identifier}"),
                            Syntax = f,
                            Doc = DocComment.From(f)
                        });
                    }
                    break;
            }
        }

        return type;
    }

    private static TypeDoc FromEnum(EnumDeclarationSyntax syntax)
    {
        TypeDoc type = new()
        {
            Name = syntax.Identifier.Text,
            Keyword = "enum",
            Declaration = Collapse($"public enum {syntax.Identifier}{(syntax.BaseList is null ? "" : " " + syntax.BaseList)}"),
            Syntax = syntax,
            Doc = DocComment.From(syntax)
        };

        foreach (EnumMemberDeclarationSyntax member in syntax.Members)
        {
            type.EnumValues.Add(new EnumValueDoc(member.Identifier.Text, member.EqualsValue?.Value.ToString(), DocComment.From(member)));
        }

        return type;
    }

    private static TypeDoc FromDelegate(DelegateDeclarationSyntax syntax)
        => new()
        {
            Name = syntax.Identifier.Text,
            Keyword = "delegate",
            Declaration = Collapse($"public delegate {syntax.ReturnType} {syntax.Identifier}{syntax.TypeParameterList}{syntax.ParameterList}{Constraints(syntax.ConstraintClauses)}"),
            Syntax = syntax,
            Doc = DocComment.From(syntax),
            DelegateParameters = syntax.ParameterList
        };

    private static bool IsPublicMember(MemberDeclarationSyntax member, bool inInterface)
    {
        if (member.Modifiers.Any(m => m.IsKind(SyntaxKind.PublicKeyword) || m.IsKind(SyntaxKind.ProtectedKeyword))) { return true; }

        return inInterface && !member.Modifiers.Any(m => m.IsKind(SyntaxKind.PrivateKeyword) || m.IsKind(SyntaxKind.InternalKeyword));
    }

    private static readonly SyntaxKind[] ShownModifiers =
    {
        SyntaxKind.StaticKeyword,
        SyntaxKind.AbstractKeyword,
        SyntaxKind.VirtualKeyword,
        SyntaxKind.OverrideKeyword,
        SyntaxKind.SealedKeyword,
        SyntaxKind.ReadOnlyKeyword,
        SyntaxKind.ConstKeyword
    };

    private static string MemberModifiers(SyntaxTokenList modifiers)
    {
        string text = string.Join(" ", modifiers.Where(m => ShownModifiers.Any(k => m.IsKind(k))).Select(static m => m.Text));
        return text.Length == 0 ? "" : text + " ";
    }

    private static string TypeModifiers(SyntaxTokenList modifiers)
        => string.Join(" ", modifiers.Where(m => ShownModifiers.Any(k => m.IsKind(k))).Select(static m => m.Text));

    private static string Constraints(SyntaxList<TypeParameterConstraintClauseSyntax> clauses)
        => clauses.Count == 0 ? "" : " " + string.Join(" ", clauses.Select(static c => c.ToString()));

    private static string Accessors(AccessorListSyntax? accessors, ArrowExpressionClauseSyntax? expressionBody)
    {
        if (accessors is null) { return expressionBody is null ? "" : "{ get; }"; }

        return "{ " + string.Join(" ", accessors.Accessors.Select(static a =>
            string.Join(" ", a.Modifiers.Select(static m => m.Text).Append(a.Keyword.Text)) + ";")) + " }";
    }

    private static string Collapse(string text)
        => Regex.Replace(text, @"\s+", " ").Trim();
}
