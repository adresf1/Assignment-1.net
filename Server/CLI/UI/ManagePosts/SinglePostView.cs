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

    Task<Post> GetSingleAsync(int id)
    {
        return IpostRepository.GetSingleAsync(id);
    }
    
}