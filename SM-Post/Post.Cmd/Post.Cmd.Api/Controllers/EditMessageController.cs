using CQRS.Core.Infraestructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.Commands;
using Post.Commom.DTOs;

namespace Post.Cmd.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EditMessageController : ControllerBase
    {
        private readonly ILogger<EditMessageController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public EditMessageController(ILogger<EditMessageController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditMessageAsync(Guid id, EditMessageCommand command)
        {
            try
            {
                command.Id = id;
                await _commandDispatcher.SendAsync(command);

                return Ok(new BaseResponse()
                {
                    Message = "Edit message request completed successfully!"
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error editing message.");
                const string CLIENT_ERROR = "Error processing request.";

                return StatusCode(StatusCodes.Status400BadRequest, new BaseResponse()
                {
                    Message = CLIENT_ERROR
                });
            }
        } 
    }
}
