using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.DTOs.Educations;
using WebApi.DTOs.Employees;
using WebApi.Utilities.Handler;

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
                return NotFound(new ResponseNotFoundHandler("Data not found"));
            }

            var data = result.Select(x=>(EducationsDto)x);

            return Ok(new ResponseOkHandler<IEnumerable<EducationsDto>>(data, "Data retrieve Successfully"));
        }

        //Logic untuk Get Education/{guid}
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _educationRepository.GetByGuid(guid); //mengambil data Education By Guid
            if (result is null)
            {
                return NotFound(new ResponseNotFoundHandler("Id not found"));
            }

            return Ok(new ResponseOkHandler<EducationsDto>((EducationsDto)result, "Data retrieve Successfully"));
        }

        //Logic untuk Post Education/
        [HttpPost]
        public IActionResult Insert(NewEducationsDto newEducationsDto)
        {
            try
            {
                var result = _educationRepository.Create(newEducationsDto); //melakukan Create Education

                return Ok(new ResponseOkHandler<EducationsDto>((EducationsDto)result, "Insert Success"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("failed to Create Data", ex.Message)); //error pada repository
            }
        }

        //Logic untuk PUT Education
        [HttpPut]
        public IActionResult Update(EducationsDto educationsDto)
        {
            try
            {
                var entity = _educationRepository.GetByGuid(educationsDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Id not Found"));
                }

                _educationRepository.Update(educationsDto); //melakukan update Education

                return Ok(new ResponseOkHandler<EducationsDto>("Data has been Updated"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Update Data", ex.Message)); //error pada repository
            }
        }

        //Logic untuk Delete Education
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var education = _educationRepository.GetByGuid(guid); //mengambil education by GUID
                if (education is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Id not Found"));
                }

                _educationRepository.Delete(education); //melakukan Delete Education

                return Ok(new ResponseOkHandler<EducationsDto>("Data has been Deleted"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Delete Data", ex.Message)); //error pada repository
            }
        }
    }
}
