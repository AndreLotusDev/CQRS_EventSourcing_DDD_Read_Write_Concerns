using CQRS.Core.Infraestructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.Commands;
using Post.Commom.DTOs;

namespace Post.Cmd.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RestoreReadDbController : ControllerBase
    {
        private readonly ILogger<NewPostController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public RestoreReadDbController(ILogger<NewPostController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            _logger.LogInformation("Restore read db request received.");

            try
            {
                await _commandDispatcher.SendAsync(new RestoreReadDbCommand());

                return StatusCode(StatusCodes.Status200OK, new BaseResponse
                {
                    Message = "Read db restored successfully."
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error restoring read db.");
                const string CLIENT_ERROR = "Error processing request.";

                return StatusCode(StatusCodes.Status400BadRequest, new BaseResponse()
                {
                    Message = CLIENT_ERROR
                });
            }
        }
    }
}
