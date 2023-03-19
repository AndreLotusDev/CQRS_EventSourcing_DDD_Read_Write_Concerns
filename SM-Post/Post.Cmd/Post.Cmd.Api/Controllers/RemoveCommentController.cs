using CQRS.Core.Infraestructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.Commands;
using Post.Commom.DTOs;

namespace Post.Cmd.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RemoveCommentController : ControllerBase
    {
        private readonly ILogger<RemoveCommentController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public RemoveCommentController(ILogger<RemoveCommentController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCommentAsync(Guid id, RemoveCommentCommand removeCommentCommand)
        {
            try
            {
                removeCommentCommand.Id = id;
                await _commandDispatcher.SendAsync(removeCommentCommand);

                return Ok(new BaseResponse()
                {
                    Message = "Remove comment request completed successfully!"
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error removing comment.");
                const string CLIENT_ERROR = "Error processing request.";

                return StatusCode(StatusCodes.Status400BadRequest, new BaseResponse()
                {
                    Message = CLIENT_ERROR
                });
            }
        }
    }
}
