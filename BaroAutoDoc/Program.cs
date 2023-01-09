using System.Runtime.CompilerServices;
using BaroAutoDoc.Commands;

try
{
    // invoke the global config constructor to initialize the config
    RuntimeHelpers.RunClassConstructor(typeof(GlobalConfig).TypeHandle);
}
catch (TypeInitializationException exception)
{
    if (exception.InnerException is not { } e) { throw; }

    // print any errors that GlobalConfig constructor threw
    Console.WriteLine($"{e.GetType().Name}: {e.Message}");
    return;
}

if (!args.Any())
{
    Console.WriteLine("Available commands:");
    Console.WriteLine(string.Join(", ", Command.CommandTypes.Select(c => c.Name)));
    Console.WriteLine("");

    Console.WriteLine("Command to invoke:");
    string cmd = Console.ReadLine() ?? "";
    args = cmd.Split(' ');
}

string command = args[0];
string[] commandArgs = args[1..];

(Activator.CreateInstance(
        Command.CommandTypes.FirstOrDefault(c => c.Name.Equals(command, StringComparison.OrdinalIgnoreCase))
        ?? throw new Exception($"Command not found: {command}")) as Command)!
    .Invoke(commandArgs);
