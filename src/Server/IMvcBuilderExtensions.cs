using Doctrina.ExperienceApi.Server.Controllers;
using Doctrina.ExperienceApi.Server.Mvc.ActionResults;
using Doctrina.ExperienceApi.Server.Mvc.Formatters;
using Doctrina.ExperienceApi.Server.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;

namespace Doctrina.ExperienceApi.Server
{
    public static class IMvcBuilderExtensions
    {
        public static IMvcBuilder AddExperienceApi(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder
                .AddApplicationPart(typeof(StatementsController).Assembly)
                .AddControllersAsServices();

            //mvcBuilder.ConfigureApiBehaviorOptions(options =>
            //{
            //    options.InvalidModelStateResponseFactory = ctx => new ValidationActionResult();
                
            //});

            mvcBuilder.AddMvcOptions(options =>
            {
                options.InputFormatters.Insert(0, new RawRequestBodyFormatter());

                options.ModelBinderProviders.Insert(0, new IriModelBinderProvider());
                options.ModelBinderProviders.Insert(0, new AgentModelBinderProvider());
                options.ModelBinderProviders.Insert(0, new StatementModelBinderProvider());
                options.ModelBinderProviders.Insert(0, new StatementCollectionModelBinderProvider());
            });

            return mvcBuilder;
        }
    }
}
