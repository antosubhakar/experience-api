using System.Runtime.Serialization;

namespace Doctrina.ExperienceApi.Data
{
    /// <summary>
    /// Resulst formats: ids, exact or canonical.
    /// </summary>
    public enum ResultFormat
    {
        /// <summary>
        /// If ids, only includes minimum information necessary in Agent, Activity, 
        /// Verb and Group Objects to identify them. For Anonymous Groups this 
        /// means including the minimum information needed to identify each member.
        /// </summary>
        [EnumMember(Value = "ids")]
        Ids = 0,

        /// <summary>
        /// If exact, returns Agent, Activity, Verb and Group Objects populated 
        /// exactly as they were when the Statement was received. 
        /// An LRS requesting Statements for the purpose of importing them would 
        /// use a format of "exact" in order to maintain Statement Immutability.
        /// </summary>
        [EnumMember(Value = "exact")]
        Exact = 1,

        /// <summary>
        /// If canonical, returns Activity Objects and Verbs populated with the 
        /// canonical definition of the Activity Objects and Display of the Verbs 
        /// as determined by the LRS, after applying the language filtering 
        /// process defined below, and return the original Agent and Group 
        /// Objects as in "exact" mode.
        /// </summary>
        [EnumMember(Value = "canonical")]
        Canonical = 2
    }
}
