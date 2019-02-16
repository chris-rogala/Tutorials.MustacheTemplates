using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
#if (BASICSWAGGERNOCONTENT || BASICSWAGGER || VERSIONEDSWAGGER)
using Tutorials.MustacheTemplates.Service.Swagger;
#endif
#if VERSIONEDSWAGGER
using Microsoft.AspNetCore.Mvc.ApiExplorer;
#endif
namespace Tutorials.MustacheTemplates.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

#if BASICSWAGGERNOCONTENT
            services.EnableBasicSwagger(false);
#endif

#if BASICSWAGGER
            services.EnableBasicSwagger();
#endif

#if VERSIONEDSWAGGER
            services.EnableVersionedSwagger();
#endif
        }

#if VERSIONEDSWAGGER
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
#else
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
#endif
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMvc();

#if (BASICSWAGGERNOCONTENT || BASICSWAGGER)
            app.EnableBasicSwagger();
#endif

#if VERSIONEDSWAGGER
            app.EnableVersionedSwagger(provider);
#endif
        }
    }
}
