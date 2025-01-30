using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IMapper mapper;

        public AuthController(UserManager<IdentityUser> userManager,
            TokenRepository tokenRepository,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.mapper = mapper;
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

            if (user != null)
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

        [HttpGet("Admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAccounts()
        {
            return Ok(userManager.Users.ToList());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAccountById()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Invalid token or user not found.");
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAccountById([FromBody] UpdateRequestDto updateUserRequestDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Invalid token or user not found.");
            }

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var userDomainModel = mapper.Map<IdentityUser>(updateUserRequestDto);

            if (!string.IsNullOrWhiteSpace(updateUserRequestDto.Username))
            {
                user.UserName = updateUserRequestDto.Username;
            }

            if (!string.IsNullOrWhiteSpace(updateUserRequestDto.Email))
            {
                user.Email = updateUserRequestDto.Email;
            }

            if (!string.IsNullOrWhiteSpace(updateUserRequestDto.Password))
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var passwordChangeResult = await userManager.ResetPasswordAsync(user, token, updateUserRequestDto.Password);

                if (!passwordChangeResult.Succeeded)
                {
                    return BadRequest("Something went wrong while changing password!");
                }
            }

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest("Something went wrong");
            }

            return Ok("User updated successfully");
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Invalid token or user not found.");
            }

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var result = await userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest("Something went Wrong!");
            }

            return Ok("User deleted successfully");
        }
    }
}
