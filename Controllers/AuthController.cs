using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant_API.Model.DTO.Auth;
using Restaurant_API.Repositories.Auth;


namespace Restaurant_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly TokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager,
            TokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser()
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Email
            };

            var IdentityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (IdentityResult.Succeeded)
            {
                // Adding Role 
                IdentityResult = await userManager.AddToRolesAsync(identityUser, new[] { "Customer" });

                if (IdentityResult.Succeeded)
                {
                    return Ok("User was registered! Please login.");
                }
            }

            return BadRequest("Something went wrong");
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Email);

            if (user == null)
            {
                var CheckPassResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (CheckPassResult)
                {
                    // Create Token
                    var roles = await userManager.GetRolesAsync(user);
                    var role = roles.Count == 1 ? roles.FirstOrDefault() : null;

                    if (role != null)
                    {
                        // Get Token
                        var jwtToken = tokenRepository.CreateJWTToken(user, role);

                        var response = new LoginResponseDto()
                        {
                            JWTToken = jwtToken
                        };

                        return Ok(response);
                    }
                }
            }
            return BadRequest("Username or password incorrect");
        }
    }
}
