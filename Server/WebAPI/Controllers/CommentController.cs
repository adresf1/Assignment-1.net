using System.Text.Json;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
 [ApiController]
    [Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly string _commentpath = @"C:\Users\adres\RiderProjects\Assignment 1.net\Server\CLI\bin\Debug\net8.0\comments.json";

    
    
    [HttpGet]
    public ActionResult<IEnumerable<Comment>> GetAllComment()
    {
        // Ensure the file exists
        if (!System.IO.File.Exists(_commentpath))
        {
            return NotFound("The comments file could not be found.");
        }

        // Read the JSON file
        var jsonData = System.IO.File.ReadAllText(_commentpath);

        // Deserialize the JSON data into a list of Post objects
        var comments = JsonSerializer.Deserialize<List<Comment>>(jsonData);

        // Return the list of posts
        return Ok(comments);
    }

    [HttpPost]
    public ActionResult<Comment> AddComment(Comment comment)
    {
        if (!System.IO.File.Exists(_commentpath))
        {
            return NotFound("The comments file could not be found.");
        }
        // Read the JSON file
        var jsonData = System.IO.File.ReadAllText(_commentpath);

        // Deserialize the JSON data into a list of Post objects
        var comments = JsonSerializer.Deserialize<List<Comment>>(jsonData);

        comments.Add(comment);
        // Return the list of posts
        return Ok(comments);
    }

    [HttpDelete("{id}")]
    public ActionResult<Comment> DeleteComment(int id)
    {
        if (!System.IO.File.Exists(_commentpath))
        {
            return NotFound("The comments file could not be found.");
        }
        // Read the JSON file
        var jsonData = System.IO.File.ReadAllText(_commentpath);

        // Deserialize the JSON data into a list of Post objects
        var comments = JsonSerializer.Deserialize<List<Comment>>(jsonData);
        var comment = comments.FirstOrDefault(p => p.Id == id);
        comments.Remove(comment);
        return NoContent();
    }

    [HttpPut("{id}")]
    public ActionResult<Comment> UpdateComment(int id, Comment updatedComment)
    {
        if (!System.IO.File.Exists(_commentpath))
        {
            return NotFound("The comments file could not be found.");
        }
        // Read the JSON file
        var jsonData = System.IO.File.ReadAllText(_commentpath);

        // Deserialize the JSON data into a list of Post objects
        var comments = JsonSerializer.Deserialize<List<Comment>>(jsonData);
        var postIndex = comments.FindIndex(p => p.Id == id);
        if (postIndex == -1)
        {
            return NotFound("Post not found");
        }

        updatedComment.Id = id; // Ensure the ID remains unchanged
        comments[postIndex] = updatedComment;
        return Ok(comments[postIndex]);
    }
    
    
}