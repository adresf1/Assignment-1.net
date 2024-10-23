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
        // Dependency injection af IPostRepository - giver adgang til repository for posts
        private readonly IPostRepository _postRepository;

        // Constructor for PostsController, som modtager et IPostRepository objekt og sætter det til den private felt _postRepository
        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        // Endpoint til at hente alle posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetAllposts()
        {
            // Henter alle posts asynkront som en liste
            var posts = await Task.Run(() => _postRepository.GetMany().ToList());

            // Hvis der ikke findes nogen posts, returneres en 404 Not Found response
            if (posts == null || !posts.Any())
            {
                return NotFound("No posts found.");
            }

            // Mapper post entiteter til PostDTO'er for at returnere dem til klienten
            var postDtos = posts.Select(p => new PostDTO(p.Id, p.Title, p.Body, p.UserId)).ToList();
            return Ok(postDtos); // Returnerer en 200 OK response med listen over PostDTO'er
        }

        // Endpoint til at hente en post baseret på dens ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PostDTO>> GetpostById(int id)
        {
            try
            {
                // Henter en enkelt post asynkront baseret på dens ID
                var post = await _postRepository.GetSingleAsync(id);

                // Mapper post entiteten til en PostDTO
                var postDto = new PostDTO(post.Id, post.Title, post.Body, post.UserId);

                // Returnerer den fundne post
                return Ok(postDto);
            }
            catch (KeyNotFoundException ex)
            {
                // Returnerer en 404 Not Found response, hvis posten ikke findes
                return NotFound(ex.Message);
            }
        }

        // Endpoint til at oprette en ny post
        [HttpPost]
        public ActionResult<PostDTO> AddPost(Post post)
        {
            try
            {
                // Tilføjer posten til repository asynkront
                var createdPost = _postRepository.AddAsync(post);

                // Mapper den oprettede post til en PostDTO
                var postDto = new PostDTO(createdPost.Id, post.Title, post.Body, post.UserId);

                // Returnerer en 201 Created response med den nye post og angiver lokationen for den nye ressource
                return CreatedAtAction(nameof(GetpostById), new { id = postDto.Id }, postDto);
            }
            catch (Exception e)
            {
                // Returnerer en 400 Bad Request response, hvis der opstår en fejl
                return BadRequest(e.Message);
            }
        }

        // Endpoint til at opdatere en eksisterende post baseret på dens ID
        [HttpPut("{id:int}")]
        public async Task<ActionResult<PostDTO>> UpdatePost(int id, Post updatedPost)
        {
            try
            {
                // Sætter ID'et på den opdaterede post til at matche ID'et i URL'en
                updatedPost.Id = id;

                // Opdaterer posten asynkront
                await _postRepository.UpdateAsync(updatedPost);

                // Mapper den opdaterede post til en PostDTO
                var postDto = new PostDTO(id, updatedPost.Title, updatedPost.Body, updatedPost.UserId);

                // Returnerer en 200 OK response med den opdaterede post
                return Ok(postDto);
            }
            catch (Exception e)
            {
                // Returnerer en 404 Not Found response, hvis der opstår en fejl, f.eks. hvis posten ikke findes
                return NotFound(e.Message);
            }
        }

        // Endpoint til at slette en post baseret på dens ID
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                // Sletter posten asynkront baseret på dens ID
                await _postRepository.DeleteAsync(id);

                // Returnerer en 204 No Content response, hvis sletningen lykkes
                return NoContent();
            }
            catch (Exception e)
            {
                // Returnerer en 404 Not Found response, hvis der opstår en fejl, f.eks. hvis posten ikke findes
                return NotFound(e.Message);
            }
        }
    }
}
