using System.Text.Json;

namespace FileRepositories;



public class CommentFileRepository : ICommentRepository
{
    private readonly string filePath = "comments.json";
     //@dev used to prevent issues with concurrent file access
    private static readonly object _fileLock = new();
    public CommentFileRepository()
    {
        
        lock (_fileLock)
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }
        }
    }

        public async Task<Comment> AddAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

        comment.Id = comments.Any() ? comments.Max(c => c.Id) + 1 : 1;
        comments.Add(comment);

        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);

        return comment;
    }

    
    public async Task UpdateAsync(Comment comment)
    {
          string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

        Comment? existingComment = comments.SingleOrDefault(c => c.Id == comment.Id);

        if (existingComment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{comment.Id}' not found");
        }

        comments.Remove(existingComment);
        comments.Add(comment);

         commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }

    public async Task DeleteAsync(int id)
    {
          string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

        Comment? existingComment = comments.SingleOrDefault(c => c.Id == id);

        if (existingComment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{id}' not found");
        }

        comments.Remove(existingComment);
         commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
          string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

        Comment? existingComment = comments.SingleOrDefault(c => c.Id == id);

        if (existingComment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{id}' not found");
        }
        
        return existingComment;
    }

    public IQueryable<Comment> GetManyAsync()
    {
        string commentsAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        return comments.AsQueryable();
    }
}
