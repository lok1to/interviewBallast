using InterviewBallast.Core.Dto.Authenticate;
using InterviewBallast.Core.Dto.Player;
using InterviewBallast.Core.Dto.User;
using InterviewBallast.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InterviewBallast.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [Authorize]
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(UserRequest userRequest)
        {
            var response = await _userService.AddUser(userRequest);
            return Ok(response);
        }
    }
}
