using Doctrina.ExperienceApi.Server.Routing;
using Microsoft.AspNetCore.Builder;
using System;

namespace Doctrina.ExperienceApi.Server
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAlternateRequestSyntax(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AlternateRequestMiddleware>();
        }

        public static IApplicationBuilder UseUnrecognizedParameters(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UnrecognizedParametersMiddleware>();
        }

        public static IApplicationBuilder UseConsistentThrough(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ConsistentThroughMiddleware>();
        }

        /// <summary>
        /// Maps required middlewares for the LRS Server to receive statements from clients
        /// </summary>
        public static IApplicationBuilder UseExperienceApiEndpoints(this IApplicationBuilder builder, Action<ApiEndpointOptions> options = null)
        {
            var defaultOptions = new ApiEndpointOptions();

            options?.Invoke(defaultOptions);

            builder.MapWhen(context => context.Request.Path.StartsWithSegments(defaultOptions.Path), experienceApi =>
            {
                experienceApi.UseMiddleware<ApiExceptionMiddleware>();

                experienceApi.UseAlternateRequestSyntax();

                experienceApi.UseRequestLocalization();

                experienceApi.UseRouting();

                experienceApi.UseAuthentication();
                experienceApi.UseAuthorization();

                experienceApi.UseConsistentThrough();

                experienceApi.UseUnrecognizedParameters();

                experienceApi.UseEndpoints(routes =>
                {
                    routes.MapControllerRoute(
                        name: "experience_api",
                        pattern: "xapi/{controller=About}");
                });
            });

            return builder;
        }
    }
}
