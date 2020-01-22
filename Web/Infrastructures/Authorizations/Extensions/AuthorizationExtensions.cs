using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Web.Infrastructures.Authorizations.Extensions
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection AddPermissionAuthorizationCore(this IServiceCollection services)
        {
            // 授权核心服务
            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, PermissionClaimsProducer>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();

            // 权限管理相关服务
            services.AddSingleton<IPermissionManager, PermissionManager>();
            services.AddSingleton<PermissionProvider, PermissionProvider>();
            return services;
        }

        public static IServiceCollection AddPermissionProvider<TPermissionProvider>(this IServiceCollection services) where TPermissionProvider : IPermissionProvider
        {
            services.AddSingleton(typeof(IPermissionProvider), typeof(TPermissionProvider));
            return services;
        }

        public static IServiceCollection AddPermissionProvider(this IServiceCollection services, Type permissionProviderType)
        {
            services.AddSingleton(typeof(IPermissionProvider), permissionProviderType);
            return services;
        }

        public static IHost UsePermissionProvider(this IHost host)
        {
            var permissionProvider = host.Services.GetRequiredService<IPermissionProvider>();
            permissionProvider.Initialize();
            return host;
        }
    }
}
