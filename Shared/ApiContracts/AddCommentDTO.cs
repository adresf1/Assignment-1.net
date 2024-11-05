namespace ApiContracts;

public class AddCommentDTO
{
  
    public string Title { get; set; }
    public string Body { get; set; }
    public UserDTO UserDto {get; set; }
    
    
}