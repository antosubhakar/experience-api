﻿using System;

namespace Doctrina.ExperienceApi.Data.Documents
{
    public class Document : IDocument
    {
        /// <summary>
        /// Gets or sets the opaque quoted string.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Last Modified
        /// </summary>
        public DateTimeOffset? LastModified { get; set; }

        /// <summary>
        /// Content type of the content
        /// </summary>
        public string ContentType { get; set; }

        public byte[] Content { get; set; }
        public string Id { get; set; }
    }
}
