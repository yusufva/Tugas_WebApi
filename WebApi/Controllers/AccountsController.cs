using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsRepository _accountsRepository;

        public AccountsController(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        //Logic untuk Get Accounts
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _accountsRepository.GetAll(); //mengambil semua data Accounts
            if (!result.Any())
            {
                return NotFound("Data not found");
            }

            return Ok(result);
        }

        //Logic untuk Get Accounts/{guid}
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _accountsRepository.GetByGuid(guid); //mengambil data Accounts By Guid
            if (result is null)
            {
                return NotFound("Id not found");
            }

            return Ok(result);
        }

        //Logic untuk Post Accounts/
        [HttpPost]
        public IActionResult Insert(Accounts accounts)
        {
            var result = _accountsRepository.Create(accounts); //melakukan Create Accounts
            if (result is null)
            {
                return BadRequest("Failed to Create Data");
            }

            return Ok(result);
        }

        //Logic untuk PUT Accounts
        [HttpPut]
        public IActionResult Update(Accounts accounts)
        {
            var result = _accountsRepository.Update(accounts); //melakukan update Accounts
            if (!result)
            {
                return BadRequest("Failed to Update Data");
            }

            return Ok("Data has been Updated");
        }

        //Logic untuk Delete Accounts
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var accounts = _accountsRepository.GetByGuid(guid); //mengambil accounts by GUID
            var result = _accountsRepository.Delete(accounts); //melakukan Delete Accounts
            if (!result)
            {
                return BadRequest("Id not found");
            }

            return Ok("Accounts has been deleted");
        }
    }
}
