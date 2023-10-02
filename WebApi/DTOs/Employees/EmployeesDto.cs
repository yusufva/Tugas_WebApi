﻿using WebApi.Models;
using WebApi.Utilities.Enums;

namespace WebApi.DTOs.Employees
{
    public class EmployeesDto : GeneralDto
    {
        public string Nik { get; set; } //deklarasi property
        public string FirstName { get; set; } //deklarasi property
        public string? LastName { get; set; } //deklarasi property
        public DateTime BirthDate { get; set; } //deklarasi property
        public GenderLevel Gender { get; set; } //deklarasi property
        public DateTime HiringDate { get; set; } //deklarasi property
        public string Email { get; set; } //deklarasi property
        public string PhoneNumber { get; set; } //deklarasi property

        public static explicit operator EmployeesDto(Employee employee) //implementasi explicit Operator
        {
            return new EmployeesDto
            {
                Guid = employee.Guid,
                Nik = employee.Nik,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthDate = employee.BirthDate,
                Gender = employee.Gender,
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
            };
        }

        public static implicit operator Employee(EmployeesDto employeeDto) //implementasi implicit Operator
        {
            return new Employee
            {
                Guid = employeeDto.Guid,
                Nik = employeeDto.Nik,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                BirthDate = employeeDto.BirthDate,
                Gender = employeeDto.Gender,
                HiringDate = employeeDto.HiringDate,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
