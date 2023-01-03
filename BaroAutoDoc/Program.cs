using BaroAutoDoc.Commands;

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
