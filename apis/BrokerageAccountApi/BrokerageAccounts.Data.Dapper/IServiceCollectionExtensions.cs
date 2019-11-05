using BrokerageAccountApi.Core.DataAccess;
using BrokerageAccounts.Data.Dapper.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BrokerageAccounts.Data.Dapper
{
    public static class IServiceCollectionExtensions
    {
        public static void RegisterEfCoreDataAccessClasses(this IServiceCollection services, String connectionString)
        {

            services.AddScoped<IStateRepository>((serviceProvider) => new StateRepository(connectionString));
            services.AddScoped<IClientRepository>((serviceProvider) => new ClientRepository(connectionString));


        }
    }
}
