using ApiContracts;

//@TODO handle null exceptions
public class HttpUsersService : IUsersService
{
    private readonly HttpClient client;

    public HttpUsersService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<UserDTO> GetUserById(int id)
    {
        return await client.GetFromJsonAsync<UserDTO>($"/users/{id}");
    }

    public async Task<UserDTO> AddUser(CreateUserDTO createUserDTO)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/users", createUserDTO);
        response.EnsureSuccessStatusCode();

        UserDTO createdUser = await response.Content.ReadFromJsonAsync<UserDTO>();
        return createdUser;
    }
}