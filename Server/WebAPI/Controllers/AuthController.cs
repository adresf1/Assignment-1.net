using ApiContracts;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    
   
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginDto)
        {
            // Find user by username asynchronously
            var user = await _userRepository.GetByUsernameAsync(loginDto.Username);
            if (user == null || user.Password != loginDto.Password)
            {
                return Unauthorized("Invalid username or password.");
            }

            // Convert to UserDTO to exclude sensitive info
            var userDto = new UserDTO(user.Id, user.Username);

            return Ok(userDto);
        }
    
}
    