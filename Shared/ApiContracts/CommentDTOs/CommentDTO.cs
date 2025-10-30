public class CommentDTO
{
    required public int Id { get; set; }
    required public string Body { get; set; }

    required public int UserId { get; set; }
    required public int PostId { get; set; }
}
