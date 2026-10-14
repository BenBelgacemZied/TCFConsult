using TCFConsult.Domain;

namespace TCFConsult.Application.Ports;

public interface IDocumentStorage
{
    Task<Document> SaveAsync(string fileName, Stream content, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Document>> ListAsync(CancellationToken cancellationToken = default);
}
