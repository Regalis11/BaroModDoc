using System.Collections.Immutable;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace BaroAutoDoc;

class Page
{
    public abstract record BodyComponent
    {
        public static readonly BodyComponent Blank = new InlineMarkdown();
        
        public abstract string ToMarkdown();

        public InlineMarkdown PostProcess(Func<string, string> postProcFunc)
            => new InlineMarkdown(postProcFunc(ToMarkdown()));
    }
    
    public record InlineMarkdown(string Value = "") : BodyComponent
    {
        public InlineMarkdown(params BodyComponent[] subComponents)
            : this(string.Join("", subComponents.Select(c => c.ToMarkdown()))) { }

        public override string ToMarkdown()
            => Value;
    }

    public record RawText(string Value = "") : BodyComponent
    {
        public static readonly ImmutableHashSet<char> CharsToEscape
            = new[] { '*', '-', '[', ']', '(', ')', '`', '<', '>', '#' }.ToImmutableHashSet();

        public override string ToMarkdown()
            => CharsToEscape.Aggregate(Value.Replace("\\", "\\\\"), (s, c) => s.Replace($"{c}", $"\\{c}"));
    }

    public abstract record ContainerComponent : BodyComponent
    {
        protected abstract string tag { get; }
        public readonly List<BodyComponent> Children = new List<BodyComponent>();

        public sealed override string ToMarkdown()
            => $"<{tag}>{string.Join(" ", Children.Select(c => c.ToMarkdown()))}</{tag}>";
    }
    
    public record Superscript : ContainerComponent
    {
        protected override string tag => "sup";
    }
    
    public record Subscript : ContainerComponent
    {
        protected override string tag => "sub";
    }


    public record Table : BodyComponent
    {
        public sealed record Row(params string?[] Values)
        {
            public ImmutableArray<int> Lengths => Values.Select(static v => v?.Length ?? 0).ToImmutableArray();

            public string ToMarkdown(IReadOnlyList<int> lengths)
                => $"| {string.Join(" | ", Values.Select((s, i) => (s ?? "").PadRight(lengths[i]).Replace("\r", "").Replace("\n", "<br/>")))} |";
        }

        public Row? HeadRow;
        public readonly List<Row> BodyRows = new();

        private IEnumerable<Row> AllRows => (HeadRow is null ? Array.Empty<Row>() : new[] { HeadRow }).Concat(BodyRows);

        public override string ToMarkdown()
        {
            List<string> lines = new List<string>();

            var rowLengths = GetLengths(AllRows.ToImmutableArray());

            if (HeadRow != null)
            {
                lines.Add(HeadRow.ToMarkdown(rowLengths));
                lines.Add($"|-{string.Join("-|-", HeadRow.Values.Select((_, i) => new string('-', rowLengths[i])))}-|");
            }

            foreach (var row in BodyRows)
            {
                lines.Add(row.ToMarkdown(rowLengths));
            }

            return $"\n{string.Join("\n", lines)}\n";

            static ImmutableArray<int> GetLengths(IReadOnlyCollection<Row> rows)
            {
                var builder = ImmutableArray.CreateBuilder<int>();
                for (int i = 0; i < rows.Max(static r => r.Values.Length); i++)
                {
                    builder.Add(rows.Select(row => row.Lengths[i]).Max());
                }

                return builder.ToImmutable();
            }
        }
    }

    public record NewLine : BodyComponent
    {
        public override string ToMarkdown() => "\n";
    }
    
    public record CodeBlock(string Lang = "", string Value = "") : BodyComponent
    {
        public override string ToMarkdown()
            => $"```{Lang}\n{Value}\n```";

        public static CodeBlock FromXElement(XElement element)
        {
            var xmlWriterSettings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true,
                //NewLineOnAttributes = true
            };

            var xmlStringBuilder = new StringBuilder();
            using (var xmlWriter = XmlWriter.Create(xmlStringBuilder, xmlWriterSettings))
            {
                element.Save(xmlWriter);
            }
            return new CodeBlock("xml", xmlStringBuilder.ToString());
        }
    }

    public record Hyperlink(string Url = "", string Text = "") : BodyComponent
    {
        public override string ToMarkdown()
            => $"[{Text}]({Url})";
    }
    
    public record Image(string Url = "", string Caption = "") : BodyComponent
    {
        public override string ToMarkdown()
            => $"![{Caption}]({Url})";
    }
    
    public record BulletList : BodyComponent
    {
        public readonly List<BodyComponent> Items = new List<BodyComponent>();

        private string ItemToMarkdown(BodyComponent component)
            => component switch
            {
                BulletList nestedList => string.Join('\n', nestedList.ToMarkdown().Split('\n').Select(i => $"  {i}")),
                _ => $"- {component.ToMarkdown()}"
            };

        public override string ToMarkdown()
            => string.Join('\n', Items.Select(ItemToMarkdown));
    }

    public class SectionBody
    {
        public readonly List<BodyComponent> Components = new List<BodyComponent>();

        public void AddNewLine()
            => Components.Add(new NewLine());

        public string ToMarkdown()
            => string.Join("", Components.Select(c => c.ToMarkdown()));
    }
    
    private readonly Section topSection = new Section();

    public string Title
    {
        get => topSection.Title;
        set => topSection.Title = value;
    }
    public SectionBody Body => topSection.Body;
    public List<Section> Subsections => topSection.Subsections;

    public string ToMarkdown()
        => topSection.ToMarkdown();

    public class Section
    {
        public string Title = "";
        public readonly SectionBody Body = new SectionBody();
        public readonly List<Section> Subsections = new List<Section>();

        public string ToMarkdown()
            => $"# {Title}\n{Body.ToMarkdown()}\n\n"
               + string.Join("\n", Subsections.SelectMany(s => s.ToMarkdown().Split("\n"))
                   .Select(l => l.StartsWith("#") ? $"#{l}" : l));
    }
}
