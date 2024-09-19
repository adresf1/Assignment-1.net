using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostView
{
    private readonly IPostRepository iPostRepository;

    public ListPostView(IPostRepository IpostRepository)
    {
        this.iPostRepository = IpostRepository;
    }

    public List<Post> ListPosts()
    {
       return iPostRepository.GetMany().ToList();
    }
}