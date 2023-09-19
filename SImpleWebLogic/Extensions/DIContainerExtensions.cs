﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SImpleWebLogic.Extensions;

public static class DIContainerExtensions
{
    public static IServiceCollection RegisterApplicationDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}
