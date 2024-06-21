using ConsoleAppFramework;
using NDummy.Core;
using NDummy.Core.Utilities;

namespace NDummy.Commands;

public sealed class AppCommand
{
    /// <summary>
    /// Create a dummy file of the specified size.
    /// </summary>
    /// <param name="size">
    /// 
    /// Specify the size of the file to be created. Acceptable units are:
    /// - Bytes(no unit or 'B' after the number, e.g., 1024 or 1024B)
    /// - Kilobytes(KB, e.g., 10KB)
    ///     - Megabytes(M or MB, e.g., 1M or 1MB)
    /// - Gigabytes(G or GB, e.g., 1G or 1GB)
    /// The size argument does not have a default and must be provided.
    /// </param>
    /// <param name="output">-o, Specify the output file path where the file will be written.</param>
    /// <param name="overwrite">Allows the command to overwrite the output file if it already exists at the specified path. Without this option, if the output file exists, the command will fail.</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    /// <returns>Exit code</returns>
    [Command("")]
    public async Task<int> RunAsync(
        [Argument] string size,
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
