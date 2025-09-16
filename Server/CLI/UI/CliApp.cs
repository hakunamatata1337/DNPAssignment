using System.Threading.Tasks;

public class CliApp(IPostRepository postRepository, IUserRepository userRepository, ICommentRepository commentRepository)
{
    private IPostRepository  postRepository = postRepository;
    private IUserRepository  userRepository = userRepository;
    private ICommentRepository commentRepository = commentRepository;



    public async Task StartAsync()
    {
        while (true)
        {


            Console.WriteLine("What do you whish to do ?:");
            Console.WriteLine("1. Manage users");
            Console.WriteLine("2. Manage posts");
            Console.WriteLine("3. Manage comments");
            Console.WriteLine("4. Exit");
            string userInput = Console.ReadLine() ?? string.Empty;

            switch (userInput)
            {
                case "1":
                    ManageUsersView manageUsersView = new ManageUsersView(userRepository);
                    await manageUsersView.StartAsync();
                    break;
                case "2":
                    ManagePostsView managePostsView = new ManagePostsView(postRepository, commentRepository);
                    await managePostsView.StartAsync();
                    break;
                case "3":
                    ManageCommentsView manageCommentsView = new ManageCommentsView(commentRepository);
                    await manageCommentsView.StartAsync(); 
                    break;
                case "4":
                    Console.WriteLine("Exiting Program...");
                    return;
                default:
                    break;
            }
        }
    }
}