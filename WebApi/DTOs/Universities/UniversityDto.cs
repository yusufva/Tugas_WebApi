using WebApi.Models;

namespace WebApi.DTOs.Universities
{
    public class UniversityDto : GeneralDto
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public static explicit operator UniversityDto(University university)
        {
            return new UniversityDto { Guid = university.Guid, Name = university.Name, Code = university.Code };
        }

        public static implicit operator University(UniversityDto universityDto)
        {
            return new University { Code = universityDto.Code, Name = universityDto.Name, Guid = universityDto.Guid, ModifiedDate = DateTime.Now };
        }
    }
}
