using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.DTOs.Employees;
using WebApi.Models;
using WebApi.Utilities.Handler;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly GenerateHandler _generateHandler;

        public EmployeeController(IEmployeeRepository employeeRepository, GenerateHandler generateHandler)
        {
            _employeeRepository = employeeRepository;
            _generateHandler = generateHandler;
        }

        //Logic untuk Get Employee
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _employeeRepository.GetAll(); //mengambil semua data Employee
            if (!result.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data not found"));
            }

            var data = result.Select(x => (EmployeesDto)x);

            return Ok(new ResponseOkHandler<IEnumerable<EmployeesDto>>(data, "Data retrieve Successfully"));
        }

        //Logic untuk Get Employee/{guid}
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _employeeRepository.GetByGuid(guid); //mengambil data Employee By Guid
            if (result is null)
            {
                return NotFound(new ResponseNotFoundHandler("Id not found"));
            }

            return Ok(new ResponseOkHandler<EmployeesDto>((EmployeesDto)result, "Data retrieve Successfully"));
        }

        //Logic untuk Post Employee/
        [HttpPost]
        public IActionResult Insert(NewEmployeesDto newEmployeesDto)
        {
            try
            {
                var nik = _generateHandler.GenerateNIK(); //memanggil generateNIK
                Employee toInsert = newEmployeesDto; //mendefinisikan object yang akan di create
                toInsert.Nik = nik; //melakukan injeksi nik yang telah di generate ke object yang akan di create

                var result = _employeeRepository.Create(toInsert); //melakukan Create Employee
                
                return Ok(new ResponseOkHandler<EmployeesDto>((EmployeesDto)result,"Insert Success"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("failed to Create Data", ex.Message)); //error pada repository
            }
        }

        //Logic untuk PUT Employee
        [HttpPut]
        public IActionResult Update(EmployeesDto employeesDto)
        {
            try
            {
                var entity = _employeeRepository.GetByGuid(employeesDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Id not found"));
                }

                _employeeRepository.Update(employeesDto); //melakukan update Employee

                return Ok(new ResponseOkHandler<EmployeesDto>("Data has been Updated"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Update Data", ex.Message)); //error pada repository
            }
        }

        //Logic untuk Delete Employee
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var employee = _employeeRepository.GetByGuid(guid); //mengambil employee by GUID
                if (employee is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Id not found"));
                }

                _employeeRepository.Delete(employee); //melakukan Delete Employee

                return Ok(new ResponseOkHandler<EmployeesDto>("Data has been Deleted"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Delete Data", ex.Message)); //error pada repository
            }
        }
    }
}
