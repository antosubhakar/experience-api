
using Microsoft.AspNetCore.Http;
using System;
using System.Net;

namespace Doctrina.ExperienceApi.Server.Extensions
{
    public static class HttpRequestExtensions
    {
        public static bool TryConcurrencyCheck(this HttpRequest request, string savedEntityTag, DateTimeOffset? lastLodified, out int statusCode)
        {
            statusCode = -1;
            var headers = request.GetTypedHeaders();

            if(request.Method == HttpMethods.Put
            && headers.IfNoneMatch.Count == 0
            && headers.IfMatch.Count == 0)
            {
                statusCode = StatusCodes.Status400BadRequest;
                return true;
            }

            if (headers.IfNoneMatch.Count > 0)
            {
                foreach (var noneMatch in headers.IfNoneMatch)
                {
                    if (!noneMatch.Tag.HasValue)
                    {
                        continue;
                    }

                    if (noneMatch.Tag.Value == "*")
                    {
                        if (!string.IsNullOrEmpty(savedEntityTag))
                        {
                            statusCode = StatusCodes.Status412PreconditionFailed;
                            return true;
                        }
                    }
                    else if (noneMatch.Tag.Value == $"\"{savedEntityTag}\"")
                    {
                        statusCode = StatusCodes.Status412PreconditionFailed;
                        return true;
                    }
                }
            }

            if (headers.IfMatch.Count > 0)
            {
                foreach (var match in headers.IfMatch)
                {
                    if (!match.Tag.HasValue)
                    {
                        continue;
                    }

                    if (match.Tag.Value == "*")
                    {
                        statusCode = StatusCodes.Status412PreconditionFailed;
                        return true;
                    }
                    else if (match.Tag.Value != $"\"{savedEntityTag}\"")
                    {
                        statusCode = StatusCodes.Status412PreconditionFailed;
                        return true;
                    }
                }
            }

            return false;
        }
    }
}