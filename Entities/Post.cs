using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Post
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }


    public string tostring()
    {
        return $"Title: {Title}\nBody: {Body}\nUserId: {UserId}";
    }
}