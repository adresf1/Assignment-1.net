using System.Text.Json;
using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;
 [ApiController]
    [Route("api/[controller]")]
public class CommentController : ControllerBase
{
       private ICommentRepostory _commentRepostory;

       public CommentController(ICommentRepostory commentRepostory)
       {
           _commentRepostory = commentRepostory;
       }
       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetAllcomments()
        {
            
            var comments = await Task.Run(() => _commentRepostory.GetMany().ToList());

            if (comments == null || !comments.Any())
            {
                return NotFound("No comments found.");
            }

            // Map post entities to Postdtos
            var commentDto = comments.Select(c => new CommentDTO(c.Id,c.Body)).ToList();
            return Ok(commentDto);
        }

        [HttpPost]
        public ActionResult<CommentDTO> AddComment(Comment comment)
        {
            try
            {
                var createdcommentt = _commentRepostory.AddAsync(comment);
                var commentDto = new CommentDTO(createdcommentt.Id,createdcommentt.Body);
                return CreatedAtAction(nameof(GetAllcomments), new { id = commentDto.Id }, commentDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteComment(int id)
        {
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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

    
