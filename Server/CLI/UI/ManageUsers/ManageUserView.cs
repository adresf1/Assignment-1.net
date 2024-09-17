using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUserView
{
    public readonly IUserRepository userRepository;

    public ManageUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public Task UpdateAsync(String username, String password, int userId)
    {
        if (username == null){ throw new ArgumentNullException(nameof(username));}

        User user = new User()
        {
            Username = username,
            Password = password,
            Id = userId
        };
        return userRepository.UpdateAsync(user);
    }
    
       
    
    
}