using CQRS.Core.Infraestructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.Commands;
using Post.Commom.DTOs;

namespace Post.Cmd.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LikePostController : ControllerBase
    {
        private readonly ILogger<LikePostController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public LikePostController(ILogger<LikePostController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> LikePostAsync(Guid id)
        {
            try
            {
                var command = new LikePostCommand
                {
                    Id = id
                };

                await _commandDispatcher.SendAsync(command);

                return Ok(new BaseResponse()
                {
                    Message = "Like post request completed successfully!"
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error liking post.");
                const string CLIENT_ERROR = "Error processing request.";

                return StatusCode(StatusCodes.Status400BadRequest, new BaseResponse()
                {
                    Message = CLIENT_ERROR
                });
            }
        }
    }
}
