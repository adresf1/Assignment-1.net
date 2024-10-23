using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ListUsersView
{
    public readonly IUserRepository userRepository;

    public ListUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task GetMany()
    {
        var users = userRepository.GetMany();
        foreach (var user in users)
        {
            Console.WriteLine(user.ToString());
        }
    }
    
    
}