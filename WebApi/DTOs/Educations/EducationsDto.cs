using WebApi.Models;

namespace WebApi.DTOs.Educations
{
    public class EducationsDto : GeneralDto
    {
        public Guid UniversityGuid { get; set; } //deklarasi property
        public string Major {  get; set; } //deklarasi property
        public string Degree { get; set; } //deklarasi property
        public float Gpa { get; set; } //deklarasi property

        public static explicit operator EducationsDto(Education education) //implementasi explicit Operator
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

        public static implicit operator Education(EducationsDto educationDto) //implementasi implicit Operator
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
