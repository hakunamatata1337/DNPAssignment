using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository userRepository;

    public AuthController(IUserRepository userRepo)
    {
        userRepository = userRepo;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO request)
    {
       
        var user =await userRepository.GetSingleAsync(request.Id);
        if (user.Password != request.Password)
        {
            return Unauthorized("Invalid password.");
        }

        UserDTO userDTO = new UserDTO
        {
            Id = user.Id,
            Username = user.Username
        };
        return Ok(userDTO);
    }
}