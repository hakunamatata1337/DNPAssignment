using ApiContracts;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository commentRepository;

    public CommentsController(ICommentRepository commentRepo)
    {
        this.commentRepository = commentRepo;
    }

   [HttpPost]
    public async Task<ActionResult<Comment>> AddComment(
       [FromBody] CreateCommentDTO request
   )
    {
        Comment comment = new() { Body = request.Body, UserId = request.UserId, PostId = request.PostId};
        Comment created = await commentRepository.AddAsync(comment);
        return Created($"/comments/{created.Id}", created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateComment(
        int id,
        [FromBody] UpdateCommentDTO request
    )
    {
        try
        {
            Comment comment = new() { Id=id, Body = request.Body, PostId = request.PostId, UserId = request.UserId };
            await commentRepository.UpdateAsync(comment);
            return Ok();
        } catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
   
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Comment>> GetCommentById(int id)
    {
        try
        {
            Comment returnedComment = await commentRepository.GetSingleAsync(id);

            return Ok(returnedComment);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IQueryable<Comment>>> GetManyComments([FromQuery] int? postId, [FromQuery] int? userId)
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

        return Ok(comments);
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