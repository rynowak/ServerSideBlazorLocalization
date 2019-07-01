using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServerSideBlazorLocalization.Data;
using System;
using System.Linq;

namespace ServerSideBlazorLocalization
{
    [BindInputElement("date", "value", "value", "onchange", true, "yyyy-MM-dd")]
    [BindInputElement("number", "value", "value", "onchange", true, null)]
    public class BindAttributes
    {
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // Priority of culture providers:
            // BasePath
            // Query string: "culture"
            // Cookie: ".AspNetCore.Culture"
            app.UseRequestLocalization(options =>
            {
                options.AddSupportedCultures("en-US", "fr-FR");
                options.AddSupportedUICultures("en-US", "fr-FR");
                options.SetDefaultCulture("en-US");

                options.RequestCultureProviders.Remove(options.RequestCultureProviders.OfType<AcceptLanguageHeaderRequestCultureProvider>().Single());
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub<App>("app");
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
