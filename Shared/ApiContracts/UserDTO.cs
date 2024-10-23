namespace ApiContracts;

public class UserDTO
{
    public int Id { get; set; }
    public string Username { get; set; }

    public UserDTO(int id, string username)
    {
        Id = id;
        Username = username;
    }
}