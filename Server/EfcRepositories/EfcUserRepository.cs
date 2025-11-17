

using Microsoft.EntityFrameworkCore;

public class EfcUserRepository : IUserRepository
{
    private readonly EfcRepositories.AppContext ctx;

    public EfcUserRepository(EfcRepositories.AppContext context)
    {
        this.ctx = context;
    }

    public async Task<User> AddAsync(User user)
    {
        var entity = await ctx.Users.AddAsync(user);
        await ctx.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task DeleteAsync(int id)
    {   
      User? existing = ctx.Users.SingleOrDefault(u => u.Id == id); 
      if (existing == null) { throw new Exception($"User with id {id} not found"); }
       ctx.Users.Remove(existing); 
       await ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user) { 
        if(!await ctx.Users.AnyAsync(u => u.Id == u.Id)) 
        { 
            throw new Exception($"User with id {user.Id} not found"); 
        }

        ctx.Users.Update(user); 
        await ctx.SaveChangesAsync();
    }

    public async Task<User> GetSingleAsync(int id)
    {
         User? existing = ctx.Users.SingleOrDefault(u => u.Id == id); 
         if(existing == null) 
         { 
            throw new Exception($"User with id {id} not found"); 
         }
        return existing;
    }

    public IQueryable<User> GetManyAsync()
    {
        return ctx.Users.AsQueryable();
    }
}