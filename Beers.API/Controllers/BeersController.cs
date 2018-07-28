using System.Threading.Tasks;
using Beers.API.Repositories.Interfaces;
using Beers.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Beers.API.Controllers
{
    [Route("api/[controller]")]
    public class BeersController : Controller
    {
        private readonly IBeerRepository _repository;

        public BeersController(IBeerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _repository.Getall();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _repository.GetById(id);
            if (data == null) return NotFound();

            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]BeerDto beer)
        {
            var updatedBeer = await _repository.Update(id, beer);
            if (updatedBeer == null) return NotFound();

            return Ok(updatedBeer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.Delete(id);
            return Ok();
        }
    }
}
