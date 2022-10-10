using Shared.DTOs;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    public Task<User> CreateAsync(UserCreationDto dto);
    //2
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
}