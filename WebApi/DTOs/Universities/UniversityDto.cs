using WebApi.Models;

namespace WebApi.DTOs.Universities
{
    public class UniversityDto : GeneralDto
    {
        public string Name { get; set; } //deklarasi property
        public string Code { get; set; } //deklarasi property

        public static explicit operator UniversityDto(University university) //implementasi explicit Operator
        {
            return new UniversityDto { Guid = university.Guid, Name = university.Name, Code = university.Code };
        }

        public static implicit operator University(UniversityDto universityDto) //implementasi implicit Operator
        {
            return new University { Code = universityDto.Code, Name = universityDto.Name, Guid = universityDto.Guid, ModifiedDate = DateTime.Now };
        }
    }
}
