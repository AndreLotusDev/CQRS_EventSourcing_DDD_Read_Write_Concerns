using CQRS.Core.Infraestructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.Commands;
using Post.Commom.DTOs;

namespace Post.Cmd.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EditCommandController : ControllerBase
    {
        private readonly ILogger<EditCommandController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public EditCommandController(ILogger<EditCommandController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCommandAsync(Guid id, EditCommentCommand command)
        {
            try
            {
                command.Id = id;
                await _commandDispatcher.SendAsync(command);

                return Ok(new BaseResponse()
                {
                    Message = "Edit command request completed successfully!"
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error editing command.");
                const string CLIENT_ERROR = "Error processing request.";

                return StatusCode(StatusCodes.Status400BadRequest, new BaseResponse()
                {
                    Message = CLIENT_ERROR
                });
            }
        }
    }
}
