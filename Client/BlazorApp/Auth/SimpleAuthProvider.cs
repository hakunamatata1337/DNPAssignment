using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

public class SimpleAuthProvider : AuthenticationStateProvider
{
    private readonly HttpClient httpClient;

    private ClaimsPrincipal currentClaimsPrincipal; 
    
    public SimpleAuthProvider(HttpClient httpClient) { this.httpClient = httpClient; }

    public async Task Login(int userId, string password)
    {
        var loginDto = new LoginUserDTO
        {
            Id = userId,
            Password = password
        };

        var response = await httpClient.PostAsJsonAsync("Auth/login", loginDto);

        if (response.IsSuccessStatusCode)
        {
            var userDto = await response.Content.ReadFromJsonAsync<UserDTO>() ?? throw new Exception("Failed to parse user data.");

            var claims = new List<Claim>
            {
                new Claim("Id", userDto.Id.ToString()),
                new Claim(ClaimTypes.Name, userDto.Username)
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth");
            currentClaimsPrincipal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(currentClaimsPrincipal)));
        }
        else
        {
            throw new Exception("Login failed");
        }
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(new AuthenticationState(currentClaimsPrincipal ?? new()));
    }
    
    public void Logout()
    {
        currentClaimsPrincipal = new();
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(currentClaimsPrincipal)));
    }
}