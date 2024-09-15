using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreatUserView
{
    public readonly IUserRepository userRepository;

    public CreatUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    
    private async Task AddUserAsync(string name, string password)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Name and password are required.");
        }

        // Create user object
        User user = new User
        {
            Username = name,
            Password = password
        };

        var existingUser = await userRepository.GetSingleAsync(user.Id);
        
        if (existingUser.Username != null)
        {
            // Throw an exception if the user already exists
            throw new InvalidOperationException("A user with this username already exists.");
        }
        
        
        User created = await userRepository.AddAsync(user);
    }
    
}