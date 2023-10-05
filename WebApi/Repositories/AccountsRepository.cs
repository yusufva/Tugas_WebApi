using WebApi.Contracts;
using WebApi.Data;
using WebApi.DTOs.Account;
using WebApi.DTOs.Educations;
using WebApi.DTOs.Universities;
using WebApi.Models;
using WebApi.Utilities.Handler;

namespace WebApi.Repositories
{
    public class AccountsRepository : GeneralRepository<Accounts>, IAccountsRepository
    {
        private readonly GenerateHandler _generateHandler;
        public AccountsRepository(BookingManagementDbContext context, GenerateHandler generateHandler) : base(context)
        {
            _generateHandler = generateHandler;
        }

        public AccountRegisterRequestDto? Register(AccountRegisterRequestDto request, bool isValid)
        {
            var transaction = _context.Database.BeginTransaction(); //melakukan inisiasi transaction
            try
            {
                if (!isValid) //if university tidak ada
                {
                    var university = new CreateUniversityDto();
                    university.Name = request.UniversityName;
                    university.Code = request.UniversityCode;
                    _context.Set<University>().Add(university); //melakukan input university baru
                    _context.SaveChanges();
                }
                var validUniversity = _context.Set<University>().Where(u => u.Code == request.UniversityCode).FirstOrDefault(); //mengambil data university sesuai code
                _context.ChangeTracker.Clear(); //melakukan clear changetracker

                var nik = _generateHandler.GenerateNIK(); //melakukan generate nik
                var employee = new Employee(); //melakukan inject data baru ke dalam objek employee
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

                _context.Set<Employee>().Add(employee); //melakukan add employee baru ke database
                _context.SaveChanges(); //melakukan save kedalam database

                var newEmployee = _context.Set<Employee>().Where(e => e.Nik == employee.Nik).FirstOrDefault(); //mengambil employee baru berdasar Nik yang baru
                _context.ChangeTracker.Clear(); //melakukan clear changetracker

                var account = new Accounts(); //melakukan inject data baru ke dalam object account
                account.Guid = newEmployee.Guid;
                account.Password = HashHandler.HashPassword(request.Password); //melakukan hash password yang akan disimpan
                account.Otp = 0;
                account.IsUsed = true;
                account.ExpiredTime = DateTime.Now;
                account.CreatedDate = DateTime.Now;
                account.ModifiedDate = DateTime.Now;
                _context.Set<Accounts>().Add(account); //melakukan add account baru ke database
                _context.SaveChanges(); //melakukan save kedalam database

                var education = new NewEducationsDto(); //melakukan inject data baru ke dalam object educations dto
                education.Guid = newEmployee.Guid;
                education.UniversityGuid = validUniversity.Guid; //memasukkan university guid dari university yang valid
                education.Major = request.Major;
                education.Degree = request.Degree;
                education.Gpa = request.GPA;
                _context.Set<Education>().Add(education); //melakukan add account baru ke database
                _context.SaveChanges(); //melakukan save kedalam database

                transaction.Commit(); //melakukan commit transaction setelah semua berhasil
                return request; //mengembalikan data request
            }
            catch (Exception ex)
            {
                transaction.Rollback(); //melakukan rollback transaction ketika transaction gagal
                throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
