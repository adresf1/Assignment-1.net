using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatPostView
{
    private readonly IPostRepository postRepo;

    public CreatPostView(IPostRepository postRepo)
    {
        this.postRepo = postRepo;
        
    }

    
    public async Task<Post> AddAsync(Post post)
    {
        // Add post to database or in-memory collection
        postRepo.AddAsync(post);  // Assuming posts is your collection or DbSet

        // Simulate asynchronous database operation
        return await Task.FromResult(post);  // In real scenarios, you'd await a database call here
    }

   
}