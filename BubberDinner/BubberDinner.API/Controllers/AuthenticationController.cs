using BubberDinner.Application.Common.Errors;
using BubberDinner.Application.Services.Authentication;
using BubberDinner.Contracts.Authentication;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

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
        ErrorOr<AuthResult> authResult = _authService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        return authResult.MatchFirst(
            authResult => Ok(MapAuthResult(authResult)),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
            );
    }

    private static AuthenticationResponse MapAuthResult(AuthResult authResult)
    {
        return new AuthenticationResponse(
            authResult.user.Id,
            authResult.user.FirstName,
            authResult.user.LastName,
            authResult.user.Email,
            authResult.Token
            );
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authService.Login(
            request.Email,
            request.Password);

        return authResult.MatchFirst(
            authResult => Ok(MapAuthResult(authResult)),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
            );
    }
}

