using WebApi.Models;

namespace WebApi.DTOs.Educations
{
    public class NewEducationsDto
    {
        public Guid Guid { get; set; } //deklarasi property
        public Guid UniversityGuid { get; set; } //deklarasi property
        public string Major { get; set; } //deklarasi property
        public string Degree { get; set; } //deklarasi property
        public float Gpa { get; set; } //deklarasi property

        public static implicit operator Education(NewEducationsDto dto) //implementasi implicit Operator
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
