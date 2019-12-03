using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCheck.Configs
{
    public static class LocalizationConfigs
    {
        private static string DEFAULT_LANGUAGE = "en-US";
        private static string RESOURCE_FOLDER = "Resources";

        private static List<CultureInfo> supportedCultures = new List<CultureInfo>
        {
            // CultureInfo handles things like formatting the dates based on culture
            // It's part of the framework so we just look up the string for our culture and add it
            new CultureInfo(DEFAULT_LANGUAGE),
            new CultureInfo("hu-HU")
        };

        public static void SetLocalizationSource(this IServiceCollection services)
        {
            // Here we tell where to look for translations
            services.AddLocalization(options => options.ResourcesPath = RESOURCE_FOLDER);
        }

        public static void SetLocalization(this IServiceCollection services)
        {
            // Configuring the localization
            services.Configure<RequestLocalizationOptions>(options =>
            {
                // We set the default culture
                options.DefaultRequestCulture = new RequestCulture(DEFAULT_LANGUAGE);

                // SupportedCultures contains things like the date,time,number and currency formatting.
                options.SupportedCultures = supportedCultures;

                // SupportedUiCultures determines which translates strings (from .resx files)
                // are looked up by the ResourceManager.
                options.SupportedUICultures = supportedCultures;

                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    // Determine the culture information for a request via values in the query string.
                    new QueryStringRequestCultureProvider(),

                    //Determine the culture information for a request via the value of a cookie.
                    new CookieRequestCultureProvider()
                };
            });
        }
    }
}
