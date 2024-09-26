using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFIleReposity : IPostRepository
{
    private readonly string filePath = "post.json";
        
    public PostFIleReposity()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    public async Task<Post> AddAsync(Post post)
    {
        string postasjson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postasjson)!;
        int maxId = posts.Count > 0 ? posts.Max(p => p.Id) : 1;
        post.Id = maxId + 1;

       
        posts.Add(post);

        
        postasjson = JsonSerializer.Serialize(posts);

        
        await File.WriteAllTextAsync(filePath, postasjson);

        return post;
    }

    public async Task UpdateAsync(Post post)
    {
       
        string postAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postAsJson)!;

        // Find the post with the matching Id
        Post? existingPost = posts.FirstOrDefault(p => p.Id == post.Id);

        if (existingPost != null)
        {
           
            existingPost.Title = post.Title;
            existingPost.Body = post.Body;
            existingPost.UserId = post.UserId;
            existingPost.Id = post.Id;

            
            postAsJson = JsonSerializer.Serialize(posts);

            
            await File.WriteAllTextAsync(filePath, postAsJson);
        }
        else
        {
            throw new KeyNotFoundException($"Post with Id {post.Id} not found.");
        }
    }

    public async Task DeleteAsync(int id)
    {
        string postAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postAsJson)!;

       
        Post? postToDelete = posts.FirstOrDefault(p => p.Id == id);

        if (postToDelete != null)
        {
            posts.Remove(postToDelete);

           
            postAsJson = JsonSerializer.Serialize(posts);

           
            await File.WriteAllTextAsync(filePath, postAsJson);
        }
        else
        {
            throw new KeyNotFoundException($"Post with Id {id} not found.");
        }
    }

    public async Task<Post> GetSingleAsync(int id)
    {
       
        string postAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postAsJson)!;

       
        Post? post = posts.FirstOrDefault(p => p.Id == id);

        return post ?? throw new KeyNotFoundException($"Post with Id {id} not found.");
    }

    public IQueryable<Post> GetMany()
    {
       
        string postAsJson = File.ReadAllText(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postAsJson)!;

        
        return posts.AsQueryable();
    }
}