using NDummy.Core;
using NDummy.Core.Utilities;

namespace NDummy.Commands;

public sealed class AppCommand : ConsoleAppBase
{
    [RootCommand]
    public async Task<int> RunAsync(
        [Option(0, CommandDescriptions.Size)] string size,
        [Option("o", CommandDescriptions.Output)] string output,
        [Option(null, CommandDescriptions.Overwrite)] bool overwrite = false)
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
                    this.Context.CancellationToken).ConfigureAwait(false);
            }
            catch (Exception)
            {
                if (File.Exists(output))
                {
                    File.Delete(output);
                }

                throw;
            }

            return 0;
        }
        catch (ArgumentException argumentException)
        {
            await Console.Error.WriteLineAsync(argumentException.Message);
            return 1;
        }
        catch (Exception exception)
        {
            await Console.Error.WriteLineAsync(exception.ToString());
            return 1;
        }
    }
}
