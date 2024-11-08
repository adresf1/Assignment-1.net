

namespace BlazorApp12.Services;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ApiContracts;

public class HttpsPostService : IPostService
{
    private readonly HttpClient _httpClient;

    public HttpsPostService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CreatPostDTO> AddAsync(CreatPostDTO post)
    {
        var response = await _httpClient.PostAsJsonAsync("api/posts", post);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CreatPostDTO>();
    }

    public Task UpdateAsync(PostDTO post)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        var response = _httpClient.DeleteAsync($"api/posts/{id}");
        response.Result.EnsureSuccessStatusCode();
        return Task.CompletedTask;
    }

    public Task<PostDTO> GetSingleAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<PostsDTO>> GetMany()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/posts");
        
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<PostsDTO>>() ?? new List<PostsDTO>();
            }
            else
            {
                return new List<PostsDTO>();  
            }
        }
        catch (Exception ex)
        {
            return new List<PostsDTO>();  
        }
    }
}