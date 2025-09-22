
using System.Text.Json;

public class UserFileRepository : IUserRepository
{
     private readonly string filePath = "users.json";

    public UserFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<User> AddAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

        user.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
        users.Add(user);

        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);

        return user;
    }


    public async Task UpdateAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

        User? existingUser = users.SingleOrDefault(u => u.Id == u.Id);

        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with ID '{user.Id}' not found");
        }

        users.Remove(existingUser);
        users.Add(user);
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
    }

    public async Task DeleteAsync(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

        User? existingUser = users.SingleOrDefault(u => u.Id == id);

        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }

        users.Remove(existingUser);
         usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
    }

    public async Task<User> GetSingleAsync(int id)
    {
         string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        User? existingUser = users.SingleOrDefault(u => u.Id == id);

        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }

        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
        return existingUser;
    }

    public IQueryable<User> GetManyAsync()
    {
        string usersAsJson = File.ReadAllTextAsync(filePath).Result;
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        return users.AsQueryable();
    }
}

