using System;

namespace Doctrina.ExperienceApi.Data.Documents
{
    public interface IDocument
    {
        string Id { get; set; }
        byte[] Content { get; set; }
        string ContentType { get; set; }
        string Tag { get; set; }
        DateTimeOffset? LastModified { get; set; }
    }
}