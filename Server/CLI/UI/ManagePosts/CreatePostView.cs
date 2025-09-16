public class CreatePostView(IPostRepository postRepository)
{ 
    private readonly IPostRepository postRepository = postRepository;


    public async Task CreatePost()
    {
        Console.WriteLine("Input title");

        string inputTitle = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Input body");

        string inputBody = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Input user id");

        string inputUserId = Console.ReadLine() ?? string.Empty;
        int parsedUserId = Convert.ToInt32(inputUserId);

        await postRepository.AddAsync(new Post { Title = inputTitle, Body = inputBody, UserId = parsedUserId});
    }
}