using Entities;
using Microsoft.VisualBasic.CompilerServices;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatPostView
{
    private readonly IPostRepository postRepo;

    public CreatPostView(IPostRepository postRepo)
    {
        this.postRepo = postRepo;
        
    }

    
    public async Task<Post> addpostAsync(string title, string body, int userId, int ID)
    {
        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(body))
        {
            Console.WriteLine("Title and body cannot be empty!");
            return null;
        }

        Post post = new Post
        {
            Title = title,
            Body = body,
            UserId = userId,
            Id = ID
        };
    
        // Add post to database or in-memory collection
        await postRepo.AddAsync(post);  // Await the async method

        // Simulate asynchronous database operation and return the post
        return post;  // No need for Task.FromResult, since you're already in an async method
    }

   
}