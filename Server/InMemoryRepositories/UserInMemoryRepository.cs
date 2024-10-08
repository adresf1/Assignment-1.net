﻿using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private  List<User> users = new List<User>();
    public Task<User> AddAsync(User user)
    {
        user.Id = users.Any() 
            ? users.Max(p => p.Id) + 1
            : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        User? existingPost = users.SingleOrDefault(p => p.Id == user.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{user.Id}' not found");
        }

        users.Remove(existingPost);
        users.Add(user);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        User? postToRemove = users.SingleOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException(
                $"Post with ID '{id}' not found");
        }

        users.Remove(postToRemove);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        return Task.FromResult(users.FirstOrDefault(p => p.Id == id));
        // Do implementation
        //  return Task.FromResult(post);
    }

    public IQueryable<User> GetMany()
    {
        return users.AsQueryable();
    }
}