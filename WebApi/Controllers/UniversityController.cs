using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.DTOs.Universities;
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
            var result = _universityRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            var data = result.Select(x => (UniversityDto)x);

            return Ok(data);
        }

        //Logic untuk Get University/{guid}
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid) { 
            var result = _universityRepository.GetByGuid(guid); //mengambil data University By Guid
            if(result is null)
            {
                return NotFound("Id not found");
            }

            return Ok((UniversityDto)result);
        }

        //Logic untuk Post University/
        [HttpPost]
        public IActionResult Insert(CreateUniversityDto universityDto)
        {
            var result = _universityRepository.Create(universityDto); //melakukan Create University
            if(result is null)
            {
                return BadRequest("Failed to Create Data");
            }

            return Ok((UniversityDto)result);
        }

        //Logic untuk PUT University
        [HttpPut]
        public IActionResult Update(UniversityDto universityDto)
        {
            var entity = _universityRepository.GetByGuid(universityDto.Guid);
            if (entity is null)
            {
                return NotFound("Id not Found");
            }

            var result = _universityRepository.Update(universityDto); //melakukan update University
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
            if (university is null)
            {
                return NotFound("Id not Found");
            }

            var result = _universityRepository.Delete(university); //melakukan Delete University
            if (!result)
            {
                return BadRequest("Id not found");
            }

            return Ok("University has been deleted");
        }
    }
}
