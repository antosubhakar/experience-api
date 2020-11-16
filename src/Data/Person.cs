using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Doctrina.ExperienceApi.Data
{
    /// <summary>
    /// The Person Object is very similar to an Agent Object, but instead of each attribute having a single value, each attribute has an array value, and it is legal to include multiple identifying properties. This is different from the FOAF concept of person, person is being used here to indicate a person-centric view of the LRS Agent data, but Agents just refer to one persona (a person in one context).
    /// </summary>
    [JsonObject]
    public class Person
    {
        public Person()
        {
            Name = new List<string>();
            Mbox = new List<Mbox>();
            Mbox_sha1sum = new List<string>();
            OpenId = new List<Iri>();
            Account = new List<Account>();
        }

        [JsonProperty("objectType")]
        public string ObjectType { get; private set; } = "Person";

        /// <summary>
        /// List of names of Agents retrieved.	
        /// </summary>
        [JsonProperty("name")]
        public List<string> Name { get; set; }

        /// <summary>
        /// List of e-mail addresses of Agents retrieved.	
        /// </summary>
        [JsonProperty("mbox")]
        public List<Mbox> Mbox { get; set; }

        /// <summary>
        /// List of the SHA1 hashes of mailto IRIs (such as go in an mbox property).	
        /// </summary>
        [JsonProperty("mbox_sha1sum")]
        public List<string> Mbox_sha1sum { get; set; }

        /// <summary>
        /// List of openids that uniquely identify the Agents retrieved.	
        /// </summary>
        [JsonProperty("openid")]
        public List<Iri> OpenId { get; set; }

        /// <summary>
        /// List of accounts to match. Complete account Objects (homePage and name) MUST be provided.
        /// </summary>
        [JsonProperty("account")]
        public List<Account> Account { get; set; }

        /// <summary>
        /// Adds Agent to Person object
        /// </summary>
        /// <param name="agent">Agent Object with a single identifier, It is not a Person Object, nor is it a Group.</param>
        public void Add(Agent agent)
        {
            if (agent == null)
            {
                throw new ArgumentNullException("agent");
            }

            if (agent.ObjectType == ExperienceApi.Data.ObjectType.Group)
            {
                throw new ArgumentException("Groups are not allowed within an Person Object.");
            }

            if (agent.Account != null)
            {
                Account.Add(agent.Account);
                return;
            }

            if (agent.Mbox != null)
            {
                Mbox.Add(agent.Mbox);
                return;
            }

            if (!string.IsNullOrWhiteSpace(agent.Mbox_SHA1SUM))
            {
                Mbox_sha1sum.Add(agent.Mbox_SHA1SUM);
                return;
            }

            if (agent.OpenId != null)
            {
                OpenId.Add(agent.OpenId);
                return;
            }
        }

        public void Combine(Agent agent)
        {
            Add(agent);
        }
    }
}
