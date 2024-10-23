using System.Text.Json;
using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {

        private readonly IPostRepository _postRepository;

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetAllposts()
        {
            var posts = await Task.Run(() => _postRepository.GetMany().ToList());

            if (posts == null || !posts.Any())
            {
                return NotFound("No posts found.");
            }

            // Map post entities to Postdtos
            var posDtos = posts.Select(p => new PostDTO(p.Id,p.Title,p.Body,p.UserId)).ToList();
            return Ok(posDtos);
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PostDTO>> GetpostById(int id)
        {
            try
            {
                var post = await _postRepository.GetSingleAsync(id);
                var postDto = new PostDTO(post.Id, post.Title, post.Body, post.UserId);
                return Ok(postDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

       

        [HttpPost]
        public ActionResult<PostDTO> AddPost(Post post)
        {
            try
            {
            var createdPost = _postRepository.AddAsync(post);
            var postDto = new PostDTO(createdPost.Id, post.Title, post.Body, post.UserId);
            return CreatedAtAction(nameof(GetpostById), new { id = postDto.Id }, postDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<PostDTO>> UpdatePost(int id, Post updatedPost)
        {
            try
            {
                updatedPost.Id = id;
                await _postRepository.UpdateAsync(updatedPost);
                var PostDto = new PostDTO(id, updatedPost.Title, updatedPost.Body, updatedPost.UserId);
                return Ok(PostDto);
                
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
            await _postRepository.DeleteAsync(id);
            return NoContent();
            }
            catch (Exception e)
            {
               return NotFound(e.Message);
            }
        }
    }
}
