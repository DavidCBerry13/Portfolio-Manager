using Microsoft.Extensions.DependencyInjection;
using Securities.Core.AppInterfaces;
using Securities.Core.AppServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Securities.Core
{
    public static class IServiceCollectionExtensions
    {


        public static void RegisterServiceClasses(this IServiceCollection services)
        {
            services.AddScoped<ITradeDateService, TradeDateService>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<ISecurityPriceService, SecurityPriceService>();
        }


    }
}
