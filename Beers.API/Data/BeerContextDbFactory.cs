using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Beers.API.Data
{
    public class TaskContextDbFactory : IDesignTimeDbContextFactory<BeerContext>
    {
        public BeerContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<BeerContext>();

            var connectionString = configuration.GetValue<string>("ConnectionString");

            builder.UseSqlServer(connectionString);

            return new BeerContext(builder.Options);
        }
    }
}
