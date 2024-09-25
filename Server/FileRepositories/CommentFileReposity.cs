using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepostory
{
    private readonly string filePath = "comments.json";
        
    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    
    public async Task<Comment> AddAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        int maxId = comments.Count > 0 ? comments.Max(c => c.Id) : 1;
        comment.Id = maxId + 1;
        comments.Add(comment);
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
        
        
        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
    // læser alle filer fra filen
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
    
       //laver det om til liste 
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

        //Matching comment
        Comment? existingComment = comments.FirstOrDefault(c => c.Id == comment.Id);

        if (existingComment != null)
        {
            //updatering af den exixterende fil 
            existingComment.Id = comment.Id;
            existingComment.Body = comment.Body;
          
            // Add any other properties that need to be updated here
        
            // Serialize the updated list back to JSON
            commentsAsJson = JsonSerializer.Serialize(comments);
        
            // Write the updated JSON back to the file
            await File.WriteAllTextAsync(filePath, commentsAsJson);
    }
        {
            
            }
        }
    public async Task DeleteAsync(int id)
    {
       //læser
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
    
        // liste
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

        // matching
        Comment? commentToDelete = comments.FirstOrDefault(c => c.Id == id);
    
        if (commentToDelete != null)
        {
            comments.Remove(commentToDelete);
        
            
            commentsAsJson = JsonSerializer.Serialize(comments);
        
            
            await File.WriteAllTextAsync(filePath, commentsAsJson);
            
                
            
        }
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
    
        
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

        
        Comment? comment = comments.FirstOrDefault(c => c.Id == id);
    
        return comment ?? throw new KeyNotFoundException($"Comment with Id {id} not found.");
    }

    public IQueryable<Comment> GetMany()
    {
        string commentsAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        return comments.AsQueryable();
    }
}