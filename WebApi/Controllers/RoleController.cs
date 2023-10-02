using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRolesRepository _roleRepository;

        public RoleController(IRolesRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        //Logic untuk Get Role
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _roleRepository.GetAll(); //mengambil semua data Role
            if (!result.Any())
            {
                return NotFound("Data not found");
            }

            return Ok(result);
        }

        //Logic untuk Get Role/{guid}
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _roleRepository.GetByGuid(guid); //mengambil data Role By Guid
            if (result is null)
            {
                return NotFound("Id not found");
            }

            return Ok(result);
        }

        //Logic untuk Post Role/
        [HttpPost]
        public IActionResult Insert(Role role)
        {
            var result = _roleRepository.Create(role); //melakukan Create Role
            if (result is null)
            {
                return BadRequest("Failed to Create Data");
            }

            return Ok(result);
        }

        //Logic untuk PUT Role
        [HttpPut]
        public IActionResult Update(Role role)
        {
            var result = _roleRepository.Update(role); //melakukan update Role
            if (!result)
            {
                return BadRequest("Failed to Update Data");
            }

            return Ok("Data has been Updated");
        }

        //Logic untuk Delete Role
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var role = _roleRepository.GetByGuid(guid); //mengambil role by GUID
            var result = _roleRepository.Delete(role); //melakukan Delete Role
            if (!result)
            {
                return BadRequest("Id not found");
            }

            return Ok("Role has been deleted");
        }
    }
}
