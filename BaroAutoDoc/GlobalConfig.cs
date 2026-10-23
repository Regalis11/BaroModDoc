using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
namespace BaroAutoDoc;

[AttributeUsage(AttributeTargets.Field)]
public sealed class ConfigValue : Attribute { }

[SuppressMessage("ReSharper", "ConvertToConstant.Global")]
internal static class GlobalConfig
{
    [ConfigValue]
    public static readonly string RepoPath = "";

    private static readonly ImmutableDictionary<Type, Func<string, object>> ConfigValueParsers = new Dictionary<Type, Func<string, object>>
    {
        [typeof(string)] = static s => s,
        [typeof(int)] = static s => int.Parse(s),
        [typeof(float)] = static s => float.Parse(s),
        [typeof(bool)] = static s => bool.Parse(s)
    }.ToImmutableDictionary();

    private const string ConfigFileName = "config.xml";

    static GlobalConfig()
    {
        string configPath = FindConfigFile()
            ?? throw new FileNotFoundException(
                $"Could not find {ConfigFileName}. Copy {ConfigFileName}.example to {ConfigFileName} in the root folder of the repository and edit it.");

        XElement config = XElement.Load(configPath);

        foreach (FieldInfo field in GetConfigFields())
        {
            if (config.GetAttributeValue(field.Name.ToLower()) is not { } value)
            {
                throw new XmlException($"Config file is missing the value for \"{field.Name}\".");
            }

            object parsedValue;
            try
            {
                parsedValue = ConfigValueParsers[field.FieldType](value);
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

        if (string.IsNullOrWhiteSpace(RepoPath))
        {
            throw new InvalidOperationException($"\"repoPath\" is empty in {configPath}, set it to the path of your local Barotrauma repository.");
        }
    }

    /// <summary>
    /// Looks for the config file in the working directory and the executable's directory, and in their parent directories.
    /// </summary>
    private static string? FindConfigFile()
    {
        foreach (string start in new[] { Directory.GetCurrentDirectory(), AppContext.BaseDirectory })
        {
            for (DirectoryInfo? dir = new(start); dir is not null; dir = dir.Parent)
            {
                string path = Path.Combine(dir.FullName, ConfigFileName);
                if (File.Exists(path)) { return path; }
            }
        }

        return null;
    }

    private static ImmutableArray<FieldInfo> GetConfigFields() =>
        typeof(GlobalConfig)
            .GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(static field => field.GetCustomAttribute<ConfigValue>() is not null)
            .ToImmutableArray();
}