using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Models;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityRepository _universityRepository;

        public UniversityController(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        [HttpGet]
        public IActionResult GetAll() {
            var result = _universityRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data not found");
            }

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid) { 
            var result = _universityRepository.GetByGuid(guid);
            if(result is null)
            {
                return NotFound("Id not found");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Insert(University university)
        {
            var result = _universityRepository.Create(university);
            if(result is null)
            {
                return BadRequest("Failed to Create Data");
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(University university)
        {
            var result = _universityRepository.Update(university);
            if(!result)
            {
                return BadRequest("Failed to Update Data");
            }

            return Ok("Data has been Updated");
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var university = _universityRepository.GetByGuid(guid);
            var result = _universityRepository.Delete(university);
            if (!result)
            {
                return BadRequest("Id not found");
            }

            return Ok("University has been deleted");
        }
    }
}
