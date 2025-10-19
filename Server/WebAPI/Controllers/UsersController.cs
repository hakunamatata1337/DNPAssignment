using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository userRepository;

    public UsersController(IUserRepository userRepo)
    {
        this.userRepository = userRepo;
    }

    [HttpPost]
    public async Task<ActionResult<UserDTO>> AddUser(
       [FromBody] CreateUserDTO request
   )
    {
        User user = new() { Username = request.Username, Password = request.Password };
        User created = await userRepository.AddAsync(user);
        UserDTO dto = new()
        {
            Id = created.Id,
            Username = created.Username
        };
        return Created($"/users/{dto.Id}", dto);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<UserDTO>> UpdateUser(
        int id,
        [FromBody] UpdateUserDTO request
    )
    {
        try
        {
            User user = new() { Id = id, Username = request.Username, Password = request.Password };
            await userRepository.UpdateAsync(user);
            return Ok();
        } catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
   
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDTO>> GetUserById(int id)
    {
        try
        {
            User returnedUser = await userRepository.GetSingleAsync(id);
            UserDTO dto = new()
            {
                Id = returnedUser.Id,
                Username = returnedUser.Username
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
    public async Task<ActionResult> GetManyUsers([FromQuery]  string contains)
    {
        IQueryable<User> users = userRepository.GetManyAsync();
        if (!string.IsNullOrEmpty(contains))
        {
            users = users.Where(u => u.Username.Contains(contains));
        }

        var dtos =users.Select(u => new UserDTO { Id = u.Id, Username = u.Username });
        return Ok(dtos);
    }



    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        try
        {
            await userRepository.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}