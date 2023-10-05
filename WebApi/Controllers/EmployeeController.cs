using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.DTOs.Employees;
using WebApi.DTOs.Universities;
using WebApi.Models;
using WebApi.Utilities.Handler;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly IAccountsRepository _accountsRepository;
        private readonly GenerateHandler _generateHandler;

        public EmployeeController(IEmployeeRepository employeeRepository, GenerateHandler generateHandler, IUniversityRepository universityRepository, IEducationRepository educationRepository, IAccountsRepository accountsRepository)
        {
            _employeeRepository = employeeRepository;
            _generateHandler = generateHandler;
            _universityRepository = universityRepository;
            _educationRepository = educationRepository;
            _accountsRepository = accountsRepository;
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

        [HttpGet("details")]
        public IActionResult GetDetail() {
            var employees = _employeeRepository.GetAll();
            var educations = _educationRepository.GetAll();
            var universities = _universityRepository.GetAll();

            if(!(employees.Any() &&  educations.Any() && universities.Any()))
            {
                return NotFound(new ResponseNotFoundHandler("Data not found"));
            }

            var employeeDetails = from emp in employees
                                  join edu in educations on emp.Guid equals edu.Guid
                                  join unv in universities on edu.UniversityGuid equals unv.Guid
                                  select new EmployeeDetailDto
                                  {
                                      Guid = emp.Guid,
                                      Nik = emp.Nik,
                                      FullName = string.Concat(emp.FirstName, " ", emp.LastName),
                                      BirthDate = emp.BirthDate,
                                      Gender = emp.Gender.ToString(),
                                      HiringDate = emp.HiringDate,
                                      Email = emp.Email,
                                      PhoneNumber = emp.PhoneNumber,
                                      Major = edu.Major,
                                      Degree = edu.Degree,
                                      Gpa = edu.Gpa,
                                      University = unv.Name
                                  };

            return Ok(new ResponseOkHandler<IEnumerable<EmployeeDetailDto>>(employeeDetails, "data retrieve successfully"));
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword(ForgotPasswordRequestDto forgotPasswordDto)
        {
            var employee = _employeeRepository.GetByEmail(forgotPasswordDto.Email);

            if (employee == null)
            {
                return NotFound(new ResponseNotFoundHandler("Data not found"));
            }

            int otp;
            int.TryParse(OtpHandler.GenerateRandomOtp(), out otp);

            var accounts = _accountsRepository.GetByGuid(employee.Guid);
            var otpUpdate = new Accounts();
            otpUpdate.Guid = employee.Guid;
            otpUpdate.Otp = otp;
            otpUpdate.IsUsed = false;
            otpUpdate.Password = accounts.Password;
            otpUpdate.CreatedDate = accounts.CreatedDate;
            otpUpdate.ModifiedDate = DateTime.Now;
            otpUpdate.ExpiredTime = DateTime.Now.AddMinutes(5);
            _accountsRepository.Update(otpUpdate);

            return Ok(new ResponseOkHandler<ForgotPasswordResponseDto>((ForgotPasswordResponseDto)otp, "password request has been send"));
        }

        [HttpPost("register")]
        public IActionResult Register(EmployeeRegisterRequestDto employeeRegisterRequest)
        {
            try
            {

            var university = _universityRepository.GetByCode(employeeRegisterRequest.UniversityCode);
            var isValid = true;
            if (university is null)
            {
                isValid = false;
            }

            var register = _employeeRepository.Register(employeeRegisterRequest, isValid);

            return Ok(new ResponseOkHandler<EmployeeRegisterRequestDto>(register, "Insert Success"));
            }
            catch (ExceptionHandler ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseInternalServerErrorHandler("Failed to Register employee", ex.Message));
            }
        }

        [HttpPost("login")]
        public IActionResult Login(EmployeeLoginDto employeeLogin)
        {
            var employee = _employeeRepository.GetByEmail(employeeLogin.Email);
            if(employee == null)
            {
                return NotFound(new ResponseNotFoundHandler("Account or Password is invalid"));
            }

            var account = _accountsRepository.GetByGuid(employee.Guid);
            if(account == null)
            {
                return NotFound(new ResponseNotFoundHandler("Account does not Valid"));
            }

            var isValid = HashHandler.ValidatePassword(employeeLogin.Password, account.Password);
            if(!isValid)
            {
                return BadRequest(new ResponseBadRequestHandler("Login Error", "Account or Password is invalid"));
            }

            //Token Handler

            return Ok(new ResponseOkHandler<EmployeeLoginDto>("User successfully Logged in"));
        }
    }
}
