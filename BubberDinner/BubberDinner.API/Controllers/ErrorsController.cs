using BubberDinner.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection;

namespace BubberDinner.API.Controllers
{
    public class ErrorsController: ControllerBase
    {
        [Route("/error")]

        public IActionResult Error()
        {
            Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            var (statusCode, message) = exception switch
            {
                IServiceException serviceException => ((int)serviceException.StatusCode, serviceException.ErrorMessage),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error ocurred.")
            };

            return Problem(statusCode: statusCode, title: message);
        }

    }
}
