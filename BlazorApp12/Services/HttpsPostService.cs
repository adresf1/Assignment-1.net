

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
        throw new NotImplementedException();
    }

    public Task<PostDTO> GetSingleAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<PostsDTO>> GetMany()
    {
        var response = await _httpClient.GetAsync("api/posts");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<PostsDTO>>();
    }
}