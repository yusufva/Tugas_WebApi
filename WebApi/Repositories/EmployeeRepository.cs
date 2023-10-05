using WebApi.Contracts;
using WebApi.Data;
using WebApi.DTOs.Account;
using WebApi.DTOs.AccountRoles;
using WebApi.DTOs.Educations;
using WebApi.DTOs.Employees;
using WebApi.DTOs.Universities;
using WebApi.Models;
using WebApi.Utilities.Handler;

namespace WebApi.Repositories
{
    public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
    {
        private readonly GenerateHandler _generateHandler;
        public EmployeeRepository(BookingManagementDbContext context, GenerateHandler generateHandler) : base(context)
        {
            _generateHandler = generateHandler;
        }

        public Employee? GetByEmail(string email)
        {
            var employee = _context.Set<Employee>().FirstOrDefault(e => e.Email == email);

            return employee;
        }

        public EmployeeRegisterRequestDto? Register(EmployeeRegisterRequestDto request, bool isValid)
        {
            var transaction = _context.Database.BeginTransaction();
            try
            {
                if (!isValid)
                {
                    var university = new CreateUniversityDto();
                    university.Name = request.UniversityName;
                    university.Code = request.UniversityCode;
                    _context.Set<University>().Add(university);
                    _context.SaveChanges();
                }
                var validUniversity = _context.Set<University>().Where(u => u.Code == request.UniversityCode).FirstOrDefault();
                _context.ChangeTracker.Clear();

                var nik = _generateHandler.GenerateNIK();
                var employee = new Employee();
                employee.Nik = nik;
                employee.FirstName = request.FirstName;
                employee.LastName = request.LastName;
                employee.BirthDate = request.BirthDate;
                employee.Gender = request.Gender;
                employee.HiringDate = request.HiringDate;
                employee.Email = request.Email;
                employee.PhoneNumber = request.PhoneNumber;
                employee.CreatedDate = DateTime.Now;
                employee.ModifiedDate = DateTime.Now;

                _context.Set<Employee>().Add(employee);
                _context.SaveChanges();

                var newEmployee = _context.Set<Employee>().Where(e => e.Nik == employee.Nik).FirstOrDefault();
                _context.ChangeTracker.Clear();

                var account = new Accounts();
                account.Guid = newEmployee.Guid;
                account.Password = request.Password;
                account.Otp = 0; 
                account.IsUsed = true;
                account.ExpiredTime = DateTime.Now;
                account.CreatedDate = DateTime.Now;
                account.ModifiedDate = DateTime.Now;
                _context.Set<Accounts>().Add(account);
                _context.SaveChanges();

                var education = new NewEducationsDto();
                education.Guid = newEmployee.Guid;
                education.UniversityGuid = validUniversity.Guid;
                education.Major = request.Major;
                education.Degree = request.Degree;
                education.Gpa = request.GPA;
                _context.Set<Education>().Add(education);
                _context.SaveChanges();

                transaction.Commit();
                return request;
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
