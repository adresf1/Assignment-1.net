using System.Text.Json;
using ApiContracts;


namespace BlazorApp12.Services;

public class HtttpsUserService : IUserService
{
    public readonly HttpClient client;

    public HtttpsUserService(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task<UserDTO> AddUserAsync(UserDTO request)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("users", request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<UserDTO>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public async Task<UserDTO> GetUserByIdAsync(int id)
    {
        // Send en GET-anmodning til "users/{id}"-endepunktet
        HttpResponseMessage httpResponse = await client.GetAsync($"users/{id}");

        // Læs svaret fra serveren
        string response = await httpResponse.Content.ReadAsStringAsync();

        // Tjek, om anmodningen var vellykket
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        // Deserialiser JSON-svaret til et UserDTO-objekt og returner det
        return JsonSerializer.Deserialize<UserDTO>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public async Task UpdateUserAsync(int id, UserDTO request)
    {
        HttpResponseMessage getResponse = await client.GetAsync($"users/{id}");
        if (!getResponse.IsSuccessStatusCode)
        {
            throw new Exception($"User with ID {id} not found.");
        }
        
        HttpResponseMessage httpResponseMessage = await client.PutAsJsonAsync($"users/{id}", request);

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            string errorResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            throw new Exception(errorResponse);
        }
        
    }

    public async Task DeleteUserAsync(int id)
    {
        HttpResponseMessage getmessege = await client.DeleteAsync($"users/{id}");
        if (!getmessege.IsSuccessStatusCode)
        {
            throw new Exception($"User with ID {id} not found.");
        }

        HttpResponseMessage httpResponseMessage  = await client.DeleteAsync($"users/{id}");
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            string errorResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            throw new Exception(errorResponse);
        }
    }

    public async Task<List<UserDTO>> GetAllUsersAsync()
    {
       
        HttpResponseMessage httpResponseMessage = await client.GetAsync("users");
    
       
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            string errorResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            throw new Exception($"Failed to retrieve users: {errorResponse}");
        }

        
        string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();

     
        return JsonSerializer.Deserialize<List<UserDTO>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

}
