using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcUserRepostory : IUserRepository
{
    private readonly AppDbContext ctx;

    public EfcUserRepostory(AppDbContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<User> AddAsync(User user)
    {
        // add the user and save
        EntityEntry<User> entityEntry =  ctx.Users.Add(user);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(User user)
    {
        if (!(await ctx.Users.AnyAsync(p => p.Id == user.Id)))
        {
            throw new ("Post with id {post.Id} not found");
        }

        ctx.Users.Update(user);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        User? existing = await ctx.Users.SingleOrDefaultAsync(p => p.Id == id);
        if (existing == null)
        {
            throw new ("Post with id {id} not found");
        }

        ctx.Users.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public async Task<User> GetSingleAsync(int id)
    {
        return await ctx.Users.SingleOrDefaultAsync(p => p.Id == id);}

    public IQueryable<User> GetMany()
    {
        return ctx.Users.AsQueryable();
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        return await ctx.Users.SingleOrDefaultAsync(p => p.Username == username);} 
}

