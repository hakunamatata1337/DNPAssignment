

using Microsoft.EntityFrameworkCore;

public class EfcCommentRepository : ICommentRepository
{
    private readonly EfcRepositories.AppContext ctx;

    public EfcCommentRepository(EfcRepositories.AppContext context)
    {
        this.ctx = context;
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        var entity = await ctx.Comments.AddAsync(comment);
        await ctx.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task DeleteAsync(int id)
    {   
      Comment? existing = ctx.Comments.SingleOrDefault(c => c.Id == id); 
      if (existing == null) { throw new Exception($"Comment with id {id} not found"); }
       ctx.Comments.Remove(existing); 
       await ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(Comment comment) { 
        if(!await ctx.Comments.AnyAsync(c => c.Id == c.Id)) 
        { 
            throw new Exception($"Comment with id {comment.Id} not found"); 
        }

        ctx.Comments.Update(comment); 
        await ctx.SaveChangesAsync();
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
         Comment? existing = ctx.Comments.SingleOrDefault(c => c.Id == id); 
         if(existing == null) 
         { 
            throw new Exception($"User with id {id} not found"); 
         }
        return existing;
    }

    public IQueryable<Comment> GetManyAsync()
    {
        return ctx.Comments.AsQueryable();
    }
}