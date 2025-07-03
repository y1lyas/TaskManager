using Microsoft.AspNetCore.Identity;
using TaskManager.Models;

public interface IUserService
{
    Task<ApplicationUser?> FindByEmailAsync(string email);
}

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationUser?> FindByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }
}
