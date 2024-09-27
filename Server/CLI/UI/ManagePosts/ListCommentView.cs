using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListCommentView
{
    private readonly ICommentRepostory _commentRepostory;

    public ListCommentView(ICommentRepostory commentRepostory)
    {
        _commentRepostory = commentRepostory;
    }
    public async Task ListComments()
    {
        var comments = _commentRepostory.GetMany();
        foreach (var comment in comments)
        {
            Console.WriteLine(comment.tostring());
        }
    }
}