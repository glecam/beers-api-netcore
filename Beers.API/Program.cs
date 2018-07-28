using Beers.API.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Beers.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args).Seed();
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
