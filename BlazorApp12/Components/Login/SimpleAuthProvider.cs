using System.Security.Claims;
using System.Text.Json;
using ApiContracts;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorApp12.Components.Login;

public class SimpleAuthProvider : AuthenticationStateProvider
{
    private readonly HttpClient httpClient;
    private ClaimsPrincipal currentClaimsPrincipal;

    public SimpleAuthProvider(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task Login(string userName, string password)
    {
        HttpResponseMessage response = null;
        try
        {
            response = await httpClient.PostAsJsonAsync(
                "api/auth/login",
                new LoginRequest(userName, password));
        
            // Ensure the response was successful, otherwise throw an error.
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            UserDTO userDto = JsonSerializer.Deserialize<UserDTO>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userDto.Username),
                new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString())};
            ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth");
            currentClaimsPrincipal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(currentClaimsPrincipal)));
        }
        catch (HttpRequestException httpEx)
        {
            throw new Exception($"HTTP Request failed: {httpEx.Message}");
        }
        catch (JsonException jsonEx)
        {
            throw new Exception($"Failed to parse response JSON: {jsonEx.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Login failed: {ex.Message}");
        }
    }

       


    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return new AuthenticationState(currentClaimsPrincipal ?? new ());
    }
    
    public void Logout()
    {
        currentClaimsPrincipal = new();
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(currentClaimsPrincipal)));
    }
    
}