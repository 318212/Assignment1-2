using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DTOs;
using Shared.Models;

namespace EfcDataAccess.DAOs;

public class PostEfcDao : IPostDao
{
    private readonly PostContext context;

    public PostEfcDao(PostContext context)
    {
        this.context = context;
    }
    
    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> added = await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        IQueryable<Post> query = context.Posts.Include(post => post.owner).AsQueryable();

        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            query = query.Where(post => 
                post.owner.UserName.ToLower().Equals(searchParameters.Username.ToLower()));
        }

        if (searchParameters.UserId != null)
        {
            query = query.Where(t => t.owner.Id == searchParameters.UserId);
        }

        if (!string.IsNullOrEmpty(searchParameters.TitleContains))
        {
            query = query.Where(t => 
                t.Title.ToLower().Contains(searchParameters.TitleContains.ToLower()));
        }

        if (!string.IsNullOrEmpty(searchParameters.ContentContains))
        {
            query = query.Where(t => 
                t.Content.ToLower().Contains(searchParameters.ContentContains.ToLower()));
        }

        List<Post> result = await query.ToListAsync();
        return result;
    }

    public async Task UpdateAsync(Post post)
    {
        context.Posts.Update(post);
        await context.SaveChangesAsync();
    }

    public async Task<Post?> GetByIdAsync(int postId)
    {
        Post? found = await context.Posts
            .AsNoTracking()
            .Include(post => post.owner)
            .SingleOrDefaultAsync(post => post.Id == postId);
        return found;
    }

    public async Task DeleteAsync(int id)
    {
        Post? existing = await GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"Post with id {id} not found");
        }

        context.Posts.Remove(existing);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Post>> GetByUserIdAsync(int id)
    {
        IQueryable<Post> query = context.Posts.Include(post => post.owner).AsQueryable();
        query = query.Where(post => id == post.owner.Id);
        List<Post> result = await query.ToListAsync();
        return result;
    }
}