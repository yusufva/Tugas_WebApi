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

        //Logic untuk Get University
        [HttpGet]
        public IActionResult GetAll() {
            var result = _universityRepository.GetAll(); //mengambil semua data University
            if (!result.Any())
            {
                return NotFound("Data not found");
            }

            return Ok(result);
        }

        //Logic untuk Get University/{guid}
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid) { 
            var result = _universityRepository.GetByGuid(guid); //mengambil data University By Guid
            if(result is null)
            {
                return NotFound("Id not found");
            }

            return Ok(result);
        }

        //Logic untuk Post University/
        [HttpPost]
        public IActionResult Insert(University university)
        {
            var result = _universityRepository.Create(university); //melakukan Create University
            if(result is null)
            {
                return BadRequest("Failed to Create Data");
            }

            return Ok(result);
        }

        //Logic untuk PUT University
        [HttpPut]
        public IActionResult Update(University university)
        {
            var result = _universityRepository.Update(university); //melakukan update University
            if(!result)
            {
                return BadRequest("Failed to Update Data");
            }

            return Ok("Data has been Updated");
        }

        //Logic untuk Delete University
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var university = _universityRepository.GetByGuid(guid); //mengambil university by GUID
            var result = _universityRepository.Delete(university); //melakukan Delete University
            if (!result)
            {
                return BadRequest("Id not found");
            }

            return Ok("University has been deleted");
        }
    }
}
