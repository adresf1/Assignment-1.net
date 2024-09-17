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
        var posts = iPostRepository.GetMany();
        return posts ?? Enumerable.Empty<Post>().AsQueryable(); 
    }
}