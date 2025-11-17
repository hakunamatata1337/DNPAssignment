using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }

    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    private User()
    {
        
    }

    [SetsRequiredMembers]
    [JsonConstructor]
    public User(int id, string username, string password) 
    {
        Id = id;
        Username = username;
        Password = password;
    }
    [SetsRequiredMembers]
    public User( string username, string password) 
    {
        Username = username;
        Password = password;
    }
}
