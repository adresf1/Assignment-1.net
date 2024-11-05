namespace ApiContracts;

public class PostsDTO
{
    public string Title { get; set; }
    public string Body { get; set; }
    public UserDTO UserDto {get; set; }
    

    public string AuthorName => $"{UserDto.Username}";

}