namespace Doctrina.ExperienceApi.Data.Extensions
{
    public static class StatementObjectExtensions
    {
        public static TObject As<TObject>(this IStatementObject obj)
        where TObject : IStatementObject
        {
            return (TObject)obj;
        }
    }
}