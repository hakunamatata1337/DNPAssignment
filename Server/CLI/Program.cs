

using InMemoryRepositories;

Console.WriteLine("Starting CLI app..");
IPostRepository postRepository = new PostInMemoryRepository();
IUserRepository userRepository = new UserInMemoryRepository();
ICommentRepository commentRepository = new CommentInMemoryRepository();


CliApp cliApp = new CliApp(postRepository, userRepository, commentRepository);
await cliApp.StartAsync();
