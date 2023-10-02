using WebApi.Models;

namespace WebApi.DTOs.Educations
{
    public class EducationsDto : GeneralDto
    {
        public Guid UniversityGuid { get; set; }
        public string Major {  get; set; }
        public string Degree { get; set; }
        public float Gpa { get; set; }

        public static explicit operator EducationsDto(Education education)
        {
            return new EducationsDto
            {
                Guid = education.Guid,
                Major = education.Major,
                Degree = education.Degree,
                Gpa = education.Gpa,
                UniversityGuid = education.UniversityGuid,
            };
        }

        public static implicit operator Education(EducationsDto educationDto)
        {
            return new Education
            {
                Guid = educationDto.Guid,
                Major = educationDto.Major,
                Degree = educationDto.Degree,
                Gpa = educationDto.Gpa,
                UniversityGuid = educationDto.UniversityGuid,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
