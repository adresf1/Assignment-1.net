using System.Text.Json;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    // Path to your JSON file
    private readonly string _postsFilePath = @"C:\Users\adres\RiderProjects\Assignment 1.net\Server\CLI\bin\Debug\net8.0\post.json";


    [HttpGet("{id:int}")]
    public ActionResult<Post> GetpostById(int id)
    {
        if (!System.IO.File.Exists(_postsFilePath))
        {
            return NotFound("The posts file could not be found.");
        }

        // Read the JSON file
        var jsonData = System.IO.File.ReadAllText(_postsFilePath);

        // Deserialize the JSON data into a list of Post objects
        var posts = JsonSerializer.Deserialize<List<Post>>(jsonData);
        var post = posts.FirstOrDefault(p => p.Id == id);
        return post is not null ? Ok(post) : NotFound("Post not found");

    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Post>> GetAllPosts()
    {
        // Ensure the file exists
        if (!System.IO.File.Exists(_postsFilePath))
        {
            return NotFound("The posts file could not be found.");
        }

        // Read the JSON file
        var jsonData = System.IO.File.ReadAllText(_postsFilePath);

        // Deserialize the JSON data into a list of Post objects
        var posts = JsonSerializer.Deserialize<List<Post>>(jsonData);

        // Return the list of posts
        return Ok(posts);
    }

    [HttpPost]
    public ActionResult<Post> AddPost(Post post)
    {
        // Read the JSON file
        var jsonData = System.IO.File.ReadAllText(_postsFilePath);

        // Deserialize the JSON data into a list of Post objects
        var posts = JsonSerializer.Deserialize<List<Post>>(jsonData);

        post.Id = posts.Max(p => p.Id) + 1; // Assign a new ID
       
        posts.Add(post);
        
        return CreatedAtAction(nameof(AddPost), new { id = posts.Count }, posts);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Post> UpdatePost(int id, Post updatedPost)
    {
        var jsonData = System.IO.File.ReadAllText(_postsFilePath);
        var posts = JsonSerializer.Deserialize<List<Post>>(jsonData);
        
       
            var postIndex = posts.FindIndex(p => p.Id == id);
            if (postIndex == -1)
            {
                return NotFound("Post not found");
            }

            updatedPost.Id = id; // Ensure the ID remains unchanged
            posts[postIndex] = updatedPost;
            return Ok(posts[postIndex]);
       
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult DeletePost(int id)
    {
        var jsonData = System.IO.File.ReadAllText(_postsFilePath);
        var posts = JsonSerializer.Deserialize<List<Post>>(jsonData);
        var post = posts.FirstOrDefault(p => p.Id == id);
        if (post is null)
        {
            return NotFound("Post not found");
        }

        posts.Remove(post);
        return NoContent();
    }
    
    
}