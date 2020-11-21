using System;

namespace Doctrina.ExperienceApi.Data.Documents
{
    public interface IDocument
    {
        /// <summary>
        /// The unique id of the document (ProfileId, StateId)
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Binary contents of the document
        /// </summary>
        byte[] Content { get; set; }

        /// <summary>
        /// Content type of the content
        /// </summary>
        string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the tag without quotes
        /// </summary>
        string Tag { get; set; }

        /// <summary>
        /// Last Modified
        /// </summary>
        DateTimeOffset? LastModified { get; set; }
    }
}