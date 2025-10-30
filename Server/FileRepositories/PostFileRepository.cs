
using System.Text.Json;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
   private readonly string filePath = "posts.json";

    public PostFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
            AddAsync(new Post { Title = "First Post", Body = "This is the body of the first post", UserId = 1 }).Wait();
        }
    }
    public async Task<Post> AddAsync(Post post)
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;


        post.Id = posts.Any() ? posts.Max(p => p.Id) + 1 : 1;
        posts.Add(post);

        postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postsAsJson);

        return post;
    }


    public async Task UpdateAsync(Post post)
    {
         string postsAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);

        if (existingPost is null)
        {
            throw new InvalidOperationException($"Post with ID '{post.Id}' not found");
        }

        posts.Remove(existingPost);
        posts.Add(post);

        postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postsAsJson);
    }

    public async Task DeleteAsync(int id)
    {
         string postsAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
        Post? existingPost = posts.SingleOrDefault(p => p.Id == id);

        if (existingPost is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found");
        }

        posts.Remove(existingPost);
        postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postsAsJson);
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
        Post? existingPost = posts.SingleOrDefault(p => p.Id == id);

        if (existingPost is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found");
        }

        postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postsAsJson);

        return existingPost;
    }

    public IQueryable<Post> GetManyAsync()
    {
        string postsAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
        return posts.AsQueryable();
    }
}

