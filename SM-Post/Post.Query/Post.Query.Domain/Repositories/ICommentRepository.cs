using Post.Cmd.Domain.Entities;

namespace Post.Cmd.Domain.Repositories;

public interface ICommentRepository
{
    Task CreateAsync(CommentEntity comment);
    Task UpdateAsync(CommentEntity comment);
    Task DeleteAsync(Guid commentId);
    Task<CommentEntity> GetByIdAsync(Guid commentId);
}