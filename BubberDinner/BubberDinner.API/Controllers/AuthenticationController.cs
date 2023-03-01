using BubberDinner.Application.Common.Errors;
using BubberDinner.Application.Services.Authentication;
using BubberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace BubberDinner.API.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthenticationController(IAuthService authService) 
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        OneOf<AuthResult, DuplicateEmailError> registerResult = _authService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        if (registerResult.IsT0)
        {
            var authResult = registerResult.AsT0;
            var response = new AuthenticationResponse(
            authResult.user.Id,
            authResult.user.FirstName,
            authResult.user.LastName,
            authResult.user.Email,
            authResult.Token);
            return Ok(response);
        }

        return Problem(statusCode: StatusCodes.Status409Conflict, title: "Email already exists.");
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authService.Login(
            request.Email,
            request.Password);
        var response = new AuthenticationResponse(
            authResult.user.Id,
            authResult.user.FirstName,
            authResult.user.LastName,
            authResult.user.Email,
            authResult.Token);
        return Ok(response);
    }
}

