using CQRS.Core.Infraestructure;
using Microsoft.AspNetCore.Mvc;
using Post.Cmd.Api.Commands;
using Post.Cmd.Api.DTOs;
using Post.Commom.DTOs;

namespace Post.Cmd.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class NewPostController : ControllerBase
{
    private readonly ILogger<NewPostController> _logger;
    private readonly ICommandDispatcher _commandDispatcher;
    
    public NewPostController(ILogger<NewPostController> logger, ICommandDispatcher commandDispatcher)
    {
        _logger = logger;
        _commandDispatcher = commandDispatcher;
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] NewPostCommand command)
    {
        _logger.LogInformation("New post request received.");

        try
        {
            var id = Guid.NewGuid();
            command.Id = id;
            
            await _commandDispatcher.SendAsync(command);

             return StatusCode(StatusCodes.Status200OK, new NewPostResponse
            {
                Id = id,
                Message = "Post created successfully."
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating post.");
            const string CLIENT_ERROR = "Error processing request.";
            
            return StatusCode(StatusCodes.Status400BadRequest,new BaseResponse()
            {
                Message = CLIENT_ERROR
            });
        }
    }
}