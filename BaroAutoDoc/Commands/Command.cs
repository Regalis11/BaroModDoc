using System.Collections.Immutable;
using System.Globalization;

namespace BaroAutoDoc.Commands;

abstract class Command
{
    public static ImmutableHashSet<Type> CommandTypes
        = typeof(Command).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Command)))
            .ToImmutableHashSet();

    public void Invoke(string[] args)
    {
        var type = GetType();
        var method = type.GetMethods()
            .Where(m => m.Name == nameof(Invoke))
            .FirstOrDefault(m => m.GetParameters() is var @params
                                 && (@params.Length != 1 || @params[0].ParameterType != typeof(string[])));
        if (method is null) { throw new Exception($"{type.Name} does not have an invoke method"); }

        var methodParameters = method.GetParameters();
        int requiredParamCount = methodParameters.Count(p => !p.HasDefaultValue);
        if (args.Length < requiredParamCount) { throw new Exception($"{type.Name} requires at least {requiredParamCount} parameters, got {args.Length} instead"); }
        if (args.Length > methodParameters.Length) { throw new Exception($"{type.Name} accepts at most {methodParameters.Length} parameters, got {args.Length} instead"); }

        object?[] parameterValues = Enumerable.Range(0, methodParameters.Length)
            .Select(i => i < args.Length
                ? Convert.ChangeType(args[i], methodParameters[i].ParameterType, CultureInfo.InvariantCulture)
                : methodParameters[i].DefaultValue)
            .ToArray();
        method.Invoke(this, parameterValues);
    }
}
