using System.Collections.Immutable;

namespace BaroAutoDoc;

public class Page
{
    public abstract class BodyComponent
    {
        public abstract string ToMarkdown();
    }
    
    public class InlineMarkdown : BodyComponent
    {
        public string Value = "";
        
        public override string ToMarkdown()
            => Value;
    }

    public class RawText : BodyComponent
    {
        public static readonly ImmutableHashSet<char> CharsToEscape
            = new[] { '*', '-', '[', ']', '(', ')', '`', '<', '>' }.ToImmutableHashSet();
        public string Value = "";
        
        public override string ToMarkdown()
            => CharsToEscape.Aggregate(Value.Replace("\\", "\\\\"), (s, c) => s.Replace($"{c}", $"\\{c}"));
    }
    
    public class CodeBlock : BodyComponent
    {
        public string Lang = "";
        public string Value = "";
        public override string ToMarkdown()
            => $"```{Lang}\n{Value}\n```";
    }

    public class Hyperlink : BodyComponent
    {
        public string Url = "";
        public string Text = "";

        public override string ToMarkdown()
            => $"[{Text}]({Url})";
    }
    
    public class BulletList : BodyComponent
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

        public string ToMarkdown()
            => string.Join("", Components.Select(c => c.ToMarkdown()));
    }
    
    public readonly Section TopSection = new Section();

    public string Title
    {
        get => TopSection.Title;
        set => TopSection.Title = value;
    }
    public SectionBody Body => TopSection.Body;

    public class Section
    {
        public string Title = "";
        public readonly SectionBody Body = new SectionBody();
        public readonly List<Section> Subsections = new List<Section>();
    }
}
