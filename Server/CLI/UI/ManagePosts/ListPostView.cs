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

    public IQueryable<Post> ListPosts()
    {
        return iPostRepository.GetMany();  // Returns the list of posts
    }
}