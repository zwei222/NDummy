using System.CommandLine;
using NDummy.Commands;

var appCommand = new AppCommand();

await appCommand.InvokeAsync(args).ConfigureAwait(false);
