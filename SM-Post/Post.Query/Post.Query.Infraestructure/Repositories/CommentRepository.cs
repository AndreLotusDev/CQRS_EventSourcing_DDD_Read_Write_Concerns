using Microsoft.EntityFrameworkCore;
using Post.Cmd.Domain.Entities;
using Post.Cmd.Domain.Repositories;
using Post.Cmd.Infrastructure.DataAccess;

namespace Post.Query.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly DatabaseContextFactory _databaseContextFactory;
    
    public CommentRepository(DatabaseContextFactory databaseContextFactory)
    {
        _databaseContextFactory = databaseContextFactory;
    }
    
    public async Task CreateAsync(CommentEntity comment)
    {
        using DatabaseContext databaseContext = _databaseContextFactory.CreateDbContext();
        databaseContext.Comments.Add(comment);
        _ = await databaseContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(CommentEntity comment)
    {
        using DatabaseContext databaseContext = _databaseContextFactory.CreateDbContext();
        databaseContext.Comments.Update(comment);
        _ = await databaseContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid commentId)
    {
        using DatabaseContext databaseContext = _databaseContextFactory.CreateDbContext();
        CommentEntity comment = await databaseContext.Comments.FindAsync(commentId);
        
        if (comment != null)
        {
            databaseContext.Comments.Remove(comment);
            _ = await databaseContext.SaveChangesAsync();
        }
        else throw new Exception("Comment not found");
    }

    public async Task<CommentEntity> GetByIdAsync(Guid commentId)
    {
        using DatabaseContext databaseContext = _databaseContextFactory.CreateDbContext();
        return await databaseContext.Comments.AsNoTracking()
            .Include(i => i.Post).AsNoTracking()
            .FirstOrDefaultAsync(f => f.CommentId == commentId);
    }
}