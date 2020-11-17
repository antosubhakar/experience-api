using Doctrina.ExperienceApi.Data.Json;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Server.Extensions
{
    public static class HttpResponseExtensions
    {
        public static async Task WriteJsonAsync(this HttpResponse response, object o, string contentType = null, CancellationToken cancellationToken = default)
        {
            var json = ObjectSerializer.ToString(o);
            await response.WriteJsonAsync(json, contentType, cancellationToken);
            await response.Body.FlushAsync();
        }

        public static async Task WriteJsonAsync(this HttpResponse response, string json, string contentType = null, CancellationToken cancellationToken = default)
        {
            response.ContentType = contentType ?? "application/json; charset=UTF-8";
            await response.WriteAsync(json, cancellationToken);
            await response.Body.FlushAsync();
        }
    }
}
