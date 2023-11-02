using WebApi.DTOs.Educations;

namespace Client.Contracts
{
    public interface IEducationRepository : IRepository<EducationsDto, NewEducationsDto, Guid>
    {
    }
}
