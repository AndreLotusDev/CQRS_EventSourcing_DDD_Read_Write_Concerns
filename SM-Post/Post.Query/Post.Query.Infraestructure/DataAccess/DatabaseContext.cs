using Microsoft.EntityFrameworkCore;
using Post.Cmd.Domain.Entities;

namespace Post.Cmd.Infrastructure.DataAccess;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<PostEntity> Posts { get; set; }
    public DbSet<CommentEntity> Comments  { get; set; }
}