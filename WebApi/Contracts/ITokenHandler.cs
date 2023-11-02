using System.Security.Claims;
using WebApi.DTOs.Account;

namespace WebApi.Contracts
{
    public interface ITokenHandler
    {
        string GenerateToken(IEnumerable<Claim> claims);
        ClaimsDTO ExtractClaimsFromJwt(string token);
    }
}
