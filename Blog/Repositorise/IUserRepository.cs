using Microsoft.AspNetCore.Identity;

namespace Blog.API.Repositorise
{
    public interface IUserRepository
    {
        Task<IdentityUser> GetByJwtToken(string jwtToken);
        Task<List<string>> GetRoles(string jwtToken);
    }
}
