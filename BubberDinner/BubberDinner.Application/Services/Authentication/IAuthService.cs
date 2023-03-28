using BubberDinner.Application.Common.Errors;
using FluentResults;

namespace BubberDinner.Application.Services.Authentication
{
    public interface IAuthService
    {
        AuthResult Login(string email, string password);
        Result<AuthResult> Register(string firstName, string lastName, string email, string password);
    }
}