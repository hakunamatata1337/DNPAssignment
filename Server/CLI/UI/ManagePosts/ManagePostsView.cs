using System.Threading.Tasks;

public class ManagePostsView(IPostRepository postRepository, ICommentRepository commentRepository)
{
    private readonly IPostRepository postRepository = postRepository;
    private readonly ICommentRepository commentRepository = commentRepository;

    public async Task StartAsync()
    {
        Console.WriteLine("What do you want to do ?");
        Console.WriteLine("1. Create new post");
        Console.WriteLine("2. List all posts");
        Console.WriteLine("3. View single post");

        string userInput = Console.ReadLine() ?? string.Empty;

        switch (userInput)
        {
            case "1":
                CreatePostView createPostView = new CreatePostView(postRepository);
                await createPostView.CreatePost();
                break;
            case "2":
                ListPostView listPostView = new ListPostView(postRepository);
                listPostView.ListPost();
                break;
            case "3":
                SinglePostView singlePostView = new SinglePostView(postRepository, commentRepository);
                await singlePostView.ViewSinglePost();
                break;      
            default:
                break;
         }
    }
}