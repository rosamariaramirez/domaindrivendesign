
using System.Net;

namespace BubberDinner.Application.Common.Errors
{
    public record struct DuplicateEmailError() : IError
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage => "Email already exists";
    }
}
