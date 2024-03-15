using AutoMapper;
using Blog.API.Repositorise;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public UsersController(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        [HttpGet]
        [Route("GetUserFromToken")]
        public async Task<IActionResult> GetByJwtToken([FromQuery] string jwtToken)
        {
            var user = await userRepository.GetByJwtToken(jwtToken);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet]
        [Route("GetRolesFromToken")]
        public async Task<IActionResult> GetRolesByJwtToken([FromQuery] string jwtToken)
        {
            var roles = await userRepository.GetRoles(jwtToken);

            if (roles == null)
            {
                return NotFound();
            }

            return Ok(roles);
        }
    }
}
