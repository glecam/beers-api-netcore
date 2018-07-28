using System;
using System.Reflection;
using Beers.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Beers.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBeercontext(this IServiceCollection services, string connectionString)
        {
            services.AddTransient(serviceProvider =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<BeerContext>();
                optionsBuilder.UseSqlServer(connectionString,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(
                            typeof(Startup).GetTypeInfo().Assembly.GetName().Name);

                        //Configuring Connection Resiliency:
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);

                    });
              
                return new BeerContext(optionsBuilder.Options);
            });

            return services;
        }
    }
}
