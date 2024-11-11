using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileReposity : IUserRepository
{

    private readonly string filePath = "users.json";


    public UserFileReposity()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    
    public async Task<User> AddAsync(User user)
    {
      string userasjson = await File.ReadAllTextAsync(filePath);
      List<User> users = JsonSerializer.Deserialize<List<User>>(userasjson);
      int maxId = users.Count > 0 ? users.Max(p => p.Id) : 1;
      user.Id = maxId + 1;
      users.Add(user);
      userasjson = JsonSerializer.Serialize(users);
      await File.WriteAllTextAsync(filePath, userasjson);
      return user;
    }

    public async Task UpdateAsync(User user)
    {
        string userasjson = JsonSerializer.Serialize(user);
        List<User> users = JsonSerializer.Deserialize<List<User>>(userasjson);
        
        User? userToUpdate = users.FirstOrDefault(p => p.Id == user.Id);
        if (userToUpdate != null)
        {
            userToUpdate.Username = user.Username;
            userToUpdate.Password = user.Password;
            
            userasjson = JsonSerializer.Serialize(userToUpdate);
            
            await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(userasjson));
            
        }
        else
        {
            throw new KeyNotFoundException($"Post with Id {user.Id} not found.");
        }
        
        
    }

    public async Task DeleteAsync(int id)
    {
       string userasjson = File.ReadAllText(filePath);
       List<User> users = JsonSerializer.Deserialize<List<User>>(userasjson);
       User? userToDelete = users.FirstOrDefault(p => p.Id == id);

       if (userToDelete !=null)
       {
           users.Remove(userToDelete);
           userasjson = JsonSerializer.Serialize(users);
           await File.WriteAllTextAsync(filePath, userasjson);
       }
       throw new KeyNotFoundException($"Post with Id {id} not found.");
    }

    public async Task<User> GetSingleAsync(int id)
    {
        string userasjson = File.ReadAllText(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(userasjson);
        User? user = users.FirstOrDefault(p => p.Id == id);

       return user ?? throw new KeyNotFoundException($"Post with Id {id} not found.");
    }

    public IQueryable<User> GetMany()
    {
      string userasjson = File.ReadAllText(filePath);
      List<User> users = JsonSerializer.Deserialize<List<User>>(userasjson);
      return users.AsQueryable();
      
    }

    public Task<User> GetByUsernameAsync(string username)
    {
        string userasjson = File.ReadAllText(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(userasjson);
        User? user = users.FirstOrDefault(p => p.Username == username);
        return user != null ? Task.FromResult(user) : throw new KeyNotFoundException($"Post with Id {username} not found.");
    }
}