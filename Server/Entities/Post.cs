using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

public class Post
{
    public int Id { get; set; }
    required public string Title { get; set; }
    required public string Body { get; set; }
    public int UserId { get; set; }

    public User User { get; set; } = null!;
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    private Post()
    {
        
    }
    [SetsRequiredMembers]
    [JsonConstructor]
    public Post(int id, string title, string body,  int userId)
    {
        Id = id;
        Title = title;
        Body = body;
        UserId = userId;
    }

    [SetsRequiredMembers]
    public Post( string title, string body,  int userId)
    {
        Title = title;
        Body = body;
        UserId = userId;
    }
}
