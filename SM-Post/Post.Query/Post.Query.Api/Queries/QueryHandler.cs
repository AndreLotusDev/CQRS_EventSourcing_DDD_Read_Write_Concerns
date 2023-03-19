using Post.Cmd.Domain.Entities;
using Post.Cmd.Domain.Repositories;

namespace Post.Query.Api.Queries
{
    public class QueryHandler : IQueryHandler
    {
        private readonly  IPostRepository _postRepository;

        public QueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<List<PostEntity>> HandlerASync(FindAllPostsQuery query)
        {
            return await _postRepository.ListAllAsync();
        }

        public async Task<List<PostEntity>> HandlerASync(FindPostByIdQuery query)
        {
            var post = await _postRepository.GetByIdAsync(query.Id);
            return new List<PostEntity> { post };
        }

        public async Task<List<PostEntity>> HandlerASync(FindPostByAuthorQuery query)
        {
            return await _postRepository.ListByAuthorAsync(query.Author);
        }

        public async Task<List<PostEntity>> HandlerASync(FindPostWithCommentsQuery query)
        {
            return await _postRepository.ListWithCommentsAsync();
        }

        public async Task<List<PostEntity>> HandlerASync(FindPostWithLikesQuery query)
        {
            return await _postRepository.ListWithLikesAsync(query.NumberOfLikes);
        }
    }
}
