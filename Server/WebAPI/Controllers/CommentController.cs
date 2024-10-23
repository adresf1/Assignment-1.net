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
    // Dependency injection af ICommentRepository - giver adgang til repository for kommentarer
    private ICommentRepostory _commentRepostory;

    // Constructor for CommentController, som modtager et ICommentRepository objekt og sætter det til den private felt _commentRepostory
    public CommentController(ICommentRepostory commentRepostory)
    {
        _commentRepostory = commentRepostory;
    }
    
    // Endpoint til at hente alle kommentarer
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentDTO>>> GetAllcomments()
    {
        // Henter alle kommentarer som en liste asynkront ved hjælp af Task.Run (ikke nødvendigt i de fleste tilfælde, men gør det asynkront)
        var comments = await Task.Run(() => _commentRepostory.GetMany().ToList());

        // Hvis der ikke findes kommentarer, returneres en 404 Not Found response
        if (comments == null || !comments.Any())
        {
            return NotFound("No comments found.");
        }

        // Mapper kommentar entiteter til CommentDTO'er for at returnere dem til klienten
        var commentDto = comments.Select(c => new CommentDTO(c.Id, c.Body)).ToList();
        return Ok(commentDto); // Returnerer en 200 OK response med listen over CommentDTO'er
    }

    // Endpoint til at tilføje en ny kommentar
    [HttpPost]
    public async Task<ActionResult<CommentDTO>> AddComment([FromBody] Comment comment)
    {
        try
        {
            // Tilføjer kommentaren til repository asynkront
            var createdComment = await _commentRepostory.AddAsync(comment);

            // Mapper den oprettede kommentar til en CommentDTO
            var commentDto = new CommentDTO(createdComment.Id, createdComment.Body);

            // Returnerer en 201 Created response med den nye kommentar, og definerer lokationen for den nye ressource
            return CreatedAtAction(nameof(GetAllcomments), new { id = commentDto.Id }, commentDto);
        }
        catch (Exception e)
        {
            // Returnerer en 400 Bad Request response med fejlen, hvis noget går galt
            return BadRequest(e.Message);
        }
    }

    // Endpoint til at slette en kommentar baseret på dens ID
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteComment(int id)
    {
        try
        {
            // Sletter kommentaren asynkront baseret på dens ID
            await _commentRepostory.DeleteAsync(id);
            return NoContent(); // Returnerer en 204 No Content response, hvis sletningen lykkes
        }
        catch (Exception e)
        {
            // Returnerer en 404 Not Found response med en fejlmeddelelse, hvis noget går galt
            return NotFound(e.Message);
        }
    }

    // Endpoint til at opdatere en eksisterende kommentar baseret på dens ID
    [HttpPut("{id:int}")]
    public async Task<ActionResult<Comment>> UpdateComment(int id, Comment updatedComment)
    {
        try
        {
            // Sætter ID'et på den opdaterede kommentar til at matche ID'et i URL'en
            updatedComment.Id = id;

            // Opdaterer kommentaren asynkront
            await _commentRepostory.UpdateAsync(updatedComment);

            // Mapper den opdaterede kommentar til en CommentDTO
            var postDTO = new CommentDTO(updatedComment.Id, updatedComment.Body);

            // Returnerer en 200 OK response med den opdaterede kommentar
            return Ok(postDTO);
        }
        catch (Exception e)
        {
            // Returnerer en 404 Not Found response, hvis noget går galt, f.eks. hvis kommentaren ikke findes
            return NotFound(e.Message);
        }
    }
}
