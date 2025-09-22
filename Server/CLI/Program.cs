

using FileRepositories;

Console.WriteLine("Starting CLI app..");
IPostRepository postRepository = new PostFileRepository();
IUserRepository userRepository = new UserFileRepository();
ICommentRepository commentRepository = new CommentFileRepository();


CliApp cliApp = new CliApp(postRepository, userRepository, commentRepository);
await cliApp.StartAsync();
