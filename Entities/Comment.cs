using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Comment
{
    [Key]
    public int Id { get; set; }
    
    public String Body { get; set; }

    public string  tostring()
    {
        return $"Id: {Id}, Body: {Body}";
    }
    
}