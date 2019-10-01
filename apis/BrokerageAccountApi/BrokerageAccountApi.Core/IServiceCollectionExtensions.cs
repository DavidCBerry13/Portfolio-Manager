using BrokerageAccountApi.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerageAccountApi.Core
{
    public static class IServiceCollectionExtensions
    {


        public static void RegisterServiceClasses(this IServiceCollection services)
        {

            services.AddScoped<IStateService, StateService>();
        }

    }
}
