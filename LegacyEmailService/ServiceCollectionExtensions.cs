using Microsoft.Extensions.DependencyInjection;
using System;

namespace DannyBoyNg.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEmailService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ILegacyEmailService, LegacyEmailService>();
            return serviceCollection;
        }

        public static IServiceCollection AddEmailService(this IServiceCollection serviceCollection, Action<EmailSettings> options)
        {
            serviceCollection.AddScoped<ILegacyEmailService, LegacyEmailService>();
            serviceCollection.Configure(options);
            return serviceCollection;
        }
    }
}
