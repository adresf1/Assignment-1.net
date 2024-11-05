using ApiContracts;

namespace BlazorApp1.Services;

public class HttpsPostService : IPostService
{
    public Task<PostDTO> AddAsync(PostDTO post)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(PostDTO post)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<PostDTO> GetSingleAsync(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<PostDTO> GetMany()
    {
        throw new NotImplementedException();
    }
}