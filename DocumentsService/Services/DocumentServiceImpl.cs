using DocumentsService.Protos;
using Grpc.Core;

namespace DocumentsService.Services;

public class DocumentServiceImpl : DocumentService.DocumentServiceBase
{
    private static readonly List<Document> Documents = new()
    {
        new Document
        {
            Id = "1",
            PatientId = "c1a2b3d4-e5f6-4789-9012-3456789abcde",
            Name = "Document 1",
        },
        new Document
        {
            Id = "2",
            PatientId = "456",
            Name = "Document 2",
        },
    };

    public override Task<DocumentList> GetAll(PatientId request, ServerCallContext context)
    {
        var documentList = new DocumentList();
        documentList.Documents.AddRange(Documents.Where(d => d.PatientId == request.Id));
        return Task.FromResult(documentList);
    }

    public override Task<Document> Get(DocumentId request, ServerCallContext context)
    {
        var document = Documents.Find(doc => doc.Id == request.Id);
        if (document is null)
        {
            throw new RpcException(
                new Status(StatusCode.NotFound, $"Document with ID {request.Id} not found.")
            );
        }
        return Task.FromResult(document);
    }
}
