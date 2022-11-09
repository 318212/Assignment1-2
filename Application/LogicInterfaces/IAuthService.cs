using Shared.DTOs;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IAuthService
{
    Task<User> LoginAsync(UserLoginDto dto);
}