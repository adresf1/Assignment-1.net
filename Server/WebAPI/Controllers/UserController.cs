using System.Text.Json;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private readonly string _Userfilepath =
        @"C:\Users\adres\RiderProjects\Assignment 1.net\Server\CLI\bin\Debug\net8.0\users.json";
        
    [HttpGet]
    public ActionResult<IEnumerable<string>> GetalleUsers()
    {
        var jsonData = System.IO.File.ReadAllText(_Userfilepath);
        var users = JsonSerializer.Deserialize<List<User>>(jsonData);
        return Ok(users);
    }

    [HttpGet("{id}")]
    public ActionResult<string> GetUserById(int id)
    {
        var jsonData = System.IO.File.ReadAllText(_Userfilepath);
        var users = JsonSerializer.Deserialize<List<User>>(jsonData);
        var user = users.FirstOrDefault(p => p.Id == id);
        return user is not null ? Ok(user) : NotFound("Post not found");

    }

    [HttpPost]
    public ActionResult<string> PostUser(User user)
    {
        var jsonData = System.IO.File.ReadAllText(_Userfilepath);
        var users = JsonSerializer.Deserialize<List<User>>(jsonData);
        user.Id = users.Max(p => p.Id) + 1; // Assign a new ID
        users.Add(user);
       return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, users);
        
        
    }

    [HttpPut("{id}")]
    public ActionResult<string> PutUser(int id, User uptateduser)
    {
        var jsonData = System.IO.File.ReadAllText(_Userfilepath);
        var users = JsonSerializer.Deserialize<List<User>>(jsonData);
        var userIndex = users.FindIndex(p => p.Id == id);
        if (userIndex == -1)
        {
            return NotFound("Post not found");
        }
        uptateduser.Id = id;
        users[userIndex] = uptateduser;
        return Ok(users[userIndex]);
    }

    [HttpDelete("{id}")]
    public ActionResult<string> DeleteUser(int id)
    {
        var jsonData = System.IO.File.ReadAllText(_Userfilepath);
        var users = JsonSerializer.Deserialize<List<User>>(jsonData);
        var userIndex = users.FindIndex(p => p.Id == id);
        if (userIndex == -1)
        {
            return NotFound("Post not found");
        }
        users.RemoveAt(userIndex);
        return NoContent();
    
    }
    }