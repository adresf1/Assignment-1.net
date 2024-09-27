namespace Entities;

public class Comment
{
    public int Id { get; set; }
    
    public String Body { get; set; }

    public string  tostring()
    {
        return $"Id: {Id}, Body: {Body}";
    }
}