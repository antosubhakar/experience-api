using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace Doctrina.ExperienceApi.Data
{
    public class StatementsQuery
    {
        /// <summary>
        /// Id of Statement to fetch	
        /// </summary>
        [FromQuery(Name = "statementId")]
        public Guid? StatementId { get; set; }

        /// <summary>
        /// Id of voided Statement to fetch.
        /// </summary>
        [FromQuery(Name = "voidedStatementId")]
        public Guid? VoidedStatementId { get; set; }

        /// <summary>
        /// Filter, only return Statements for which the specified Agent or Group is the Actor or Object of the Statement.
        /// </summary>
        [FromQuery(Name = "agent")]
        public Agent Agent { get; set; }

        /// <summary>
        /// Filter, only return Statements matching the specified Verb id.	
        /// </summary>
        [FromQuery(Name = "verb")]
        public Iri VerbId { get; set; }

        /// <summary>
        /// Filter, only return Statements for which the Object of the Statement is an Activity with the specified id.	
        /// </summary>
        [FromQuery(Name = "activity")]
        public Iri ActivityId { get; set; }

        /// <summary>
        /// Filter, only return Statements matching the specified registration id. Note that although frequently a unique registration will be used for one Actor assigned to one Activity, this cannot be assumed. If only Statements for a certain Actor or Activity are required, those parameters also need to be specified.
        /// </summary>
        [FromQuery(Name = "registration")]
        public Guid? Registration { get; set; }

        [FromQuery(Name = "related_activities")]
        public bool? RelatedActivities { get; set; }

        [FromQuery(Name = "related_agents")]
        public bool? RelatedAgents { get; set; }

        /// <summary>
        /// Only Statements stored since the specified Timestamp (exclusive) are returned.	
        /// </summary>
        [FromQuery(Name = "since")]
        public DateTimeOffset? Since { get; set; }

        /// <summary>
        /// Only Statements stored at or before the specified Timestamp are returned.
        /// </summary>
        [FromQuery(Name = "until")]
        public DateTimeOffset? Until { get; set; }

        /// <summary>
        /// Maximum number of Statements to return. 0 indicates return the maximum the server will allow.
        /// </summary>
        [FromQuery(Name = "limit")]
        public int? Limit { get; set; }

        [FromQuery(Name = "format")]
        public ResultFormat? Format { get; set; }

        [FromQuery(Name = "ascending")]
        public bool? Ascending { get; set; }

        /// <summary>
        /// If true, the LRS uses the multipart response format and includes all attachments as described previously. If false, the LRS sends the prescribed response with Content-Type application/json and does not send attachment data.	
        /// </summary>
        [FromQuery(Name = "attachments")]
        public bool? Attachments { get; set; }

        public StatementsQuery() { }

        public virtual NameValueCollection ToParameterMap(ApiVersion version)
        {
            var result = HttpUtility.ParseQueryString(string.Empty);

            if (Agent != null)
            {
                result.Add("agent", Agent.ToJson(version));
            }
            if (VerbId != null)
            {
                result.Add("verb", VerbId.ToString());
            }
            if (ActivityId != null)
            {
                result.Add("activity", ActivityId.ToString());
            }
            if (Registration != null)
            {
                result.Add("registration", Registration.Value.ToString());
            }
            if (RelatedActivities != null)
            {
                result.Add("related_activities", RelatedActivities.Value.ToString());
            }
            if (RelatedAgents != null)
            {
                result.Add("related_agents", RelatedAgents.Value.ToString());
            }
            if (Since != null)
            {
                result.Add("since", Since.Value.ToString("o"));
            }
            if (Until != null)
            {
                result.Add("until", Until.Value.ToString("o"));
            }
            if (Limit != null)
            {
                result.Add("limit", Limit.ToString());
            }
            if (Format.HasValue)
            {
                result.Add("format", Enum.GetName(typeof(ResultFormat), Format.Value));
            }
            if (Attachments != null)
            {
                result.Add("attachments", Attachments.Value.ToString());
            }
            if (Ascending != null)
            {
                result.Add("ascending", Ascending.Value.ToString());
            }

            return result;
        }

        public virtual StatementsQuery ParseQuery(IQueryCollection query)
        {
            var parameters = new StatementsQuery();
            if(query.TryGetValue("agent", out StringValues agentString))
            {
                parameters.Agent = new Agent((string)agentString);
            }

            if (query.TryGetValue("verb", out StringValues verbString))
            {
                parameters.VerbId = new Iri((string)verbString);
            }

            if (query.TryGetValue("activity", out StringValues activityString))
            {
                parameters.ActivityId = new Iri((string)activityString);
            }

            if (query.TryGetValue("registration", out StringValues registrationString))
            {
                parameters.Registration = Guid.Parse((string)registrationString);
            }

            if (query.TryGetValue("related_activities", out StringValues relatedActivitiesString))
            {
                if(bool.TryParse(relatedActivitiesString, out bool relatedActivities))
                    parameters.RelatedActivities = relatedActivities;
            }

            if (query.TryGetValue("related_agents", out StringValues relateAgentsString))
            {
                if(bool.TryParse(relateAgentsString, out bool relatedAgents))
                    parameters.RelatedAgents = relatedAgents;
            }

            if (query.TryGetValue("since", out StringValues sinceString))
            {
                if (DateTimeOffset.TryParse(sinceString, out DateTimeOffset since))
                    parameters.Since = since;
            }

            if (query.TryGetValue("until", out StringValues untilString))
            {
                if (DateTimeOffset.TryParse(untilString, out DateTimeOffset until))
                    parameters.Until = until;
            }

            if(query.TryGetValue("format", out StringValues formatString))
            {
                if (Enum.TryParse(formatString, out ResultFormat format))
                    parameters.Format = format;
            }

            if (query.TryGetValue("attachments", out StringValues attachmentsString))
            {
                if (bool.TryParse(attachmentsString, out bool attachments))
                    parameters.Attachments = attachments;
            }

            if (query.TryGetValue("ascending", out StringValues ascendingString))
            {
                if (bool.TryParse(ascendingString, out bool ascending))
                    parameters.Ascending = ascending;
            }

            return parameters;
        }
    }
}
