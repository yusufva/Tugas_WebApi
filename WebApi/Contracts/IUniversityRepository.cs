using WebApi.Models;

namespace WebApi.Contracts
{
    //contract interface University
    public interface IUniversityRepository
    {
        IEnumerable<University> GetAll();
        University? GetByGuid(Guid guid);
        University? Create(University university);
        bool Update(University university);
        bool Delete(University university);
    }
}
