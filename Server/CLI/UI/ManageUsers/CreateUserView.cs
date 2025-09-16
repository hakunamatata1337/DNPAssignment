
public class CreateUserView(IUserRepository userRepository)
{
    private readonly IUserRepository userRepository = userRepository;


    public async Task CreateUser()
    {
        Console.WriteLine("Input username");

        string inputUsername = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Input password");

        string inputPassword = Console.ReadLine() ?? string.Empty;

        await userRepository.AddAsync(new User { Username = inputUsername, Password = inputPassword });
    }
}