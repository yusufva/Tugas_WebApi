using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.DTOs.Account;
using WebApi.Models;
using WebApi.Utilities.Handler;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public AccountsController(IAccountsRepository accountsRepository, IEmployeeRepository employeeRepository)
        {
            _accountsRepository = accountsRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpPost("change-password")]
        public IActionResult ChangePassword(ChangePasswordRequestDto changePasswordRequest)
        {
            var employee = _employeeRepository.GetByEmail(changePasswordRequest.Email);
            if (employee == null)
            {
                return NotFound(new ResponseNotFoundHandler("data not found"));
            }

            var account = _accountsRepository.GetByGuid(employee.Guid);
            if (account == null)
            {
                return NotFound(new ResponseNotFoundHandler("account not found"));
            }

            if (changePasswordRequest.Otp != account.Otp)
            {
                return BadRequest(new ResponseBadRequestHandler("OTP Error", "OTP does not match"));
            }
            if (account.IsUsed == true)
            {
                return BadRequest(new ResponseBadRequestHandler("OTP Error", "This OTP has been used"));
            }
            if (DateTime.Now > account.ExpiredTime)
            {
                return BadRequest(new ResponseBadRequestHandler("OTP Error", "This OTP has been Expired"));
            }

            var toUpdate = new Accounts();
            toUpdate.Guid = account.Guid;
            toUpdate.Password = HashHandler.HashPassword(changePasswordRequest.NewPassword);
            toUpdate.Otp = account.Otp;
            toUpdate.IsUsed = true;
            toUpdate.ExpiredTime = account.ExpiredTime;
            toUpdate.CreatedDate = account.CreatedDate;
            toUpdate.ModifiedDate = DateTime.Now;

            _accountsRepository.Update(toUpdate);

            return Ok(new ResponseOkHandler<ChangePasswordRequestDto>("Password has been changed"));
        }

        //Logic untuk Get Accounts
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _accountsRepository.GetAll(); //mengambil semua data Accounts
            if (!result.Any())
            {
                return NotFound(new ResponseNotFoundHandler("Data not found"));
            }

            var data = result.Select(x => (AccountsDto)x);

            return Ok(new ResponseOkHandler<IEnumerable<AccountsDto>>(data, "Data retrieve Successfully"));
        }

        //Logic untuk Get Accounts/{guid}
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _accountsRepository.GetByGuid(guid); //mengambil data Accounts By Guid
            if (result is null)
            {
                return NotFound(new ResponseNotFoundHandler("Data not found"));
            }

            return Ok(new ResponseOkHandler<AccountsDto>((AccountsDto)result, "Data retrieve Successfully"));
        }

        //Logic untuk Post Accounts/
        [HttpPost]
        public IActionResult Insert(NewAccountsDto newAccountsDto)
        {
            try
            {
                newAccountsDto.Password = HashHandler.HashPassword(newAccountsDto.Password);
                var result = _accountsRepository.Create(newAccountsDto); //melakukan Create Accounts
                if (result is null)
                {
                    return BadRequest("Failed to Create Data");
                }

                return Ok(new ResponseOkHandler<AccountsDto>((AccountsDto)result, "Insert Success"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("failed to Create Data", ex.Message)); //error pada repository
            }
        }

        //Logic untuk PUT Accounts
        [HttpPut]
        public IActionResult Update(AccountsDto accountsDto)
        {
            try
            {
                var entity = _accountsRepository.GetByGuid(accountsDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Data not found"));
                }

                Accounts toUpdate = accountsDto;
                toUpdate.CreatedDate = entity.CreatedDate;
                toUpdate.ModifiedDate = DateTime.Now;
                toUpdate.Password = HashHandler.HashPassword(accountsDto.Password);

                _accountsRepository.Update(toUpdate); //melakukan update Accounts

                return Ok(new ResponseOkHandler<AccountsDto>("Data has been Updated"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Update Data", ex.Message)); //error pada repository
            }
        }

        //Logic untuk Delete Accounts
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var accounts = _accountsRepository.GetByGuid(guid); //mengambil accounts by GUID
                if (accounts is null)
                {
                    return NotFound(new ResponseNotFoundHandler("Data not found"));
                }

                _accountsRepository.Delete(accounts); //melakukan Delete Accounts

                return Ok(new ResponseOkHandler<AccountsDto>("Data has been Deleted"));

            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Delete Data", ex.Message)); //error pada repository
            }
        }
    }
}
