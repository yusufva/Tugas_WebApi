using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.DTOs.Account;
using WebApi.DTOs.Employees;
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
        private readonly IUniversityRepository _universityRepository;
        private readonly IEmailHandler _emailHandler;

        public AccountsController(IAccountsRepository accountsRepository, IEmployeeRepository employeeRepository, IUniversityRepository universityRepository)
        {
            _accountsRepository = accountsRepository;
            _employeeRepository = employeeRepository;
            _universityRepository = universityRepository;
        }

        [HttpPut("change-password")]
        public IActionResult ChangePassword(ChangePasswordRequestDto changePasswordRequest)
        {
            var employee = _employeeRepository.GetByEmail(changePasswordRequest.Email); //mengambil data employee berdasar email
            if (employee == null)
            {
                return NotFound(new ResponseNotFoundHandler("data not found"));
            }

            var account = _accountsRepository.GetByGuid(employee.Guid); //mengambil data accoun berdasar employee guid
            if (account == null)
            {
                return NotFound(new ResponseNotFoundHandler("account not found"));
            }

            if (changePasswordRequest.Otp != account.Otp) //mengecek kecocokan otp
            {
                return BadRequest(new ResponseBadRequestHandler("OTP Error", "OTP does not match"));
            }
            if (account.IsUsed == true) //mengecek otp sudah digunakan atau belum
            {
                return BadRequest(new ResponseBadRequestHandler("OTP Error", "This OTP has been used"));
            }
            if (DateTime.Now > account.ExpiredTime) //mengecek apakah otp sudah expired
            {
                return BadRequest(new ResponseBadRequestHandler("OTP Error", "This OTP has been Expired"));
            }

            var toUpdate = new Accounts();
            toUpdate.Guid = account.Guid;
            toUpdate.Password = HashHandler.HashPassword(changePasswordRequest.NewPassword); //melakukan inject password baru
            toUpdate.Otp = account.Otp;
            toUpdate.IsUsed = true;
            toUpdate.ExpiredTime = account.ExpiredTime;
            toUpdate.CreatedDate = account.CreatedDate;
            toUpdate.ModifiedDate = DateTime.Now;

            _accountsRepository.Update(toUpdate); //melakukan update accounts

            return Ok(new ResponseOkHandler<ChangePasswordRequestDto>("Password has been changed"));
        }

        [HttpPost("register")]
        public IActionResult Register(AccountRegisterRequestDto accountRegisterRequest)
        {
            try
            {
                var university = _universityRepository.GetByCode(accountRegisterRequest.UniversityCode); //mengambil data university by code
                var isValid = true; //defaul value isValid = true
                if (university is null) //jika tidak ditemukan university
                {
                    isValid = false; //isvalid menjadi false
                }

                var register = _accountsRepository.Register(accountRegisterRequest, isValid); //memanggil function register pada account repo

                return Ok(new ResponseOkHandler<AccountRegisterRequestDto>(register, "Account Creation Success")); //mengembalikan message register success
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Register employee", ex.Message)); //mengembalikan error jika account creation fail
            }
        }


        [HttpPost("login")]
        public IActionResult Login(EmployeeLoginDto employeeLogin)
        {
            var employee = _employeeRepository.GetByEmail(employeeLogin.Email); //mengambil data employee berdasar email
            if (employee == null)
            {
                return NotFound(new ResponseNotFoundHandler("Account or Password is invalid")); //response jika employee tidak ditemukan
            }

            var account = _accountsRepository.GetByGuid(employee.Guid); //mengambil data account berdasar employee guid
            if (account == null)
            {
                return NotFound(new ResponseNotFoundHandler("Account does not Valid")); //response jika account tidak ditemukan
            }

            var isValid = HashHandler.ValidatePassword(employeeLogin.Password, account.Password); //melakukan validasi password yang diinput dengan yang di database
            if (!isValid) //jika false
            {
                return BadRequest(new ResponseBadRequestHandler("Login Error", "Account or Password is invalid")); //response jika password salah
            }

            //Token Handler menunggu diajarkan

            return Ok(new ResponseOkHandler<EmployeeLoginDto>("User successfully Logged in")); //respponse ketika user berhasil login
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
