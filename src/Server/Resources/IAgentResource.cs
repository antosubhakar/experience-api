using Doctrina.ExperienceApi.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Server.Resources
{
    /// <summary>
    /// The Agents Resource provides a method to retrieve a special Object with combined information about an Agent derived from an outside service, such as a directory service. 
    /// This resource has Concurrency controls associated with it.
    /// </summary>
    public interface IAgentResource
    {
        /// <summary>
        /// Return a special, Person Object for a specified Agent. 
        /// The Person Object is very similar to an Agent Object, but instead of 
        /// each attribute having a single value, each attribute has an array value, 
        /// and it is legal to include multiple identifying properties. 
        /// This is different from the FOAF concept of person, person is being used here to indicate a person-centric view of the LRS Agent data, but Agents just refer to one persona (a person in one context).
        /// The "agent" parameter is a normal Agent Object with a single identifier and no arrays.It is not a Person Object, nor is it a Group.
        /// </summary>
        /// <param name="agent">The Agent representation to use in fetching expanded Agent information.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        Task<Person> GetPerson(Agent agent, CancellationToken cancellationToken = default);
    }
}
