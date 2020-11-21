﻿using Doctrina.ExperienceApi.Server.Exceptions;
using Doctrina.ExperienceApi.Server.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Doctrina.ExperienceApi.Server.Routing
{
    /// <summary>
    /// Re-route alternate requests to controllers
    /// </summary>
    public class AlternateRequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string[] allowedMethodNames = new string[] { "POST", "GET", "PUT", "DELETE" };
        private readonly string[] formHttpHeaders = new string[] { "Authorization", "X-Experience-API-Version", "Content-Type", "Content-Length", "If-Match", "If-None-Match" };
        // TODO: This might work in most chases but is not really valid.
        private readonly Regex unsafeUrlRegex = new Regex(@"^-\]_.~!*'();:@&=+$,/?%#[A-z0-9]");

        public AlternateRequestMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.HasValue && context.Request.Path.Value.StartsWith("/xapi/"))
            {
                await AlternateRequest(context);
            }
            else
            {
                await _next(context);
            }
        }

        private static async Task BadRequest(HttpContext context, string message)
        {
            await context.Response.WriteBadRequest(new { message = message }, contentType: "application/json");
        }

        private Task AlternateRequest(HttpContext context)
        {
            var request = context.Request;

            // Must include parameter method
            string methodQuery = request.Query["method"].FirstOrDefault()?.ToUpperInvariant();
            if (string.IsNullOrWhiteSpace(methodQuery))
            {
                return _next(context);
            }

            if (request.Method.ToUpperInvariant() != "POST")
            {
                return BadRequest(context, "An LRS rejects an alternate request syntax not issued as a POST");
            }

            if (!allowedMethodNames.Contains(methodQuery))
            {
                return BadRequest(context, $"Query parameter method \"{methodQuery}\" is not alloed. ");
            }

            // Multiple query parameters are not allowed
            if (request.Query.Count != 1)
            {
                return BadRequest(context, "An LRS will reject an alternate request syntax which contains any extra information with error code 400 Bad Request (Communication 1.3.s3.b4)");
            }

            if (!request.HasFormContentType)
            {
                return BadRequest(context, "Alternate request syntax sending content does not have a form parameter with the name of \"content\"");
            }

            // Set request method to query method
            request.Method = methodQuery;

            // Parse form data values
            var formData = request.Form.ToDictionary(x => x.Key, y => y.Value.ToString());
            request.ContentType = "application/json";

            if (new string[] { "POST", "PUT" }.Contains(methodQuery))
            {
                if (!formData.ContainsKey("content"))
                {
                    // An LRS will reject an alternate request syntax sending content which does not have a form parameter with the name of \"content\" (Communication 1.3.s3.b4)
                    context.Response.StatusCode = 400;
                    return BadRequest(context, "Alternate request syntax sending content does not have a form parameter with the name of \"content\"");
                }
            }

            if (formData.ContainsKey("content"))
            {
                string urlEncodedContent = formData["content"];

                if (unsafeUrlRegex.IsMatch(urlEncodedContent))
                {
                    return BadRequest(context, $"Form data 'content' contains unsafe charactors.");
                }

                string decodedContent = HttpUtility.UrlDecode(urlEncodedContent);
                var ms = new MemoryStream();
                using (var sw = new StreamWriter(ms, Encoding.UTF8, leaveOpen: true))
                {
                    sw.Write(decodedContent);
                }
                ms.Position = 0;
                request.Body = ms;

                formData.Remove("content");
            }

            // Treat all known form headers as request headers
            if (formData.Any())
            {
                foreach (var headerName in formHttpHeaders)
                {
                    var formHeader = formData.FirstOrDefault(x => x.Key.Equals(headerName, StringComparison.InvariantCultureIgnoreCase));
                    if (!formHeader.Equals(default(KeyValuePair<string, string>)))
                    {
                        request.Headers[formHeader.Key] = formHeader.Value;
                        formData.Remove(formHeader.Key);
                    }
                }
            }

            // Treat the rest as query parameters
            var queryCollection = HttpUtility.ParseQueryString(string.Empty);
            foreach (var name in formData)
            {
                queryCollection.Add(name.Key, name.Value);
            }
            if (queryCollection.Count > 0)
            {
                request.QueryString = new QueryString("?" + queryCollection.ToString());
            }
            else
            {
                request.QueryString = new QueryString();
            }

            return _next(context);
        }
    }
}
