namespace NDummy.Commands;

public static class CommandDescriptions
{
    public const string Size = """
                               Specify the size of the file to be created. Acceptable units are:
                                 - Bytes (no unit or 'B' after the number, e.g., 1024 or 1024B)
                                 - Kilobytes (KB, e.g., 10KB)
                                 - Megabytes (M or MB, e.g., 1M or 1MB)
                                 - Gigabytes (G or GB, e.g., 1G or 1GB)
                               The size argument does not have a default and must be provided.
                               """;

    public const string Output = """
                                 Specify the output file path where the file will be written.
                                 """;

    public const string Overwrite = """
                                    Allows the command to overwrite the output file if it already exists at the specified path. Without this option, if the output file exists, the command will fail.
                                    """;
}
