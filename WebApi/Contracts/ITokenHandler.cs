using System.Security.Claims;

namespace WebApi.Contracts
{
    public interface ITokenHandler
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}
