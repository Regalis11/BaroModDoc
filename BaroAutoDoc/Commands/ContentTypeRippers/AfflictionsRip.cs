using System.Collections.Immutable;
using System.Text.RegularExpressions;
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
        foreach (var (key, cls) in contentTypeFinder.AfflictionPrefabs.Union(contentTypeFinder.AfflictionTypes))
        {

            Console.WriteLine($"{key}:");
            ImmutableArray<DeclaredField> fields = cls.Members
                                                      .OfType<FieldDeclarationSyntax>()
                                                      .SelectMany(ConvertField)
                                                      .ToImmutableArray();

            static IEnumerable<DeclaredField> ConvertField(FieldDeclarationSyntax field) =>
                from variable in field.Declaration.Variables
                let name = variable.Identifier.Text
                let type = field.Declaration.Type.ToString()
                select new DeclaredField(name, type);


            var ctorBodies = cls.FindInitializerMethodBodies("LoadEffects");

            List<XMLAssignedField> xmlAssignedFields = new();

            foreach (StatementSyntax statement in ctorBodies.SelectMany(static ctorBody => ctorBody.Statements))
            {
                // FIXME this is genuinely unreadable (not like this form of code is readable anyway) but I can do better
                if (statement is not ExpressionStatementSyntax
                    {
                        Expression: AssignmentExpressionSyntax
                        {
                            Right: InvocationExpressionSyntax
                            {
                                Expression: MemberAccessExpressionSyntax { Name: IdentifierNameSyntax rightIdentifier },
                                ArgumentList.Arguments: var assignmentArgumentList
                            },
                            Left: IdentifierNameSyntax leftIdentifier
                        }
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

            foreach (var ctorBody in ctorBodies)
            {
                foreach (SupportedSubElement affectedElement in  SubElementFinder.FindSubElementsFrom(ctorBody))
                {
                    // TODO we need to consider that a lot of lists are created in the constructor and then assigned into the global variable
                    // one such case is the descriptions in affliction
                    if (affectedElement.AffectedField.Length is 0 || !fields.Any(f => affectedElement.AffectedField.Contains(f.Name))) { continue; }
                    Console.WriteLine($"    {affectedElement.XMLName} {string.Join(',', affectedElement.AffectedField)}");
                }
            }
        }
    }
}