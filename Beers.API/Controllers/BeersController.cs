using System.Threading.Tasks;
using Beers.API.Repositories;
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

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.Delete(id);
            return Ok();
        }
    }
}
