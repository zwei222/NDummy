using Cysharp.Collections;

namespace NDummy.Core;

public static class DummyGenerator
{
    public static async ValueTask GenerateAsync(
        string filePath,
        long size,
        bool overwrite = false,
        CancellationToken cancellationToken = default)
    {
        var buffer = GenerateRandomBytes(size);

        await buffer.WriteToFileAsync(
            filePath,
            overwrite ? FileMode.Create : FileMode.CreateNew,
            cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public static async ValueTask GenerateAsync(
        Stream stream,
        long size,
        CancellationToken cancellationToken = default)
    {
        var buffer = GenerateRandomBytes(size);

        await buffer.WriteToAsync(stream, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    private static NativeMemoryArray<byte> GenerateRandomBytes(long size)
    {
        var buffer = new NativeMemoryArray<byte>(size);

        foreach (var span in buffer.AsSpanSequence())
        {
            Random.Shared.NextBytes(span);
        }

        return buffer;
    }
}
