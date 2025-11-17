

using Microsoft.EntityFrameworkCore;

public class EfcPostRepository : IPostRepository
{
    private readonly EfcRepositories.AppContext ctx;

    public EfcPostRepository(EfcRepositories.AppContext context)
    {
        this.ctx = context;
    }

    public async Task<Post> AddAsync(Post post)
    {
        var entity = await ctx.Posts.AddAsync(post);
        await ctx.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task DeleteAsync(int id)
    {   
      Post? existing = ctx.Posts.SingleOrDefault(p => p.Id == id); 
      if (existing == null) { throw new Exception($"Post with id {id} not found"); }
       ctx.Posts.Remove(existing); 
       await ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(Post post) { 
        if(!await ctx.Posts.AnyAsync(p => p.Id == post.Id)) 
        { 
            throw new Exception($"Post with id {post.Id} not found"); 
        }

        ctx.Posts.Update(post); 
        await ctx.SaveChangesAsync();
    }

    public async Task<Post> GetSingleAsync(int id)
    {
         Post? existing = ctx.Posts.SingleOrDefault(p => p.Id == id); 
         if(existing == null) 
         { 
            throw new Exception($"Post with id {id} not found"); 
         }
        return existing;
    }

    public IQueryable<Post> GetManyAsync()
    {
        return ctx.Posts.AsQueryable();
    }
}