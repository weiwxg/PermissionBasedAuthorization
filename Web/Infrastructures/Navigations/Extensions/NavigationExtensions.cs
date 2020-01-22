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
        public static IServiceCollection AddNavigation(this IServiceCollection services)
        {
            services.AddSingleton<INavigationManager, NavigationManager>();
            services.AddSingleton<INavigationProvider, NavigationProvider>();
            return services;
        }

        public static IServiceCollection AddNavigation<TNavigationProvider>(this IServiceCollection services) where TNavigationProvider : INavigationProvider
        {
            services.AddSingleton<INavigationManager, NavigationManager>();
            services.AddSingleton(typeof(INavigationProvider), typeof(TNavigationProvider));
            return services;
        }

        public static IServiceCollection AddNavigation(this IServiceCollection services, Type navigationProviderType)
        {
            services.AddSingleton<INavigationManager, NavigationManager>();
            services.AddSingleton(typeof(INavigationProvider), navigationProviderType);
            return services;
        }

        public static IHost UseNavigation(this IHost host)
        {
            var navigationProvider = host.Services.GetRequiredService<INavigationProvider>();
            navigationProvider.Initialize();
            return host;
        }
    }
}
