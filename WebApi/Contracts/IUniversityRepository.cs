using WebApi.Models;

namespace WebApi.Contracts
{
    //contract interface University
    public interface IUniversityRepository : IGeneralRepository<University>
    {
        public University? GetByCode(string code);
    }
}
