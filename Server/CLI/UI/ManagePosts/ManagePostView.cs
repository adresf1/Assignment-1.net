using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostView
{
    public readonly IPostRepository iPostRepository;
    
    public ManagePostView(IPostRepository IpostRepository)
    {
        this.iPostRepository = IpostRepository;
        
    }

    public Task UpdateAsync(Post post)
    {
       return iPostRepository.UpdateAsync(post);
    }
    
}