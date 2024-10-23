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
        public async Task<ActionResult<CommentDTO>> AddComment([FromBody] Comment comment)
        {
            try
            {
                var createdComment = await _commentRepostory.AddAsync(comment);

                var commentDto = new CommentDTO(createdComment.Id, createdComment.Body);

                return CreatedAtAction(nameof(GetAllcomments), new { id = commentDto.Id }, commentDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            try
            {
                await _commentRepostory.DeleteAsync(id);
                return NoContent();

            }
            catch (Exception e)
            {
               return NotFound(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Comment>> UpdateComment(int id, Comment updatedComment)
        {
            try
            {
                updatedComment.Id = id;
                await _commentRepostory.UpdateAsync(updatedComment);
                var postDTO = new CommentDTO(updatedComment.Id, updatedComment.Body);
                return Ok(postDTO);
            }
            catch (Exception e)
            {
               return NotFound(e.Message);
            }
            
        }
}
        
    
