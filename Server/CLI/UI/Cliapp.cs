using CLI.UI.ManagePosts;
using RepositoryContracts;

namespace CLI.UI;

public class Cliapp
{
    private readonly ICommentRepostory  _commentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPostRepository _postRepository;

    public Cliapp(ICommentRepostory commentRepository, IUserRepository userRepository, IPostRepository postRepository)
    {
        // Tildel constructorens parametre til felterne
        _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
    }

    public async Task StartAsync()
    {
        _userRepository.GetMany();
        _postRepository.GetMany();
        _commentRepository.GetMany();
         
    }
}