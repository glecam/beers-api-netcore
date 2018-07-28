using Beers.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Beers.API.Data
{
    public class BeerContext : DbContext
    {
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Brewery> Breweries { get; set; }

        public BeerContext(DbContextOptions<BeerContext> options) : base(options)
        {

        }
    }
}
