using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

public class Comment
{
    public int Id { get; set; }
    public required string Body { get; set; }

    public int UserId { get; set; }
    public int PostId { get; set; }

    public User User { get; set; } = null!; 
    public Post Post { get; set; } = null!;


    private Comment() { }


    [SetsRequiredMembers]
    [JsonConstructor]
    public Comment(int id, string body, int userId, int postId)
    {
        Id = id;
        Body = body;
        UserId = userId;
        PostId = postId;
    }

    [SetsRequiredMembers]
     public Comment( string body, int userId, int postId)
    {
        Body = body;
        UserId = userId;
        PostId = postId;
    }
}
