using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepostory
{
    private  List<Comment> comments = new List<Comment>();
    public Task<Comment> AddAsync(Comment comment)
    {
        comment.Id = comments.Any() 
            ? comments.Max(p => p.Id) + 1
            : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comment comment)
    {
        Comment? existingPost = comments.SingleOrDefault(p => p.Id == comment.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{comment.Id}' not found");
        }

        comments.Remove(existingPost);
        comments.Add(comment);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Comment? postToRemove = comments.SingleOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        comments.Remove(postToRemove);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        return Task.FromResult(comments.FirstOrDefault(p => p.Id == id));
        // Do implementation
        //  return Task.FromResult(post);
    }

    public IQueryable<Comment> GetMany()
    {
        return comments.AsQueryable();
    }
}