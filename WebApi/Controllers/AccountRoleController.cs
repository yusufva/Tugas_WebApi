using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountRoleController : ControllerBase
    {
        private readonly IAccountRoleRepository _accountRoleRepository;

        public AccountRoleController(IAccountRoleRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }

        //Logic untuk Get AccountRole
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _accountRoleRepository.GetAll(); //mengambil semua data AccountRole
            if (!result.Any())
            {
                return NotFound("Data not found");
            }

            return Ok(result);
        }

        //Logic untuk Get AccountRole/{guid}
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _accountRoleRepository.GetByGuid(guid); //mengambil data AccountRole By Guid
            if (result is null)
            {
                return NotFound("Id not found");
            }

            return Ok(result);
        }

        //Logic untuk Post AccountRole/
        [HttpPost]
        public IActionResult Insert(AccountRole accountRole)
        {
            var result = _accountRoleRepository.Create(accountRole); //melakukan Create AccountRole
            if (result is null)
            {
                return BadRequest("Failed to Create Data");
            }

            return Ok(result);
        }

        //Logic untuk PUT AccountRole
        [HttpPut]
        public IActionResult Update(AccountRole accountRole)
        {
            var result = _accountRoleRepository.Update(accountRole); //melakukan update AccountRole
            if (!result)
            {
                return BadRequest("Failed to Update Data");
            }

            return Ok("Data has been Updated");
        }

        //Logic untuk Delete AccountRole
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var accountRole = _accountRoleRepository.GetByGuid(guid); //mengambil role by GUID
            var result = _accountRoleRepository.Delete(accountRole); //melakukan Delete AccountRole
            if (!result)
            {
                return BadRequest("Id not found");
            }

            return Ok("AccountRole has been deleted");
        }
    }
}
