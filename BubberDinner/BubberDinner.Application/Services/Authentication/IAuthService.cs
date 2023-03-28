using BubberDinner.Application.Common.Errors;
using OneOf;

namespace BubberDinner.Application.Services.Authentication
{
    public interface IAuthService
    {
        AuthResult Login(string email, string password);
        OneOf<AuthResult, IError> Register(string firstName, string lastName, string email, string password);
    }
}