using CQRS.Core.Infraestructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.Commands;
using Post.Commom.DTOs;

namespace Post.Cmd.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AddCommandController : ControllerBase
    {
        private readonly ILogger<AddCommandController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public AddCommandController(ILogger<AddCommandController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AddCommandAsync(Guid id, AddCommentCommand command)
        {
            try
            {
                command.Id = id;
                await _commandDispatcher.SendAsync(command);

                return Ok(new BaseResponse()
                {
                    Message = "Add command request completed successfully!"
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error adding command.");
                const string CLIENT_ERROR = "Error processing request.";

                return StatusCode(StatusCodes.Status400BadRequest,new BaseResponse()
                {
                    Message = CLIENT_ERROR
                });
            }
        }
    }
}
