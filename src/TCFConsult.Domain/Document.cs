namespace TCFConsult.Domain;

public sealed record Document(Guid Id, string FileName, DateTimeOffset CreatedAt, long SizeBytes)
{
    public static Document Create(string fileName, long sizeBytes) =>
        new(Guid.NewGuid(), fileName, DateTimeOffset.UtcNow, sizeBytes);
}