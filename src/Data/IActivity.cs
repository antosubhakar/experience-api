using System;

namespace Doctrina.ExperienceApi.Data
{
    public interface IActivity
    {
        Iri Id { get; }
        ActivityDefinition Definition { get; }
    }
}