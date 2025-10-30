using ApiContracts;

//@TODO handle null exceptions
public class HttpPostsService : IPostsService
{
    private readonly HttpClient client;

    public HttpPostsService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<List<PostDTO>> GetPostsAsync()
    {
        return await client.GetFromJsonAsync<List<PostDTO>>("/posts");
    }

    public async Task<PostDTO> GetPostByIdAsync(int id)
    {
        return await client.GetFromJsonAsync<PostDTO>($"/posts/{id}");
    }

    public async Task<PostDTO> CreatePostAsync(CreatePostDTO createPostDTO)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/posts", createPostDTO);
        response.EnsureSuccessStatusCode();

        PostDTO createdPost = await response.Content.ReadFromJsonAsync<PostDTO>();
        return createdPost;
    }
}