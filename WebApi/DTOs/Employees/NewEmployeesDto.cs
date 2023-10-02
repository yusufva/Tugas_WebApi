using WebApi.Models;
using WebApi.Utilities.Enums;

namespace WebApi.DTOs.Employees
{
    public class NewEmployeesDto
    {
        public string Nik { get; set; } //deklarasi property
        public string FirstName { get; set; } //deklarasi property
        public string? LastName { get; set; } //deklarasi property
        public DateTime BirthDate { get; set; } //deklarasi property
        public GenderLevel Gender { get; set; } //deklarasi property
        public DateTime HiringDate { get; set; } //deklarasi property
        public string Email { get; set; } //deklarasi property
        public string PhoneNumber { get; set; } //deklarasi property

        public static implicit operator Employee(NewEmployeesDto dto) //implementasi implicit Operator
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
