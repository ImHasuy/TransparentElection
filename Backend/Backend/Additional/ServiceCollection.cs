using Backend.interfaces;
using Backend.Services;

namespace Backend.Additional;

public static class ServiceCollection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IImportAndGenerationService, ImportAndGenerationService>();
    }
}