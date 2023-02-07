namespace BaroAutoDoc;

static class DefaultValue
{
    public static string MakeMorePresentable(string valueStr, string type)
    {
        return type.Equals("Color", StringComparison.OrdinalIgnoreCase)
            ? $"<div style=\"background: {IntColorToCss(valueStr)};\">{valueStr.Replace("\"", "")}</div>"
            : valueStr;
    }

    public static string IntColorToCss(string valueStr)
    {
        var split = valueStr.Replace("\"", "").Split(",");
        if (split.Length != 4) { return valueStr; }
        if (!int.TryParse(split[3], out var intAlpha)) { return valueStr; }
        return $"rgba({split[0]},{split[1]},{split[2]},{(intAlpha / 255f)})";
    }
}
