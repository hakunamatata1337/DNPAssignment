
namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    List<User> users = new List<User>();

    public UserInMemoryRepository()
    {
        users.Add(new User { Id = 0, Username = "Maciek", Password = "123" });
        users.Add(new User { Id = 1, Username = "Adam", Password = "456" });
        users.Add(new User { Id = 2, Username = "Jakub", Password = "789" });
    }

    public Task<User> AddAsync(User user)
    {
        user.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
        users.Add(user);

        return Task.FromResult(user);
    }


    public Task UpdateAsync(User user)
    {
        User? existingUser = users.SingleOrDefault(u => u.Id == u.Id);

        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with ID '{user.Id}' not found");
        }

        users.Remove(existingUser);
        users.Add(user);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        User? existingUser = users.SingleOrDefault(u => u.Id == id);

        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }

        users.Remove(existingUser);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        User? existingUser = users.SingleOrDefault(u => u.Id == id);

        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }

        return Task.FromResult(existingUser);
    }

    public IQueryable<User> GetManyAsync()
    {
        return users.AsQueryable();
    }
}

