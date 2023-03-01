using BubberDinner.Domain.Entities;

namespace BubberDinner.Application.Services.Authentication
{
    public record AuthResult(
        User user,
        string Token
        );
}
