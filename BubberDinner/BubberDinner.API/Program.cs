using BubberDinner.API.Errors;
using BubberDinner.API.Filters;
//0. using BubberDinner.API.Middleware;
using BubberDinner.Application;
using BubberDinner.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    //1. builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
    builder.Services.AddControllers();
    builder.Services.AddSingleton<ProblemDetailsFactory, BubberDinnerProblemDetailsFactory>();
}

var app = builder.Build();
{
    //0. app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseExceptionHandler("/error");
    // 4. app.Map("/error", (HttpContext httpContext) => 
    //{
    //    Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
    //    return Results.Problem(extensions: extensionsDic!);
    //}); ---- change the ErrorController path

    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
