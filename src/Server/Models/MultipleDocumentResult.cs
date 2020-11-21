using System;
using System.Collections.Generic;

namespace Doctrina.ExperienceApi.Server.Models
{
    public class MultipleDocumentResult
    {
        public MultipleDocumentResult()
        {
            Ids = new HashSet<string>();
        }

        public ICollection<string> Ids { get; set; }

        public DateTimeOffset? LastModified { get; set; }

        public bool IsEmpty { get; private set; }

        public static MultipleDocumentResult Success(ICollection<string> ids, DateTimeOffset? lastModified)
        {
            return new MultipleDocumentResult()
            {
                Ids = ids,
                LastModified = lastModified
            };
        }

        public static MultipleDocumentResult Empty()
        {
            return new MultipleDocumentResult()
            {
                IsEmpty = true
            };
        }
    }
}