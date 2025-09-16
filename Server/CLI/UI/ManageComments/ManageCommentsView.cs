using System.Threading.Tasks;

public class ManageCommentsView(ICommentRepository commentRepository)
{
    private readonly ICommentRepository commentRepository = commentRepository;


    public async Task StartAsync()
    {
        Console.WriteLine("What do you want to do ?");
        Console.WriteLine("1. Add comment to existing post");

        string userInput = Console.ReadLine() ?? string.Empty;

        switch (userInput)
        {
            case "1":
                CreateCommentView createCommentView = new CreateCommentView(commentRepository);
                await createCommentView.CreateComment();
                break;
            default:
                break;
         }
    }
}