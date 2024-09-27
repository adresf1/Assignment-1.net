namespace Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public string tostring()
    {
        return $"User id {Id} username {Username} password {Password}";
    }
    
}