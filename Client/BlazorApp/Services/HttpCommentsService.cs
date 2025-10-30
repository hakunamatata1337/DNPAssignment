using ApiContracts;

//@TODO handle null exceptions
public class HttpCommentsService : ICommentsService
{
    private readonly HttpClient client;

    public HttpCommentsService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<List<CommentDTO>> GetCommentsByPostIdAsync(int postId)
    {
        return await client.GetFromJsonAsync<List<CommentDTO>>($"/comments?postId={postId}") ?? new List<CommentDTO>();
    }

    public async Task<CommentDTO> AddCommentAsync(CreateCommentDTO createCommentDTO)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/comments", createCommentDTO);
        response.EnsureSuccessStatusCode();

        CommentDTO createdComment = await response.Content.ReadFromJsonAsync<CommentDTO>();
        return createdComment;
    }
}