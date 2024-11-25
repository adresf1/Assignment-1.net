using System.ComponentModel.DataAnnotations;

namespace Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public override string ToString()
    {
        return $"User id {Id} username {Username}";
    }
}
    
