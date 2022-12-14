using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao postDao;
    private readonly IUserDao userDao;

    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        this.postDao = postDao;
        this.userDao = userDao;
    }

    public async Task<Post> CreateAsync(PostCreationDto dto)
    {
        User? user = await userDao.GetByIdAsync(dto.OwnerId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerId} was not found.");
        }

        Post post = new Post(user, dto.Title, dto.Content);
        ValidatePost(post);
        
        Post created = await postDao.CreateAsync(post);
        return created;
    }

    private void ValidatePost(Post dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty");
        if (string.IsNullOrEmpty(dto.Content)) throw new Exception("Content cannot be empty");
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        return postDao.GetAsync(searchParameters);
    }

    public async Task UpdateAsync(PostUpdateDto dto)
    {
        Post? existing = await postDao.GetByIdAsync(dto.Id);

        if (existing == null)
        {
            throw new Exception($"Post with ID {dto.Id} not found!");
        }

        User? user = null;
        if (dto.OwnerId != null)
        {
            user = await userDao.GetByIdAsync((int)dto.OwnerId);
            if (user == null)
            {
                throw new Exception($"User with id {dto.OwnerId} was not found.");
            }
        }

        User userToUse = user ?? existing.owner;
        string titleToUse = dto.Title ?? existing.Title;
        string contentToUse = dto.Content ?? existing.Content;

        Post updated = new(userToUse, titleToUse, contentToUse)
        {
            Id = existing.Id
        };
        
        ValidatePost(updated);
        await postDao.UpdateAsync(updated);
    }
    

    public async Task DeleteAsync(int id)
    {
        Post? post = await postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with ID {id} was not found");
        }

        await postDao.DeleteAsync(id);
    }
    
    public async Task<IEnumerable<Post>> GetByUserIdAsync(int id)
    {
        return await postDao.GetByUserIdAsync(id);
    }

    public async Task<Post> GetByIdAsync(int? id)
    {
        if (id == null)
        {
            throw new Exception("Id cannot be null");
        }

        Post? post = await postDao.GetByIdAsync((int)id);
        if (post == null)
        {
            throw new Exception($"Post with {id} not found");
        }

        return post;
    }
}