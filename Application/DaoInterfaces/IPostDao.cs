using Shared.DTOs;
using Shared.Models;

namespace Application.DaoInterfaces;

public interface IPostDao
{
    Task<Post> CreateAsync(Post post);
    //3
    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters);
    //4
    Task UpdateAsync(Post post);
    Task<Post?> GetByIdAsync(int postId);

    Task DeleteAsync(int id);
    
}