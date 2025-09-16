public class ListPostView(IPostRepository postRepository)
{ 
    private readonly IPostRepository postRepository = postRepository;


    public void ListPost()
    {
        IQueryable<Post> postsQuery = postRepository.GetManyAsync();
        List<Post> posts = postsQuery.ToList();

        foreach (var post in posts)
        {
            Console.WriteLine("-------------------");
            Console.WriteLine($"Id: {post.Id}");
            Console.WriteLine($"Title: {post.Title}");
            Console.WriteLine($"Body: {post.Body}");
            Console.WriteLine($"User id: {post.UserId}");
            Console.WriteLine("-------------------");
        }
    }
}