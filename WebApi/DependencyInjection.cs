using FluentValidation.AspNetCore;
using FluentValidation;
using System.Reflection;

namespace WebApi;
//al final el fluent validation se utiliza en web api 
public static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddValidation();

        return services;
    }
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddFluentValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}