
namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    List<Post> posts = new List<Post>();

    public PostInMemoryRepository()
    {
        posts.Add(new Post { Id = 0, Title = "Mysz", Body = "Mysz smaczna", UserId = 0});
        posts.Add(new Post { Id = 1, Title = "Kot", Body = "Kot je", UserId = 2});
        posts.Add(new Post { Id = 2, Title = "Szczur", Body = "Szczur capi", UserId = 1});
    }

    public Task<Post> AddAsync(Post post)
    {
        post.Id = posts.Any() ? posts.Max(p => p.Id) + 1 : 1;
        posts.Add(post);

        return Task.FromResult(post);
    }


    public Task UpdateAsync(Post post)
    {
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);

        if (existingPost is null)
        {
            throw new InvalidOperationException($"Post with ID '{post.Id}' not found");
        }

        posts.Remove(existingPost);
        posts.Add(post);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Post? existingPost = posts.SingleOrDefault(p => p.Id == id);

        if (existingPost is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found");
        }

        posts.Remove(existingPost);
        return Task.CompletedTask;
    }

    public Task<Post> GetSingleAsync(int id)
    {
        Post? existingPost = posts.SingleOrDefault(p => p.Id == id);

        if (existingPost is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found");
        }

        return Task.FromResult(existingPost);
    }

    public IQueryable<Post> GetManyAsync()
    {
        return posts.AsQueryable();
    }
}

