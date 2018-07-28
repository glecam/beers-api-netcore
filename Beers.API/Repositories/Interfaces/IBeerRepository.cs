using System.Linq;
using System.Threading.Tasks;
using Beers.API.Models;
using Beers.API.ViewModels;

namespace Beers.API.Repositories.Interfaces
{
    public interface IBeerRepository
    {
        IQueryable<Beer> Getall();
        Task<Beer> GetById(int id);
        Task<Beer> Create(Beer beer);
        Task<Beer> Update(int id, BeerDto beer);
        Task Delete(int id);
    }
}