using Microsoft.AspNetCore.Identity;

namespace Blog.API.Repositorise
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
