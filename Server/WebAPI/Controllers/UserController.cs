using System.Text.Json;
using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            var users = await Task.Run(() => _userRepository.GetMany().ToList());

            if (users == null || !users.Any())
            {
                return NotFound("No users found.");
            }

            // Map User entities to UserDTOs
            var userDTOs = users.Select(u => new UserDTO(u.Id, u.Username)).ToList();
            return Ok(userDTOs);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            try
            {
                var user = await _userRepository.GetSingleAsync(id);
                var userDto = new UserDTO(user.Id, user.Username);
                return Ok(userDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(User user)
        {
            try
            {
                var createdUser = await _userRepository.AddAsync(user);
                var userDto = new UserDTO(createdUser.Id, createdUser.Username);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, userDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserDTO>> PutUser(int id, User updatedUser)
        {
            try
            {
                updatedUser.Id = id; 
                await _userRepository.UpdateAsync(updatedUser);
                var userDto = new UserDTO(updatedUser.Id, updatedUser.Username);
                return Ok(userDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
 
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                await _userRepository.DeleteAsync(id);
                return NoContent(); // 204 status code, meaning successfully deleted
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

