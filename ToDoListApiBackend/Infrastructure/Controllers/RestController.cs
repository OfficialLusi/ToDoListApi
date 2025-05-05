using LusiUtilsLibrary.Backend.Initialization;
using Microsoft.AspNetCore.Mvc;
using TodoListApi.Dtos;
using ToDoListApiBackend.Application.Interfaces;

namespace ToDoListApiBackend.Infrastructure.Controllers;

[ApiController]
[Route("api/ToDoListApi")]
public class RestController : Controller
{
    private readonly IApplicationService _applicationService;
    private readonly ILogger<RestController> _logger;

    public RestController(IApplicationService applicationService, ILogger<RestController> logger)
    {
        #region Initialize checks
        InitializeChecks.InitialCheck(applicationService, "ApplicationService cannot be null");
        InitializeChecks.InitialCheck(logger, "Logger cannot be null");
        #endregion

        _applicationService = applicationService;
        _logger = logger;
    }

    #region Users
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult AddUser(UserDto userDto)
    {
        try
        {
            _applicationService.AddUser(userDto);
            _logger.LogInformation("RestController - Add user call executed succesfully with status code <{statusCode}>", StatusCodes.Status200OK);
            return Ok();
        }
        catch (Exception ex) 
        {
            _logger.LogError(ex, "RestController - Add user call not executed.");
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateUser(UserDto userDto)
    {
        try
        {
            _applicationService.UpdateUser(userDto);
            _logger.LogInformation("RestController - Update user call executed succesfully with status code <{statusCode}>", StatusCodes.Status200OK);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "RestController - Update user call not executed.");
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateUserPassword(UserDto userDto)
    {
        try
        {
            _applicationService.UpdateUserPassword(userDto);
            _logger.LogInformation("RestController - Update user call executed succesfully with status code <{statusCode}>", StatusCodes.Status200OK);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "RestController - Update user call not executed.");
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteUser(UserDto userDto)
    {
        try
        {
            _applicationService.DeleteUser(userDto);
            _logger.LogInformation("RestController - Delete user call executed succesfully with status code <{statusCode}>", StatusCodes.Status200OK);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "RestController - Delete user call not executed.");
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetUserByGuid(Guid userGuid)
    {
        try
        {
            _applicationService.GetUserByGuid(userGuid);
            _logger.LogInformation("RestController - Get user by guid call executed succesfully with status code <{statusCode}>", StatusCodes.Status200OK);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "RestController - Get user by guid call not executed.");
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAllUsers()
    {
        try
        {
            _applicationService.GetAllUsers();
            _logger.LogInformation("RestController - Get all users call executed succesfully with status code <{statusCode}>", StatusCodes.Status200OK);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "RestController - Get all users by guid call not executed.");
            return BadRequest();
        }
    }
    #endregion

    #region Activities

    #endregion
}
