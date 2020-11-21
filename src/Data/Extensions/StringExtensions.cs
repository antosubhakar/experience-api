namespace Doctrina.ExperienceApi.Data.Extensions
{
    public static class StringExtensions
    {
        public static string ToOpaqueQuotedString(this string s)
        {
            if(s.IndexOf($"\"{s}\"") == -1)
            {
                return $"\"{s}\"";
            }

            return s;
        }
    }
}