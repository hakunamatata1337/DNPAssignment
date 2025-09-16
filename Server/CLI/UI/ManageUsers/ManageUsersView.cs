using System.Threading.Tasks;

public class ManageUsersView(IUserRepository userRepository)
{
    private readonly IUserRepository userRepository = userRepository;


    public async Task StartAsync()
    {
        Console.WriteLine("What do you want to do ?");
        Console.WriteLine("1. Create new user");

        string userInput = Console.ReadLine() ?? string.Empty;

        switch (userInput)
        {
            case "1":
                CreateUserView createUserView = new CreateUserView(userRepository);
                await createUserView.CreateUser();
                break;
            default:
                break;
         }
    }
}