using ApiContracts;

public interface IPostsService
{
    Task<List<PostDTO>> GetPostsAsync();

    Task<PostDTO> GetPostByIdAsync(int id); 

    Task<PostDTO> CreatePostAsync(CreatePostDTO createPostDTO);
}   