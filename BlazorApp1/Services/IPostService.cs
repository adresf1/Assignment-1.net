using ApiContracts;

namespace BlazorApp1.Services;

public interface IPostService
{
    Task<CreatPostDTO> AddAsync(CreatPostDTO post);
    Task UpdateAsync(PostDTO post);
    Task DeleteAsync(int id);
    Task<PostDTO> GetSingleAsync(int id);
    Task<List<PostsDTO>> GetMany();
}
