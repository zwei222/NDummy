using System.CommandLine;
using NDummy.Core;
using NDummy.Core.Utilities;

namespace NDummy.Commands;

public sealed class AppCommand : RootCommand
{
    private readonly Argument<string> sizeArgument;

    private readonly Option<string> outputOption;

    private readonly Option<bool> overwriteOption;

    public AppCommand()
    {
        // Arguments
        this.sizeArgument = new Argument<string>(
            name: "size",
            description: CommandDescriptions.Size);
        this.AddArgument(sizeArgument);

        // Options
        this.outputOption = new Option<string>(
            aliases: new[] { "--output", "-o" },
            description: CommandDescriptions.Output)
        {
            IsRequired = true
        };
        this.overwriteOption = new Option<bool>(
            aliases: new[] { "--overwrite" },
            description: CommandDescriptions.Overwrite,
            getDefaultValue: static () => false);
        this.AddOption(outputOption);
        this.AddOption(overwriteOption);

        // Handlers
        this.SetHandlers();
    }

    private void SetHandlers()
    {
        this.SetHandler(async (context) =>
        {
            var size = context.ParseResult.GetValueForArgument(this.sizeArgument);
            var output = context.ParseResult.GetValueForOption(this.outputOption)!;
            var overwrite = context.ParseResult.GetValueForOption(this.overwriteOption);
            var cancellationToken = context.GetCancellationToken();

            await this.RunAsync(
                size,
                output,
                overwrite,
                cancellationToken).ConfigureAwait(false);
        });
    }

    private async ValueTask RunAsync(
        string size,
        string output,
        bool overwrite = false,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var actualSize = FileSizeUtility.ConvertToByteSize(size);

            if (overwrite is false && File.Exists(output))
            {
                throw new ArgumentException("The output file already exists.");
            }

            try
            {
                await DummyGenerator.GenerateAsync(
                    output,
                    actualSize,
                    overwrite,
                    cancellationToken).ConfigureAwait(false);
            }
            catch (Exception)
            {
                if (File.Exists(output))
                {
                    File.Delete(output);
                }

                throw;
            }
        }
        catch (ArgumentException argumentException)
        {
            await Console.Error.WriteLineAsync(argumentException.Message);
            Environment.Exit(1);
        }
        catch (Exception exception)
        {
            await Console.Error.WriteLineAsync(exception.ToString());
            Environment.Exit(1);
        }
    }
}
