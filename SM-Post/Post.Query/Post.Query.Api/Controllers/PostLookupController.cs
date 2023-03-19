using CQRS.Core.Infraestructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Domain.Entities;
using Post.Query.Api.DTOs;
using Post.Query.Api.Queries;

namespace Post.Query.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PostLookupController : ControllerBase
    {
        private readonly ILogger<PostLookupController> _logger;
        private readonly IQueryDispatcher<PostEntity> _queryDispatcher;

        public PostLookupController(ILogger<PostLookupController> logger, IQueryDispatcher<PostEntity> queryDispatcher)
        {
            _logger = logger;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPostsAsync()
        {
            try
            {
                var query = new FindAllPostsQuery();
                var result = await _queryDispatcher.SendAsync(query);
            
                if(result == null || !result.Any())
                    return NoContent();

                var count = result.Count();

                return Ok(new PostLookupResponse
                {
                    Posts = result,
                    Message = $"Successfully returned {count} posts {(count > 1 ? "s" : string.Empty)}"
                });
            }
            catch (Exception e)
            {
                const string SAFE_ERROR_MESSAGE = "An error occurred while trying to retrieve all posts";
                _logger.LogError(e, SAFE_ERROR_MESSAGE);

                return BadRequest(new PostLookupResponse
                {
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }

        [HttpGet("byId/{postId}")]
        public async Task<ActionResult> GetByPostIdAsync(Guid postId)
        {
            try
            {
                var posts = await _queryDispatcher.SendAsync(new FindPostByIdQuery() { Id = postId });

                if (posts == null || !posts.Any())
                    return NoContent();

                return Ok(new PostLookupResponse
                {
                    Posts = posts,
                    Message = posts.Any() ? $"Find!" : "Not found"
                });
            }
            catch (Exception e)
            {
                const string SAFE_ERROR_MESSAGE = "An error occurred while trying to retrieve a single post";
                _logger.LogError(e, SAFE_ERROR_MESSAGE);

                return BadRequest(new PostLookupResponse
                {
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }

        [HttpGet("byAuthor/{author}")]
        public async Task<ActionResult> GetPostsByAuthorAsync(string author)
        {
            try
            {
                var posts = await _queryDispatcher.SendAsync(new FindPostByAuthorQuery() { Author = author });

                if (posts == null || !posts.Any())
                    return NoContent();

                return Ok(new PostLookupResponse
                {
                    Posts = posts,
                    Message = posts.Any() ? $"Find!" : "Not found"
                });
            }
            catch (Exception e)
            {
                const string SAFE_ERROR_MESSAGE = "An error occurred while trying to retrieve a post by author";
                _logger.LogError(e, SAFE_ERROR_MESSAGE);

                return BadRequest(new PostLookupResponse
                {
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }

        [HttpGet("WithComments")]
        public async Task<ActionResult> GetPostsWithCommentsAsync()
        {
            try
            {
                var posts = await _queryDispatcher.SendAsync(new FindPostWithCommentsQuery());

                if (posts == null || !posts.Any())
                    return NoContent();

                return Ok(new PostLookupResponse
                {
                    Posts = posts,
                    Message = posts.Any() ? $"Find!" : "Not found"
                });
            }
            catch (Exception e)
            {
                const string SAFE_ERROR_MESSAGE = "An error occurred while trying to retrieve posts with comments";
                _logger.LogError(e, SAFE_ERROR_MESSAGE);

                return BadRequest(new PostLookupResponse
                {
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }

        [HttpGet("withLikes/{numberOfLikes}")]
        public async Task<ActionResult> GetPostsWithLikesASync(int numberOfLikes)
        {
            try
            {
                var posts = await _queryDispatcher.SendAsync(new FindPostWithLikesQuery() { NumberOfLikes = numberOfLikes });

                if (posts == null || !posts.Any())
                    return NoContent();

                return Ok(new PostLookupResponse
                {
                    Posts = posts,
                    Message = posts.Any() ? $"Find!" : "Not found"
                });
            }
            catch (Exception e)
            {
                const string SAFE_ERROR_MESSAGE = "An error occurred while trying to retrieve posts with likes";
                _logger.LogError(e, SAFE_ERROR_MESSAGE);

                return BadRequest(new PostLookupResponse
                {
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }
    }
}
