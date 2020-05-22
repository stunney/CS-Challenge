using System;
using System.Globalization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using LocalizationCultureCore.StringLocalizer;

namespace JokeGenerator
{
    public sealed class LocalizedConsoleWriter
    {
        private readonly ILogger logger;
        private readonly IStringLocalizer localizer;
        
        internal LocalizedConsoleWriter(string cultureString="en-CA")
        {
            if (string.IsNullOrEmpty(cultureString)) throw new ArgumentNullException("cultureString");

            CultureInfo.CurrentUICulture = new CultureInfo(cultureString, false);

            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder
                .AddConsole()
                .AddFilter(level => level >= LogLevel.Warning)
            );

            var loggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();
            logger = loggerFactory.CreateLogger("");
            
            localizer = (IStringLocalizer)new JsonStringLocalizer("Resources", "JokeGenerator", logger, CultureInfo.CurrentUICulture);            
        }

        internal void WriteLine()
        {
            Console.WriteLine();
        }

        internal void WriteLine(string key, params object[] args)
        {
            string val = GetLocalizedString(key);
            if (string.IsNullOrEmpty(val)) val = key;
            Console.WriteLine(val, args);
        }

        internal string GetLocalizedString(string key)
        {
            return localizer.GetString(key);
        }
    }
}