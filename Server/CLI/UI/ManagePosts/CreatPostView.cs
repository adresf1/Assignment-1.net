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

    
    public async Task addpostAsync(string title, string body, int userId)
    {
        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(body))
        {
            Console.WriteLine("Title and body cannot be empty!");
            
        }

        Post post = new Post
        {
            Title = title,
            Body = body,
            UserId = userId
        };
    
        // Add post to database or in-memory collection
        await postRepo.AddAsync(post);  // Await the async method

       
    }

   
}