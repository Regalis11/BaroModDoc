using System.Collections.Immutable;

namespace BaroAutoDoc.Commands;

public abstract class Command
{
    public static ImmutableHashSet<Type> CommandTypes
        = typeof(Command).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Command)))
            .ToImmutableHashSet();

    public abstract void Invoke(string[] args);
}
