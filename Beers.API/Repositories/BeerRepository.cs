using System.Linq;
using System.Threading.Tasks;
using Beers.API.Data;
using Beers.API.Exceptions;
using Beers.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Beers.API.Repositories
{
    public class BeerRepository : IBeerRepository
    {
        private readonly BeerContext _context;

        public BeerRepository(BeerContext context)
        {
            _context = context;
        }

        public IQueryable<Beer> Getall()
        {
            return _context.Beers.Include(x => x.Brewery).AsNoTracking();
        }

        public async Task<Beer> GetById(int id)
        {
            return await _context.Beers.Include(x => x.Brewery).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Beer> Create(Beer beer)
        {
            await _context.Beers.AddAsync(beer);
            await _context.SaveChangesAsync();
            return beer;
        }

        public async Task<Beer> Update(int id, Beer beer)
        {
            var persistedBeer = await GetById(id);
            if (persistedBeer == null) throw new NotFoundException();

            persistedBeer.BreweryId = beer.BreweryId;
            persistedBeer.Name = beer.Name;
            persistedBeer.Rating = beer.Rating;

            _context.Beers.Update(beer);

            await _context.SaveChangesAsync();

            return beer;
        }

        public async Task Delete(int id)
        {
            var beer = await GetById(id);
            if (beer == null) throw new NotFoundException();

            _context.Beers.Remove(beer);

            await _context.SaveChangesAsync();
        }
    }
}
