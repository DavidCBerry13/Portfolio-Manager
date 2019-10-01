using Microsoft.Extensions.DependencyInjection;
using Securities.Core.DataAccess;
using Securities.Data.Dapper.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Securities.Data.Dapper
{
    public static class IServiceCollectionExtensions
    {


        public static void RegisterDapperDataAccessClasses(this IServiceCollection services,
            String connectionString)
        {
            services.AddScoped<ITradeDateRepository>(serviceProvider => new TradeDateRepository(connectionString));
            services.AddScoped<ISecurityRepository>(serviceProvider => new SecurityRepository(connectionString));
            services.AddScoped<ISecurityPriceRepository>(serviceProvider => new SecurityPriceRepository(connectionString));
        }


    }
}
