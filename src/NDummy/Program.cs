using NDummy.Commands;

var builder = ConsoleApp.CreateBuilder(args);
var consoleApp = builder.Build();

consoleApp.AddCommands<AppCommand>();
await consoleApp.RunAsync().ConfigureAwait(false);
