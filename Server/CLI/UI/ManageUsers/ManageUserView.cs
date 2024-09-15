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

    public Task UpdateAsync(User user)
    {
        return userRepository.UpdateAsync(user);
    }
    
}