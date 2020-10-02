using Newtonsoft.Json.Linq;

namespace Doctrina.ExperienceApi.Data{
    public interface IJsonData
    {
        string ToJson();

        string ToJson(ResultFormat format = ResultFormat.Exact);
    }
}
