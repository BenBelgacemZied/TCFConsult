using System.Text.Json;
using TCFConsult.Application.Ports;
using TCFConsult.Domain;

namespace TCFConsult.Infrastructure.Storage;

public sealed class LocalDocumentStorage(string rootPath) : IDocumentStorage
{
    private readonly string _root = rootPath;
    private string IndexPath => Path.Combine(_root, "documents.json");

    public async Task<Document> SaveAsync(string fileName, Stream content, CancellationToken cancellationToken = default)
    {
        Directory.CreateDirectory(_root);
        var safeName = Path.GetFileName(fileName);
        var path = Path.Combine(_root, $"{Guid.NewGuid():N}-{safeName}");
        await using (var output = File.Create(path)) await content.CopyToAsync(output, cancellationToken);
        var info = new FileInfo(path);
        var document = Document.Create(safeName, info.Length);
        var list = (await ReadIndexAsync(cancellationToken)).ToList();
        list.Insert(0, document);
        await File.WriteAllTextAsync(IndexPath, JsonSerializer.Serialize(list), cancellationToken);
        return document;
    }

    public async Task<IReadOnlyList<Document>> ListAsync(CancellationToken cancellationToken = default) => await ReadIndexAsync(cancellationToken);

    private async Task<IReadOnlyList<Document>> ReadIndexAsync(CancellationToken cancellationToken)
    {
        if (!File.Exists(IndexPath)) return [];
        await using var input = File.OpenRead(IndexPath);
        return await JsonSerializer.DeserializeAsync<List<Document>>(input, cancellationToken: cancellationToken) ?? [];
    }
}