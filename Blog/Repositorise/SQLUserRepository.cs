using Blog.API.Data;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Blog.API.Repositorise
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly BlogDBContext dBContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public SQLUserRepository(BlogDBContext dBContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.dBContext = dBContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IdentityUser> GetByJwtToken(string jwtToken)
        {
            // Validate and decode the JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(jwtToken);

            // Check if the 'sub' claim exists
            var userEmailClaim = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            if (userEmailClaim == null)
            {
                return null;
            }

            // Retrieve user ID from the 'sub' claim
            var userEmail = userEmailClaim.Value;

            var user = await userManager.FindByEmailAsync(userEmail);

            if (user is null)
            {
                return null;
            }

            return user;
        }

        public async Task<List<string>> GetRoles(string jwtToken)
        {
            var currentUser = await GetByJwtToken(jwtToken);

            if (currentUser is null)
            {
                return null;
            }

            List<string> roles = new List<string>();
            roles.AddRange(await userManager.GetRolesAsync(currentUser));

            if (roles is null)
            {
                return null;
            }

            return roles;
        }
    }
}
