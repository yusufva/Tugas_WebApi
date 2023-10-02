using WebApi.Models;
using WebApi.Utilities.Enums;

namespace WebApi.DTOs.Employees
{
    public class NewEmployeesDto
    {
        public string Nik { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public static implicit operator Employee(NewEmployeesDto dto)
        {
            return new Employee
            {
                Nik = dto.Nik,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
                HiringDate = dto.HiringDate,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
