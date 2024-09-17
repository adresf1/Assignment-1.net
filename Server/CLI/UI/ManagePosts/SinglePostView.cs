using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    public readonly IPostRepository IpostRepository;
    
    public SinglePostView(IPostRepository IpostRepository)
    {
        this.IpostRepository = IpostRepository;
        
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        if (id <= 0) // Check if id is valid
        {
            Console.WriteLine("No valid post id provided");
            return null;  
        }

        
        Post post = await IpostRepository.GetSingleAsync(id);
        

        if (post == null)
        {
            Console.WriteLine($"No post found with id {id}");
            return null;
        }

        return post;
    }
        
    }
    
