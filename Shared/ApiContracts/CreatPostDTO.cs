namespace ApiContracts;

public class CreatPostDTO
{
    public string Title { get; set; }
    public string Body { get; set; }
    
    public int Id { get; set; }
    public int UserId { get; set; }
}