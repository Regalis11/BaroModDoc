using System.Collections.Immutable;

namespace BaroAutoDoc;

public class Page
{
    public abstract record BodyComponent
    {
        public static readonly BodyComponent Blank = new InlineMarkdown();
        
        public abstract string ToMarkdown();
    }
    
    public record InlineMarkdown(string Value = "") : BodyComponent
    {
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

    public record NewLine : BodyComponent
    {
        public override string ToMarkdown() => "\n";
    }
    
    public record CodeBlock(string Lang = "", string Value = "") : BodyComponent
    {
        public override string ToMarkdown()
            => $"```{Lang}\n{Value}\n```";
    }

    public record Hyperlink(string Url = "", string Text = "") : BodyComponent
    {
        public override string ToMarkdown()
            => $"[{Text}]({Url})";
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
