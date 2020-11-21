using System;

namespace Doctrina.ExperienceApi.Data.Documents
{
    public class Document : IDocument
    {
        /// <inheritdoc />
        public string Tag { get; set; }

        /// <inheritdoc />
        public DateTimeOffset? LastModified { get; set; }

        /// <inheritdoc />
        public string ContentType { get; set; }

        /// <inheritdoc />
        public byte[] Content { get; set; }

        /// <inheritdoc />
        public string Id { get; set; }
    }
}
