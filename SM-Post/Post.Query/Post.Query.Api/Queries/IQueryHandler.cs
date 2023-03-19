using Post.Cmd.Domain.Entities;

namespace Post.Query.Api.Queries
{
    public interface IQueryHandler
    {
        Task<List<PostEntity>> HandlerASync(FindAllPostsQuery query);
        Task<List<PostEntity>> HandlerASync(FindPostByIdQuery query);
        Task<List<PostEntity>> HandlerASync(FindPostByAuthorQuery query);
        Task<List<PostEntity>> HandlerASync(FindPostWithCommentsQuery query);
        Task<List<PostEntity>> HandlerASync(FindPostWithLikesQuery query);
    }
}
