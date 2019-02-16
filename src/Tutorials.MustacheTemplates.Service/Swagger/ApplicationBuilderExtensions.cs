using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Tutorials.MustacheTemplates.Service.Swagger
{
    public static class ApplicationBuilderExtensions
    {
        public static void EnableBasicSwagger(this IApplicationBuilder instance)
        {
            instance.UseSwagger();

            instance.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }

        public static void EnableVersionedSwagger(this IApplicationBuilder instance, IApiVersionDescriptionProvider provider)
        {
            instance.UseSwagger();

            instance.UseSwaggerUI(
                options =>
                {
                    // build a swagger endpoint for each discovered API version
                    foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
        }
    }
}
