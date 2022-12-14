using Shared.DTOs;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDto dto);
    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters);
    //3
    Task UpdateAsync(PostUpdateDto dto);
    //4
    Task DeleteAsync(int id);
    
    Task<IEnumerable<Post>> GetByUserIdAsync(int id);

    Task<Post> GetByIdAsync(int? id);
}