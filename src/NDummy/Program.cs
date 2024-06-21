using ConsoleAppFramework;
using NDummy.Commands;

var app = ConsoleApp.Create();

app.Add<AppCommand>();
await app.RunAsync(args).ConfigureAwait(false);
