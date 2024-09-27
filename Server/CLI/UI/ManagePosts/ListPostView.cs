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

    public async Task ListPosts()
    {
        var posts = iPostRepository.GetMany();
        foreach (var post in posts)
        {
            Console.WriteLine(post.tostring());
        }
    }
}