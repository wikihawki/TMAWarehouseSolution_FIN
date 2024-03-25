using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T>(IGenericRepositoryInterface<T> generciRepositoryInterface) : 
    Controller where T : class
    {

        [HttpGet("all")]
        public async Task<IActionResult> GetAll() => Ok(await generciRepositoryInterface.GetAll());

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("Invalid request sent");
            return Ok(await generciRepositoryInterface.DeleteById(id));
        }

        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest("Invalid request sent");
            return Ok(await generciRepositoryInterface.GetById(id));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(T model)
        {
            if(model == null) return BadRequest("Bad request");
            return Ok(await generciRepositoryInterface.Insert(model));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(T model)
        {
            if (model == null) return BadRequest("Bad request");
            return Ok(await generciRepositoryInterface.Update(model));
        }
    }
}
