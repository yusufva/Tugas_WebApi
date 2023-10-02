using WebApi.Models;

namespace WebApi.DTOs.Universities
{
    public class CreateUniversityDto
    {
        public string Name { get; set; } //deklarasi property
        public string Code { get; set; } //deklarasi property

        public static implicit operator University(CreateUniversityDto createUniversityDto) //implementasi implicit Operator
        {
            return new University
            {
                Code = createUniversityDto.Code,
                Name = createUniversityDto.Name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }

    }
}
