using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.DTOs.Universities;
using WebApi.Utilities.Handler;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
                return NotFound(new ResponseNotFoundHandler("Data not found"));
            }

            var data = result.Select(x => (UniversityDto)x);

            return Ok(new ResponseOkHandler<IEnumerable<UniversityDto>>(data, "Data retrieve Successfully"));
        }

        //Logic untuk Get University/{guid}
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid) { 
            var result = _universityRepository.GetByGuid(guid); //mengambil data University By Guid
            if(result is null)
            {
                return NotFound(new ResponseNotFoundHandler("Id not found"));
            }

            return Ok(new ResponseOkHandler<UniversityDto>((UniversityDto)result, "Data retrieve Successfully"));
        }

        //Logic untuk Post University/
        [HttpPost]
        public IActionResult Insert(CreateUniversityDto universityDto)
        {
            try
            {
                var result = _universityRepository.Create(universityDto); //melakukan Create University
                if(result is null)
                {
                    return BadRequest("Failed to Create Data");
                }

                return Ok(new ResponseOkHandler<UniversityDto>((UniversityDto)result, "Insert Success"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("failed to Create Data", ex.Message)); //error pada repository
            }
        }

        //Logic untuk PUT University
        [HttpPut]
        public IActionResult Update(UniversityDto universityDto)
        {
            try
            {
                var entity = _universityRepository.GetByGuid(universityDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Id not found"));
                }

                _universityRepository.Update(universityDto); //melakukan update University

                return Ok(new ResponseOkHandler<UniversityDto>("Data has been Updated"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Update Data", ex.Message)); //error pada repository
            }
        }

        //Logic untuk Delete University
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var university = _universityRepository.GetByGuid(guid); //mengambil university by GUID
                if (university is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Id not found"));
                }

                _universityRepository.Delete(university); //melakukan Delete University
                return Ok(new ResponseOkHandler<UniversityDto>("Data has been Deleted"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Delete Data", ex.Message)); //error pada repository
            }
        }
    }
}
