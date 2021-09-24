using ikubINFO.Entity.Dtos;
using System.Threading.Tasks;

namespace ikubINFO.Service.Authorization
{
    public interface IAuthorizationService
    {
        Task<TokenResult> Token(JwtTokenInfo request);
    }
}