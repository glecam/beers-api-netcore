using System.Linq;
using System.Threading.Tasks;
using Beers.API.Models;

namespace Beers.API.Repositories
{
    public interface IBeerRepository
    {
        IQueryable<Beer> Getall();
        Task<Beer> GetById(int id);
        Task<Beer> Create(Beer beer);
        Task<Beer> Update(int id, Beer beer);
        Task Delete(int id);
    }
}