using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using BaroAutoDoc;

public sealed class ConfigValue : Attribute { }

[SuppressMessage("ReSharper", "ConvertToConstant.Global")]
internal static class GlobalConfig
{
    [ConfigValue]
    public static readonly string RepoPath = Constants.DefaultRepoPath;

    private readonly record struct ConfigValueParser(Func<string, object> Parse);

    private static readonly ImmutableDictionary<Type, ConfigValueParser> ConfigValueParsers = new Dictionary<Type, ConfigValueParser>
    {
        [typeof(string)] = new ConfigValueParser(static s => s),
        [typeof(int)] = new ConfigValueParser(static s => int.Parse(s)),
        [typeof(float)] = new ConfigValueParser(static s => float.Parse(s)),
        [typeof(bool)] = new ConfigValueParser(static s => bool.Parse(s))
    }.ToImmutableDictionary();

    private readonly static string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.xml");

    static GlobalConfig()
    {
        if (!File.Exists(ConfigPath))
        {
            CreateConfigFile(ConfigPath);
            throw new FileNotFoundException("No config file found, generating a new one. Fill in the required information.", ConfigPath);
        }

        XElement config = XElement.Load(ConfigPath);

        foreach (FieldInfo field in GetConfigFields())
        {
            if (config.GetAttributeValue(field.Name.ToLower()) is not { } value)
            {
                throw new XmlException($"Config file is missing the value for \"{field.Name}\".");
            }

            object parsedValue;
            try
            {
                parsedValue = ConfigValueParsers[field.FieldType].Parse(value);
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException($"Config value \"{field.Name}\" has an unsupported type \"{field.FieldType}\".");
            }
            catch (Exception)
            {
                throw new InvalidOperationException($"Config value \"{field.Name}\" has an invalid value \"{value}\".");
            }

            field.SetValue(null, parsedValue);
        }
    }

    private static void CreateConfigFile(string filePath)
    {
        XElement root = new XElement("Config");

        foreach (FieldInfo field in GetConfigFields())
        {
            object? defaultValue = field.GetValue(null);
            if (defaultValue is null)
            {
                throw new InvalidOperationException($"Config file option \"{field.Name}\" has no default value.");
            }

            root.Add(new XAttribute(field.Name.ToLower(), defaultValue));
        }

        XmlWriterSettings settings = new XmlWriterSettings
        {
            Indent = true,
            OmitXmlDeclaration = true,
            NewLineOnAttributes = true
        };

        using XmlWriter writer = XmlWriter.Create(filePath, settings);

        root.WriteTo(writer);
        writer.Flush();
    }

    private static ImmutableArray<FieldInfo> GetConfigFields() =>
        typeof(GlobalConfig)
            .GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(static field => field.GetCustomAttribute<ConfigValue>() is not null)
            .ToImmutableArray();
}