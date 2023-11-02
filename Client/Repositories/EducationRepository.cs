using Client.Contracts;
using WebApi.DTOs.Educations;
using WebApi.DTOs.Employees;
using WebApi.Models;

namespace Client.Repositories
{
    public class EducationRepository : GeneralRepository<EducationsDto, NewEducationsDto, Guid>, IEducationRepository
    {
        public EducationRepository(string request = "Education/") : base(request)
        {
        }
    }
}
