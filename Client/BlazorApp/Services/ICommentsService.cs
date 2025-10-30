public interface ICommentsService
{
    Task<List<CommentDTO>> GetCommentsByPostIdAsync(int postId);


    Task<CommentDTO> AddCommentAsync(CreateCommentDTO createCommentDTO);
}