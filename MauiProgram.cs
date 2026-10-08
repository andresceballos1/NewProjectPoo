using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ClimaApi.Services;
using ClimaApi.ViewModels;
using ClimaApi.Views;

namespace ClimaApi;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Lectura de la configuración del archivo embebido applicationconfig.json
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("ClimaApi.applicationconfig.json");

        if (stream != null)
        {
            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();

            builder.Configuration.AddConfiguration(config);
        }

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Registro de Servicios
        builder.Services.AddSingleton<DatabaseService>();
        builder.Services.AddSingleton<UbicacionService>();
        builder.Services.AddSingleton<ClimaService>();

        // Registro de ViewModels
        builder.Services.AddTransient<ListaTareasViewModel>();
        builder.Services.AddTransient<NuevaTareaViewModel>();
        builder.Services.AddTransient<DetalleTareaViewModel>();

        // Registro de Vistas
        builder.Services.AddTransient<ListaTareasPage>();
        builder.Services.AddTransient<NuevaTareaPage>();
        builder.Services.AddTransient<DetalleTareaPage>();

        return builder.Build();
    }
}