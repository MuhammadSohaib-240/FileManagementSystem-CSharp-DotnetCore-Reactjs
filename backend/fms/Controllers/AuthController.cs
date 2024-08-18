using fms.Models.DTOs;
using fms.Security;
using fms.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace fms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;

        public AuthController(IUserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDTO dto)
        {
            var result = _userService.Register(dto);
            if (result == "success")
            {
                return Ok(new { message = "success" });
            }
            return BadRequest(new { message = result });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO dto)
        {
            var jwt = _userService.Login(dto);
            if (jwt == "Invalid Credentials")
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = false,
                SameSite = SameSiteMode.None,
                Secure = true
            });

            Console.WriteLine($"Token set in cookie: {jwt}");
            return Ok(new { message = "success" });
        }


        [HttpPost("logout")]
        public IActionResult LogOut()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { message = "success" });
        }

        [Authorize]
        [HttpGet("user")]
        public IActionResult GetUser()
        {
            try
            {
                var user = _userService.GetUserFromToken(Request.Cookies["jwt"]);
                if (user == null) throw new Exception("Unauthorized");
                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
    }
}
