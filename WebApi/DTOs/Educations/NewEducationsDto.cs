using WebApi.Models;

namespace WebApi.DTOs.Educations
{
    public class NewEducationsDto
    {
        public Guid Guid { get; set; }
        public Guid UniversityGuid { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public float Gpa { get; set; }

        public static implicit operator Education(NewEducationsDto dto)
        {
            return new Education
            {
                Guid = dto.Guid,
                Major = dto.Major,
                Degree = dto.Degree,
                Gpa = dto.Gpa,
                UniversityGuid = dto.UniversityGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
