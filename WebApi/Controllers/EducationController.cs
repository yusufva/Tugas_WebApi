using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.DTOs.Educations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly IEducationRepository _educationRepository;

        public EducationController(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }

        //Logic untuk Get Education
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _educationRepository.GetAll(); //mengambil semua data Education
            if (!result.Any())
            {
                return NotFound("Data not found");
            }

            var data = result.Select(x=>(EducationsDto)x);

            return Ok(data);
        }

        //Logic untuk Get Education/{guid}
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _educationRepository.GetByGuid(guid); //mengambil data Education By Guid
            if (result is null)
            {
                return NotFound("Id not found");
            }

            return Ok((EducationsDto)result);
        }

        //Logic untuk Post Education/
        [HttpPost]
        public IActionResult Insert(NewEducationsDto newEducationsDto)
        {
            var result = _educationRepository.Create(newEducationsDto); //melakukan Create Education
            if (result is null)
            {
                return BadRequest("Failed to Create Data");
            }

            return Ok((EducationsDto) result);
        }

        //Logic untuk PUT Education
        [HttpPut]
        public IActionResult Update(EducationsDto educationsDto)
        {
            var entity = _educationRepository.GetByGuid(educationsDto.Guid);
            if (entity is null)
            {
                return NotFound("Id not Found");
            }

            var result = _educationRepository.Update(educationsDto); //melakukan update Education
            if (!result)
            {
                return BadRequest("Failed to Update Data");
            }

            return Ok("Data has been Updated");
        }

        //Logic untuk Delete Education
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var education = _educationRepository.GetByGuid(guid); //mengambil education by GUID
            if (education is null)
            {
                return NotFound("Id not Found");
            }

            var result = _educationRepository.Delete(education); //melakukan Delete Education
            if (!result)
            {
                return BadRequest("Id not found");
            }

            return Ok("Education has been deleted");
        }
    }
}
