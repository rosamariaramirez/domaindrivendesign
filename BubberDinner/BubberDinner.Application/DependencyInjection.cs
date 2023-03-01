﻿using BubberDinner.Application.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace BubberDinner.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
