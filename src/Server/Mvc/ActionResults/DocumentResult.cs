using Doctrina.ExperienceApi.Data.Documents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Doctrina.ExperienceApi.Server.Mvc.ActionResults
{
    public class DocumentResult : FileContentResult
    {
        private readonly IDocument Document;

        public DocumentResult(IDocument document)
        : base(document.Content, document.ContentType)
        {
            if (document is null)
            {
                throw new System.ArgumentNullException(nameof(document));
            }

            Document = document;

            LastModified = document.LastModified;
            EntityTag = new EntityTagHeaderValue($"\"{document.Tag}\"");
        }

        public static DocumentResult Success(IDocument document)
        {
            return new DocumentResult(document);
        }
    }
}