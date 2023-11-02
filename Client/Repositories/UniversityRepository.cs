using Client.Contracts;
using WebApi.DTOs.Employees;
using WebApi.DTOs.Universities;
using WebApi.Models;

namespace Client.Repositories
{
    public class UniversityRepository : GeneralRepository<UniversityDto, CreateUniversityDto, Guid>, IUniversityRepository
    {
        public UniversityRepository(string request = "University/") : base(request)
        {
        }
    }
}
