using System.Text.Json;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly string _postsFilePath = @"C:\Users\adres\RiderProjects\Assignment 1.net\Server\CLI\bin\Debug\net8.0\post.json";

        [HttpGet("{id:int}")]
        public ActionResult<Post> GetPostById(int id)
        {
            if (!System.IO.File.Exists(_postsFilePath))
            {
                return NotFound("The posts file could not be found.");
            }

            var jsonData = System.IO.File.ReadAllText(_postsFilePath);
            var posts = JsonSerializer.Deserialize<List<Post>>(jsonData);

            if (posts == null) return NotFound("No posts found.");

            var post = posts.FirstOrDefault(p => p.Id == id);
            return post is not null ? Ok(post) : NotFound("Post not found");
        }

        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetAllPosts()
        {
            if (!System.IO.File.Exists(_postsFilePath))
            {
                return NotFound("The posts file could not be found.");
            }

            var jsonData = System.IO.File.ReadAllText(_postsFilePath);
            var posts = JsonSerializer.Deserialize<List<Post>>(jsonData);

            if (posts == null) return NotFound("No posts found.");

            return Ok(posts);
        }

        [HttpPost]
        public ActionResult<Post> AddPost(Post post)
        {
            var jsonData = System.IO.File.ReadAllText(_postsFilePath);
            var posts = JsonSerializer.Deserialize<List<Post>>(jsonData) ?? new List<Post>();

            post.Id = posts.Any() ? posts.Max(p => p.Id) + 1 : 1; // Assign a new ID
            posts.Add(post);

            // Write the updated list back to the file
            System.IO.File.WriteAllText(_postsFilePath, JsonSerializer.Serialize(posts));

            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post); // Return the newly created post
        }

        [HttpPut("{id:int}")]
        public ActionResult<Post> UpdatePost(int id, Post updatedPost)
        {
            var jsonData = System.IO.File.ReadAllText(_postsFilePath);
            var posts = JsonSerializer.Deserialize<List<Post>>(jsonData);

            if (posts == null) return NotFound("No posts found.");

            var postIndex = posts.FindIndex(p => p.Id == id);
            if (postIndex == -1)
            {
                return NotFound("Post not found");
            }

            updatedPost.Id = id; // Ensure the ID remains unchanged
            posts[postIndex] = updatedPost;

            // Write the updated list back to the file
            System.IO.File.WriteAllText(_postsFilePath, JsonSerializer.Serialize(posts));

            return Ok(updatedPost);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeletePost(int id)
        {
            var jsonData = System.IO.File.ReadAllText(_postsFilePath);
            var posts = JsonSerializer.Deserialize<List<Post>>(jsonData);

            if (posts == null) return NotFound("No posts found.");

            var post = posts.FirstOrDefault(p => p.Id == id);
            if (post is null)
            {
                return NotFound("Post not found");
            }

            posts.Remove(post);

            // Write the updated list back to the file
            System.IO.File.WriteAllText(_postsFilePath, JsonSerializer.Serialize(posts));

            return NoContent(); // 204 status code, meaning successfully deleted
        }
    }
}
