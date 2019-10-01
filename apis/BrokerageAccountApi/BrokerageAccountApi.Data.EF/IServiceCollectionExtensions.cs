using BrokerageAccountApi.Core.DataAccess;
using BrokerageAccountApi.Data.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerageAccountApi.Data.EF
{
    public static class IServiceCollectionExtensions
    {



        public static void RegisterEfCoreDataAccessClasses(this IServiceCollection services,
            String connectionString, ILoggerFactory loggerFactory)
        {
            services.AddDbContext<BrokerageAccountDbContext>(options =>
                options
                .UseSqlServer(connectionString)
                .UseLoggerFactory(loggerFactory)
            );

            services.AddScoped<IStateRepository, StateRepository>();
        }

    }
}
