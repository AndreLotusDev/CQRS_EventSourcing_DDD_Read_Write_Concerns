using Microsoft.EntityFrameworkCore;
using Post.Cmd.Domain.Entities;
using Post.Cmd.Domain.Repositories;
using Post.Cmd.Infrastructure.DataAccess;

namespace Post.Query.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly DatabaseContextFactory _databaseContextFactory;
    
    public PostRepository(DatabaseContextFactory databaseContextFactory)
    {
        _databaseContextFactory = databaseContextFactory;
    }
    
    public async Task CreateAsync(PostEntity post)
    {
        using DatabaseContext databaseContext = _databaseContextFactory.CreateDbContext();
        databaseContext.Posts.Add(post);
        _ = await databaseContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(PostEntity post)
    {
        using DatabaseContext databaseContext = _databaseContextFactory.CreateDbContext();
        databaseContext.Posts.Update(post);
        _ = await databaseContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid postId)
    {
        using DatabaseContext databaseContext = _databaseContextFactory.CreateDbContext();
        PostEntity post = await databaseContext.Posts.FindAsync(postId);
        
        if (post != null)
        {
            databaseContext.Posts.Remove(post);
            _ = await databaseContext.SaveChangesAsync();
        }
        else throw new Exception("Post not found");
    }

    public async Task<PostEntity> GetByIdAsync(Guid postId)
    {
        using DatabaseContext databaseContext = _databaseContextFactory.CreateDbContext();
        return await databaseContext.Posts
            .Include(i => i.Comments)
            .FirstOrDefaultAsync(f => f.PostId == postId);
    }

    public async Task<List<PostEntity>> ListAllAsync()
    {
        using DatabaseContext databaseContext = _databaseContextFactory.CreateDbContext();
        return await databaseContext
            .Posts.AsNoTracking()
            .Include(i => i.Comments).AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<PostEntity>> ListByAuthorAsync(string author)
    {
        using DatabaseContext databaseContext = _databaseContextFactory.CreateDbContext();
        return await databaseContext
            .Posts.AsNoTracking()
            .Include(i => i.Comments).AsNoTracking()
            .Where(w => w.Author.ToLower().Trim() == author.ToLower().Trim())
            .ToListAsync();
    }

    public async Task<List<PostEntity>> ListWithLikesAsync(int numberOfLikes)
    {
        using DatabaseContext databaseContext = _databaseContextFactory.CreateDbContext();
        return await databaseContext
            .Posts.AsNoTracking()
            .Include(i => i.Comments).AsNoTracking()
            .Where(w => w.Likes >= numberOfLikes)
            .ToListAsync();
    }

    public async Task<List<PostEntity>> ListWithCommentsAsync()
    {
        using DatabaseContext databaseContext = _databaseContextFactory.CreateDbContext();
        return await databaseContext
            .Posts.AsNoTracking()
            .Include(i => i.Comments).AsNoTracking()
            .Where(w => w.Comments.Count > 0)
            .ToListAsync();
    }
}