using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace Application.Logic;

public class AuthService : IAuthService
{
    private readonly IUserDao userDao;

    public AuthService(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    public async Task<User> LoginAsync(UserLoginDto dto)
    {
        User? existing = await userDao.GetByUsernameAsync(dto.Username);

        if (existing == null)
        {
            throw new Exception("Account with this username exists");
        }

        if (!dto.Password.Equals(existing.Password))
        {
            throw new Exception("Password incorrect");
        }
        return existing;
    }
}