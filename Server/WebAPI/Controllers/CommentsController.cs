using ApiContracts;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository commentRepository;

    public CommentsController(ICommentRepository commentRepo)
    {
        commentRepository = commentRepo;
    }

   [HttpPost]
    public async Task<ActionResult<CommentDTO>> AddComment(
       [FromBody] CreateCommentDTO request
   )
    {
        Comment comment = new (request.Body,request.UserId, request.PostId);
        Comment created = await commentRepository.AddAsync(comment);

        CommentDTO dto = new()
        {
            Id = created.Id,
            Body = created.Body,
            UserId = created.UserId,
            PostId = created.PostId
        };
        return Created($"/comments/{dto.Id}", dto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateComment(
        int id,
        [FromBody] UpdateCommentDTO request
    )
    {
        try
        {
            Comment comment = new(id, request.Body, request.PostId, request.UserId );
            await commentRepository.UpdateAsync(comment);
            return Ok();
        } catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
   
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CommentDTO>> GetCommentById(int id)
    {
        try
        {
            Comment returnedComment = await commentRepository.GetSingleAsync(id);

            CommentDTO dto = new()
            {
                Id = returnedComment.Id,
                Body = returnedComment.Body,
                UserId = returnedComment.UserId,
                PostId = returnedComment.PostId
            };

            return Ok(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IQueryable<CommentDTO>>> GetManyComments([FromQuery] int? postId, [FromQuery] int? userId)
    {
        IQueryable<Comment> comments = commentRepository.GetManyAsync();

        if (postId.HasValue)
        {
            comments = comments.Where(c => c.PostId == postId);
        }

        if (userId.HasValue)
        {
            comments = comments.Where(c => c.UserId == userId);
        }

        var dtos =comments.Select(c => new CommentDTO { 
                Id = c.Id,
                Body = c.Body,
                UserId = c.UserId,
                PostId = c.PostId });

        return Ok(dtos);
    }



    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteComment(int id)
    {
        try
        {
            await commentRepository.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}