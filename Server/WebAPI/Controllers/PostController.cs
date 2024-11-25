using System.Text.Json;
using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers
{[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostRepository _postRepository;

    public PostsController(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    // Endpoint til at hente alle posts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostDTO>>> GetAllposts()
    {
        var posts =  _postRepository.GetMany();

        if (!posts.Any())
        {
            return NotFound("No posts found.");
        }

        var postDtos = posts.Select(p => new PostDTO(p.Id, p.Title, p.Body, p.UserId)).ToList();
        return Ok(postDtos);
    }

    // Endpoint til at hente en post baseret på dens ID
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

    // Endpoint til at oprette en ny post
    [HttpPost]
    public async Task<ActionResult<PostDTO>> AddPost([FromBody] Post newPost)
    {
        try
        {
            if (newPost == null)
            {
                return BadRequest("Post data is required.");
            }

            await _postRepository.AddAsync(newPost);

            var postDto = new PostDTO(newPost.Id, newPost.Title, newPost.Body, newPost.UserId);
            return CreatedAtAction(nameof(GetpostById), new { id = newPost.Id }, postDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // Endpoint til at opdatere en eksisterende post baseret på dens ID
    [HttpPut("{id:int}")]
    public async Task<ActionResult<PostDTO>> UpdatePost(int id, [FromBody] Post updatedPost)
    {
        try
        {
            updatedPost.Id = id;
            await _postRepository.UpdateAsync(updatedPost);

            var postDto = new PostDTO(id, updatedPost.Title, updatedPost.Body, updatedPost.UserId);
            return Ok(postDto);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    // Endpoint til at slette en post baseret på dens ID
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
