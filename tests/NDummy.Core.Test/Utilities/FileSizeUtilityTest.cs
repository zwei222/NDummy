using NDummy.Core.Utilities;
using Xunit;

namespace NDummy.Core.Test.Utilities;

public class FileSizeUtilityTest
{
    [Theory(DisplayName = "File size")]
    [InlineData("1", 1)]
    [InlineData("1B", 1)]
    [InlineData("1K", 1024)]
    [InlineData("1M", 1024 * 1024)]
    [InlineData("1G", 1024 * 1024 * 1024)]
    [InlineData("1024", 1024)]
    [InlineData("1KB", 1024)]
    [InlineData("1MB", 1024 * 1024)]
    [InlineData("1GB", 1024 * 1024 * 1024)]
    public void FileSizeTest(string fileSize, int expected)
    {
        var actual = FileSizeUtility.ConvertToByteSize(fileSize);

        Assert.Equal(expected, actual);
    }
}
