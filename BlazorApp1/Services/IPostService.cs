using ApiContracts;

namespace BlazorApp1.Services;

public interface IPostService
{
    Task<PostDTO> AddAsync(PostDTO post);
    Task UpdateAsync(PostDTO post);
    Task DeleteAsync(int id);
    Task<PostDTO> GetSingleAsync(int id);
    IQueryable<PostDTO> GetMany();
}
