using ApiContracts;

namespace BlazorApp1.Services;

public interface IUserService
{
    // Create: Tilføjer en ny bruger baseret på data fra CreateUserDto
    Task<UserDTO> AddUserAsync(UserDTO request);

    // Read: Henter en bruger baseret på et specifikt ID
    Task<UserDTO> GetUserByIdAsync(int id);

    // Update: Opdaterer en eksisterende bruger baseret på ID og data fra UpdateUserDto
    Task UpdateUserAsync(int id, UserDTO request);

    // Delete: Sletter en bruger baseret på et specifikt ID
    Task DeleteUserAsync(int id);

    // List: Henter en liste over alle brugere
    Task<List<UserDTO>> GetAllUsersAsync();
}