using ApiContracts;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostRepository postRepository;

    public PostsController(IPostRepository postRepo)
    {
        this.postRepository = postRepo;
    }

   [HttpPost]
    public async Task<ActionResult<PostDTO>> AddPost(
       [FromBody] CreatePostDTO request
   )
    {
        Post post = new() { Title = request.Title, Body = request.Body, UserId = request.UserId };
        Post created = await postRepository.AddAsync(post);
        PostDTO dto = new()
        {
            Id = created.Id,
            Title = created.Title,
            Body = created.Body,
            UserId = created.UserId
        };
        return Created($"/posts/{dto.Id}", dto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdatePost(
        int id,
        [FromBody] UpdatePostDTO request
    )
    {
        try
        {
            Post post = new() { Id = id, Title = request.Title, Body = request.Body, UserId = request.UserId };
            await postRepository.UpdateAsync(post);
            return Ok();
        } catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
   
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Post>> GetPostById(int id)
    {
        try
        {
            Post returnedPost = await postRepository.GetSingleAsync(id);
            Console.WriteLine($"Fetched post: {returnedPost.Id}");
            return Ok(returnedPost);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IQueryable<PostDTO>>> GetManyPosts([FromQuery] string? titleContains, [FromQuery] int? writtenBy)
    {
        IQueryable<Post> posts = postRepository.GetManyAsync();

        if (!string.IsNullOrEmpty(titleContains))
        {
            posts = posts.Where(p => p.Title.Contains(titleContains));
        }

        if (writtenBy.HasValue)
        {
            posts = posts.Where(p => p.UserId == writtenBy);
        }

        var dtos =posts.Select(p => new PostDTO { Id = p.Id, Title = p.Title, Body = p.Body, UserId = p.UserId });

        return Ok(dtos);
    }



    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeletePost(int id)
    {
        try
        {
            await postRepository.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}