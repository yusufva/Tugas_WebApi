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
                return NotFound("Data not found");
            }

            var data = result.Select(x => (EmployeesDto)x);

            return Ok(data);
        }

        //Logic untuk Get Employee/{guid}
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _employeeRepository.GetByGuid(guid); //mengambil data Employee By Guid
            if (result is null)
            {
                return NotFound("Id not found");
            }

            return Ok((EmployeesDto)result);
        }

        //Logic untuk Post Employee/
        [HttpPost]
        public IActionResult Insert(NewEmployeesDto newEmployeesDto)
        {
            var nik = _generateHandler.GenerateNIK();
            Employee toInsert = newEmployeesDto;
            toInsert.Nik = nik;

            var result = _employeeRepository.Create(toInsert); //melakukan Create Employee
            if (result is null)
            {
                return BadRequest("Failed to Create Data");
            }

            return Ok((EmployeesDto)result);
        }

        //Logic untuk PUT Employee
        [HttpPut]
        public IActionResult Update(EmployeesDto employeesDto)
        {
            var entity = _employeeRepository.GetByGuid(employeesDto.Guid);
            if (entity is null)
            {
                return NotFound("Id not Found");
            }

            var result = _employeeRepository.Update(employeesDto); //melakukan update Employee
            if (!result)
            {
                return BadRequest("Failed to Update Data");
            }

            return Ok("Data has been Updated");
        }

        //Logic untuk Delete Employee
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var employee = _employeeRepository.GetByGuid(guid); //mengambil employee by GUID
            if (employee is null)
            {
                return NotFound("Id not Found");
            }

            var result = _employeeRepository.Delete(employee); //melakukan Delete Employee
            if (!result)
            {
                return BadRequest("Id not found");
            }

            return Ok("Employee has been deleted");
        }
    }
}
