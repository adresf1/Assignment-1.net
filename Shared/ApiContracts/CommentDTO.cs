namespace ApiContracts;

public class CommentDTO
{
    public int Id { get; set; }
    
    public String Body { get; set; }

    public CommentDTO(int id, String body)
    {
        Id = id;
        Body = body;
        
    }
}