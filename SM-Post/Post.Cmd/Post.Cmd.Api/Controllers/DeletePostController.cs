using CQRS.Core.Infraestructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.Commands;
using Post.Commom.DTOs;

namespace Post.Cmd.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DeletePostController : ControllerBase
    {
        private readonly ILogger<DeletePostController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public DeletePostController(ILogger<DeletePostController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostAsync(Guid id, DeletePostCommand deletePostCommand)
        {
            try
            {
                deletePostCommand.Id = id;
                await _commandDispatcher.SendAsync(deletePostCommand);

                return Ok(new BaseResponse()
                {
                    Message = "Delete post request completed successfully!"
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error deleting post.");
                const string CLIENT_ERROR = "Error processing request.";

                return StatusCode(StatusCodes.Status400BadRequest, new BaseResponse()
                {
                    Message = CLIENT_ERROR
                });
            }
        }
    }
}
