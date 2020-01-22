using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Infrastructures.Navigations.Extensions
{
    public static class NavigationExtensions
    {
        public static IServiceCollection AddNavigationCore(this IServiceCollection services)
        {
            services.AddSingleton<INavigationManager, NavigationManager>();
            services.AddSingleton<INavigationProvider, NavigationProvider>();
            return services;
        }

        public static IServiceCollection AddNavigationProvider<TNavigationProvider>(this IServiceCollection services) where TNavigationProvider : INavigationProvider
        {
            services.AddSingleton(typeof(INavigationProvider), typeof(TNavigationProvider));
            return services;
        }

        public static IServiceCollection AddNavigationProvider(this IServiceCollection services, Type navigationProviderType)
        {
            services.AddSingleton(typeof(INavigationProvider), navigationProviderType);
            return services;
        }

        public static IHost UseNavigationProvider(this IHost host)
        {
            var navigationProvider = host.Services.GetRequiredService<INavigationProvider>();
            navigationProvider.Initialize();
            return host;
        }
    }
}
