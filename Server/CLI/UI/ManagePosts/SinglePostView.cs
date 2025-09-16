using System.Threading.Tasks;

public class SinglePostView(IPostRepository postRepository, ICommentRepository commentRepository)
{ 
    private readonly IPostRepository postRepository = postRepository;
    private readonly ICommentRepository commentRepository = commentRepository;


    public async Task ViewSinglePost()
    {
        Console.WriteLine("Input post id");

        string inputPostId = Console.ReadLine() ?? string.Empty;
        int parsedPostId = Convert.ToInt32(inputPostId);


        Post post = await postRepository.GetSingleAsync(parsedPostId);
        IQueryable<Comment> commentsQuery = commentRepository.GetManyAsync();
        List<Comment> comments = commentsQuery.ToList();

        IEnumerable<Comment> postComments = comments.Where(c => c.PostId == parsedPostId);

        Console.WriteLine("-------------------");
        Console.WriteLine($"Id: {post.Id}");
        Console.WriteLine($"Title: {post.Title}");
        Console.WriteLine($"Body: {post.Body}");
        Console.WriteLine($"User id: {post.UserId}");
        Console.WriteLine("Comments: ");
        foreach (var comment in postComments)
        {
            Console.WriteLine("-------------------");
            Console.WriteLine($"User id: {comment.UserId}");
            Console.WriteLine($"Body: {comment.Body}");
            Console.WriteLine("-------------------");
        }
        Console.WriteLine("-------------------");
    }
}