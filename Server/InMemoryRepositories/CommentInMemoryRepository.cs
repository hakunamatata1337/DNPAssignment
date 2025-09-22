
namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
{
    List<Comment> comments = new List<Comment>();

    public CommentInMemoryRepository()
    {
        comments.Add(new Comment { Id = 0, Body = "To prawda", UserId = 1, PostId = 2 });
        comments.Add(new Comment { Id = 1, Body = "Polska przejmuje serva xdddd", UserId = 2, PostId = 0 });
        comments.Add(new Comment { Id = 2, Body = "Gowno", UserId = 2, PostId = 2 });
    }

    public Task<Comment> AddAsync(Comment comment)
    {
        comment.Id = comments.Any() ? comments.Max(c => c.Id) + 1 : 1;
        comments.Add(comment);

        return Task.FromResult(comment);
    }


    public Task UpdateAsync(Comment comment)
    {
        Comment? existingComment = comments.SingleOrDefault(c => c.Id == comment.Id);

        if (existingComment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{comment.Id}' not found");
        }

        comments.Remove(existingComment);
        comments.Add(comment);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Comment? existingComment = comments.SingleOrDefault(c => c.Id == id);

        if (existingComment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{id}' not found");
        }

        comments.Remove(existingComment);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        Comment? existingComment = comments.SingleOrDefault(c => c.Id == id);

        if (existingComment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{id}' not found");
        }

        return Task.FromResult(existingComment);
    }

    public IQueryable<Comment> GetManyAsync()
    {
        return comments.AsQueryable();
    }
}

