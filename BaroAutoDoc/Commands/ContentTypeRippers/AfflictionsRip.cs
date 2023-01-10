using System.Collections.Immutable;
using BaroAutoDoc.SyntaxWalkers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc.Commands.ContentTypeSpecific;

class AfflictionsRip : Command
{
    public readonly record struct DeclaredField(string Name, string Type);

    public readonly record struct XMLAssignedField(DeclaredField Field, string XMLIdentifier);

    public void Invoke()
    {
        Directory.SetCurrentDirectory(GlobalConfig.RepoPath);
        const string srcPathFmt = "Barotrauma/Barotrauma{0}/{0}Source/Characters/Health/Afflictions/";
        string[] srcPathParams = { "Shared", "Client" };

        var contentTypeFinder = new AfflictionRipper();
        foreach (string p in srcPathParams)
        {
            string srcPath = string.Format(srcPathFmt, p);
            contentTypeFinder.VisitAllInDirectory(srcPath);
        }

        // TODO we probably want to reuse this for other content types
        foreach (var (key, cls) in contentTypeFinder.AfflictionPrefabs)
        {
            ImmutableArray<DeclaredField> fields = cls.Members
                                                      .OfType<FieldDeclarationSyntax>()
                                                      .SelectMany(ConvertField)
                                                      .ToImmutableArray();

            static IEnumerable<DeclaredField> ConvertField(FieldDeclarationSyntax field) =>
                from variable in field.Declaration.Variables
                let name = variable.Identifier.Text
                let type = field.Declaration.Type.ToString()
                select new DeclaredField(name, type);

            var constructorStatements = cls.Members.OfType<ConstructorDeclarationSyntax>()
                                           .Select(static ctor => ctor.Body)
                                           .OfType<BlockSyntax>() // filter nulls
                                           .SelectMany(static ctorBody => ctorBody.Statements)
                                           .OfType<ExpressionStatementSyntax>();

            List<XMLAssignedField> xmlAssignedFields = new();

            foreach (ExpressionStatementSyntax statement in constructorStatements)
            {
                if (statement.Expression is not AssignmentExpressionSyntax
                    {
                        Right: InvocationExpressionSyntax
                        {
                            Expression: MemberAccessExpressionSyntax { Name: IdentifierNameSyntax rightIdentifier },
                            ArgumentList.Arguments: var assignmentArgumentList
                        },
                        Left: IdentifierNameSyntax leftIdentifier
                    }) { continue; }

                string assignmentMethodName = rightIdentifier.GetIdentifierString(),
                       assignedVariableName = leftIdentifier.GetIdentifierString();

                // probably a better way to do this
                if (!assignmentMethodName.StartsWith("GetAttribute", StringComparison.OrdinalIgnoreCase)) { continue; }

                foreach (DeclaredField field in fields)
                {
                    if (field.Name != assignedVariableName) { continue; }

                    string xmlIdentifier = assignmentArgumentList[0].ToString().EvaluateAsCSharpExpression();
                    xmlAssignedFields.Add(new XMLAssignedField(field, xmlIdentifier));
                    break;
                }
            }

            foreach (XMLAssignedField field in xmlAssignedFields)
            {
                Console.WriteLine($"{key} {field.Field.Name} {field.Field.Type} {field.XMLIdentifier}");
            }
        }
    }
}