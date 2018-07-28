using System.Linq;
using System.Threading.Tasks;
using Beers.API.Data;
using Beers.API.Exceptions;
using Beers.API.Models;
using Beers.API.Repositories.Interfaces;
using Beers.API.ViewModels;
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

        public async Task<Beer> Update(int id, BeerDto beer)
        {
            var persistedBeer = await GetById(id);
            if (persistedBeer == null) return null;

            if (beer.Rating.HasValue)
                persistedBeer.Rating = beer.Rating.Value;

            _context.Beers.Update(persistedBeer);

            await _context.SaveChangesAsync();

            return persistedBeer;
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
