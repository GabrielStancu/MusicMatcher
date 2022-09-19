using Business.Services;

namespace API.Extensions.ServiceCollection;

public static class ServiceExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
    }
}
