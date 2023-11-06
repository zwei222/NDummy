namespace NDummy.Core.Utilities;

public static class FileSizeUtility
{
    public static long ConvertToByteSize(ReadOnlySpan<char> fileSize)
    {
        var sizeUnitLength = 0;

        do
        {
            sizeUnitLength++;
        } while (char.IsDigit(fileSize[^sizeUnitLength]) is false);

        sizeUnitLength--;

        if (sizeUnitLength == 0)
        {
            return int.Parse(fileSize);
        }

        var sizeUnit = fileSize[^sizeUnitLength];
        var sizeValue = long.Parse(fileSize[..^sizeUnitLength]);

        return sizeUnit switch
        {
            'B' => sizeValue,
            'K' => sizeValue * 1024,
            'M' => sizeValue * 1024 * 1024,
            'G' => sizeValue * 1024 * 1024 * 1024,
            _ => throw new ArgumentException("Invalid size unit.", nameof(fileSize))
        };
    }
}
