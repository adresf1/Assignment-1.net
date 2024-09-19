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

    public Task UpdateAsync(String title, String body, int userID)
    {
        if (title is null) throw new ArgumentNullException(nameof(title));
        Post post = new Post()
        {
            Title = title,
            Body = body,
       
            UserId = userID
        };
        
       return iPostRepository.UpdateAsync(post);
    }
    
    
}