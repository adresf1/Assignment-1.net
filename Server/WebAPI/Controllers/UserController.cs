using System.Text.Json;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly string _userFilePath = 
            @"C:\Users\adres\RiderProjects\Assignment 1.net\Server\CLI\bin\Debug\net8.0\users.json";
        
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            if (!System.IO.File.Exists(_userFilePath))
            {
                return NotFound("The users file could not be found.");
            }

            var jsonData = System.IO.File.ReadAllText(_userFilePath);
            var users = JsonSerializer.Deserialize<List<User>>(jsonData);

            if (users == null || users.Count == 0)
            {
                return NotFound("No users found.");
            }

            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public ActionResult<User> GetUserById(int id)
        {
            if (!System.IO.File.Exists(_userFilePath))
            {
                return NotFound("The users file could not be found.");
            }

            var jsonData = System.IO.File.ReadAllText(_userFilePath);
            var users = JsonSerializer.Deserialize<List<User>>(jsonData);

            if (users == null) return NotFound("No users found.");

            var user = users.FirstOrDefault(p => p.Id == id);
            return user is not null ? Ok(user) : NotFound("User not found");
        }

        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            if (!System.IO.File.Exists(_userFilePath))
            {
                return NotFound("The users file could not be found.");
            }

            var jsonData = System.IO.File.ReadAllText(_userFilePath);
            var users = JsonSerializer.Deserialize<List<User>>(jsonData) ?? new List<User>();

            user.Id = users.Any() ? users.Max(p => p.Id) + 1 : 1; // Assign a new ID
            users.Add(user);

            // Write the updated list of users back to the file
            System.IO.File.WriteAllText(_userFilePath, JsonSerializer.Serialize(users));

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id:int}")]
        public ActionResult<User> PutUser(int id, User updatedUser)
        {
            if (!System.IO.File.Exists(_userFilePath))
            {
                return NotFound("The users file could not be found.");
            }

            var jsonData = System.IO.File.ReadAllText(_userFilePath);
            var users = JsonSerializer.Deserialize<List<User>>(jsonData);

            if (users == null) return NotFound("No users found.");

            var userIndex = users.FindIndex(p => p.Id == id);
            if (userIndex == -1)
            {
                return NotFound("User not found");
            }

            updatedUser.Id = id; // Ensure the ID remains unchanged
            users[userIndex] = updatedUser;

            // Write the updated list of users back to the file
            System.IO.File.WriteAllText(_userFilePath, JsonSerializer.Serialize(users));

            return Ok(updatedUser);
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteUser(int id)
        {
            if (!System.IO.File.Exists(_userFilePath))
            {
                return NotFound("The users file could not be found.");
            }

            var jsonData = System.IO.File.ReadAllText(_userFilePath);
            var users = JsonSerializer.Deserialize<List<User>>(jsonData);

            if (users == null) return NotFound("No users found.");

            var userIndex = users.FindIndex(p => p.Id == id);
            if (userIndex == -1)
            {
                return NotFound("User not found");
            }

            users.RemoveAt(userIndex);

            // Write the updated list of users back to the file
            System.IO.File.WriteAllText(_userFilePath, JsonSerializer.Serialize(users));

            return NoContent(); // 204 status code, meaning successfully deleted
        }
    }
}
