using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatCommentView
{
    private ICommentRepostory _commentRepostory;

    public CreatCommentView(ICommentRepostory commentRepostory)
    {
        _commentRepostory = commentRepostory;
    }

    public async Task<Comment> AddAsync(String body)
    {
        
        Comment comment = new Comment
        {
            Body = body
        };

        return await _commentRepostory.AddAsync(comment); 
    }
}