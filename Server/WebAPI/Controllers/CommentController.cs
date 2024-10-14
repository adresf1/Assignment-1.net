using System.Text.Json;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
 [ApiController]
    [Route("api/[controller]")]
public class CommentController : ControllerBase
{
        private readonly string _commentPath = 
            @"C:\Users\adres\RiderProjects\Assignment 1.net\Server\CLI\bin\Debug\net8.0\comments.json";

        [HttpGet]
        public ActionResult<IEnumerable<Comment>> GetAllComments()
        {
            if (!System.IO.File.Exists(_commentPath))
            {
                return NotFound("The comments file could not be found.");
            }

            var jsonData = System.IO.File.ReadAllText(_commentPath);
            var comments = JsonSerializer.Deserialize<List<Comment>>(jsonData) ?? new List<Comment>();

            if (comments.Count == 0)
            {
                return NotFound("No comments found.");
            }

            return Ok(comments);
        }

        [HttpPost]
        public ActionResult<Comment> AddComment(Comment comment)
        {
            if (!System.IO.File.Exists(_commentPath))
            {
                return NotFound("The comments file could not be found.");
            }

            var jsonData = System.IO.File.ReadAllText(_commentPath);
            var comments = JsonSerializer.Deserialize<List<Comment>>(jsonData) ?? new List<Comment>();

            comment.Id = comments.Any() ? comments.Max(c => c.Id) + 1 : 1; // Assign a new ID
            comments.Add(comment);

            // Write the updated list of comments back to the file
            System.IO.File.WriteAllText(_commentPath, JsonSerializer.Serialize(comments));

            return CreatedAtAction(nameof(GetAllComments), new { id = comment.Id }, comment);
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteComment(int id)
        {
            if (!System.IO.File.Exists(_commentPath))
            {
                return NotFound("The comments file could not be found.");
            }

            var jsonData = System.IO.File.ReadAllText(_commentPath);
            var comments = JsonSerializer.Deserialize<List<Comment>>(jsonData) ?? new List<Comment>();

            var commentIndex = comments.FindIndex(c => c.Id == id);
            if (commentIndex == -1)
            {
                return NotFound("Comment not found");
            }

            comments.RemoveAt(commentIndex);

            // Write the updated list of comments back to the file
            System.IO.File.WriteAllText(_commentPath, JsonSerializer.Serialize(comments));

            return NoContent(); // 204 status code for successful deletion
        }

        [HttpPut("{id:int}")]
        public ActionResult<Comment> UpdateComment(int id, Comment updatedComment)
        {
            if (!System.IO.File.Exists(_commentPath))
            {
                return NotFound("The comments file could not be found.");
            }

            var jsonData = System.IO.File.ReadAllText(_commentPath);
            var comments = JsonSerializer.Deserialize<List<Comment>>(jsonData) ?? new List<Comment>();

            var commentIndex = comments.FindIndex(c => c.Id == id);
            if (commentIndex == -1)
            {
                return NotFound("Comment not found");
            }

            updatedComment.Id = id; // Ensure the ID remains unchanged
            comments[commentIndex] = updatedComment;

            // Write the updated list of comments back to the file
            System.IO.File.WriteAllText(_commentPath, JsonSerializer.Serialize(comments));

            return Ok(updatedComment);
        }
    }

    
