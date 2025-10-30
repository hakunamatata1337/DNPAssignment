public interface IUsersService
{
    public Task<UserDTO> GetUserById(int id);

    public Task<UserDTO> AddUser(CreateUserDTO createUserDTO);
}