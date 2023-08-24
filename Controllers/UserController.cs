using API.DTOs.User;
using API.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

// Indicates that this class is an API controller
[ApiController]
// Sets the base route for the controller
[Route("[controller]")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // GET: /User
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<GetUserDTO>>>> Get()
    {
        // Return a response with the list of User objects
        return Ok(await _userService.Get());
    }
    // GET: /User/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetUserDTO>>> GetById(int id)
    {
        // Return a response with the User object for the given id
        return Ok(await _userService.GetById(id));
    }

    // POST: /User
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<GetUserDTO>>>> AddUser(AddUserDTO userDTO)
    {
        // Return a response after adding the User object
        return Ok(await _userService.AddUser(userDTO));
    }

    // PUT: /User
    [HttpPut]
    public async Task<ActionResult<ServiceResponse<List<GetUserDTO>>>> UpdateUser(UpdateUserDTO userDTO)
    {
        // Update the User object and return a response
        var response = await _userService.UpdateUser(userDTO);
        if(response.Data is null) return NotFound(response);
        return Ok(response);
    }

    // DELETE: /User/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<GetUserDTO>>> Delete(int id)
    {
        // Delete the User object and return a response
        var response = await _userService.DeleteUser(id);
        if(response.Data is null) return NotFound(response);
        return Ok(response);
    }
}
