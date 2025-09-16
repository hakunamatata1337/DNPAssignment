
public class CreateCommentView(ICommentRepository commentRepository)
{
    private readonly ICommentRepository commentRepository = commentRepository;


    public async Task CreateComment()
    {
        Console.WriteLine("Input user id");

        string inputUserId = Console.ReadLine() ?? string.Empty;
        int parsedUserId = Convert.ToInt32(inputUserId);

        Console.WriteLine("Input post id");

        string inputPostId = Console.ReadLine() ?? string.Empty;
        int parsedPostId = Convert.ToInt32(inputPostId);

        Console.WriteLine("Input body");

        string inputBody = Console.ReadLine() ?? string.Empty;

        await commentRepository.AddAsync(new Comment { UserId = parsedUserId, PostId = parsedPostId, Body = inputBody});
    }
}