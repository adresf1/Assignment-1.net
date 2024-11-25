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
        // Dependency injection af IUserRepository - giver adgang til repository for users
        private readonly IUserRepository _userRepository;

        // Constructor for UserController, som modtager et IUserRepository objekt og sætter det til den private felt _userRepository
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Endpoint til at hente alle brugere
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            // Henter alle brugere asynkront som en liste
            var users = await Task.Run(() => _userRepository.GetMany().ToList());

            // Hvis der ikke findes nogen brugere, returneres en 404 Not Found response
            if (users == null || !users.Any())
            {
                return NotFound("No users found.");
            }

            // Mapper User entiteter til UserDTO'er for at returnere dem til klienten
            var userDTOs = users.Select(u => new UserDTO(u.Id, u.Username)).ToList();
            return Ok(userDTOs); // Returnerer en 200 OK response med listen over UserDTO'er
        }

        // Endpoint til at hente en bruger baseret på dens ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            try
            {
                // Henter en enkelt bruger asynkront baseret på dens ID
                var user = await _userRepository.GetSingleAsync(id);

                // Mapper User entiteten til en UserDTO
                var userDto = new UserDTO(user.Id, user.Username);

                // Returnerer den fundne bruger
                return Ok(userDto);
            }
            catch (KeyNotFoundException ex)
            {
                // Returnerer en 404 Not Found response, hvis brugeren ikke findes
                return NotFound(ex.Message);
            }
        }

        // Endpoint til at oprette en ny bruger
       /*
        [HttpPost]
        public async Task<ActionResult<UserDTO>> AddUser([FromBody] User newUser)
        {
            try
            {
                if (newUser == null)
                {
                    return BadRequest("User data is required.");
                }

                await _userRepository.AddAsync(newUser);

                var userDto = new UserDTO(newUser.Id, newUser.Username);
                return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, userDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        */
        
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(User user)
        {
            try
            {
                // Tilføjer brugeren til repository asynkront
                var createdUser = await _userRepository.AddAsync(user);

                // Mapper den oprettede bruger til en UserDTO
                var userDto = new UserDTO(createdUser.Id, createdUser.Username);

                // Returnerer en 201 Created response med den nye bruger og angiver lokationen for den nye ressource
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, userDto);
            }
            catch (Exception ex)
            {
                // Returnerer en 400 Bad Request response, hvis der opstår en fejl
                return BadRequest(ex.Message);
            }
        }
        

        // Endpoint til at opdatere en eksisterende bruger baseret på dens ID
        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserDTO>> PutUser(int id, User updatedUser)
        {
            try
            {
                // Sætter ID'et på den opdaterede bruger til at matche ID'et i URL'en
                updatedUser.Id = id;

                // Opdaterer brugeren asynkront
                await _userRepository.UpdateAsync(updatedUser);

                // Mapper den opdaterede bruger til en UserDTO
                var userDto = new UserDTO(updatedUser.Id, updatedUser.Username);

                // Returnerer en 200 OK response med den opdaterede bruger
                return Ok(userDto);
            }
            catch (KeyNotFoundException ex)
            {
                // Returnerer en 404 Not Found response, hvis brugeren ikke findes
                return NotFound(ex.Message);
            }
        }

        // Endpoint til at slette en bruger baseret på dens ID
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                // Sletter brugeren asynkront baseret på dens ID
                await _userRepository.DeleteAsync(id);

                // Returnerer en 204 No Content response, hvis sletningen lykkes
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // Returnerer en 404 Not Found response, hvis brugeren ikke findes
                return NotFound(ex.Message);
            }
        }
    }
}

