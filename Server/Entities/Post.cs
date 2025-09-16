public class Post
{
    public int Id { get; set; }
    required public string Title { get; set; }
    required public string Body { get; set; }
    public int UserId { get; set; }
}
