using WebApi.DTOs.Universities;
using WebApi.Models;

namespace Client.Contracts
{
    public interface IUniversityRepository : IRepository<UniversityDto, CreateUniversityDto, Guid>
    {
    }
}
