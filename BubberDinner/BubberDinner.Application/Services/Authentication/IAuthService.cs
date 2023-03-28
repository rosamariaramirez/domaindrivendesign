using BubberDinner.Application.Common.Errors;
using ErrorOr;

namespace BubberDinner.Application.Services.Authentication
{
    public interface IAuthService
    {
        ErrorOr<AuthResult> Login(string email, string password);
        ErrorOr<AuthResult> Register(string firstName, string lastName, string email, string password);
    }
}