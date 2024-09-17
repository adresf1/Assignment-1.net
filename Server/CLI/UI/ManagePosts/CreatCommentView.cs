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

    public async Task<Comment> AddAsync(int id, String body)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID must be greater than 0", nameof(id));
        }
    
        

        Comment comment = new Comment
        {
            Id = id,
            Body = body
        };

        return await _commentRepostory.AddAsync(comment); 
    }
}