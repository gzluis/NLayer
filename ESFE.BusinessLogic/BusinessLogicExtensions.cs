﻿using ESFE.DataAccess;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ESFE.BusinessLogic
{
    public static class BusinessLogicExtensions
    {
        public static IServiceCollection AddBusinessLogicServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMediatR(
                cfg => 
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
            );
            services.AddDataAccessServices(configuration);

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
