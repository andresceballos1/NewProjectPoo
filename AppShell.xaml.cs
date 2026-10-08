using ClimaApi.Views;
using Microsoft.Maui.Controls;

namespace ClimaApi;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(NuevaTareaPage), typeof(NuevaTareaPage));
        Routing.RegisterRoute(nameof(DetalleTareaPage), typeof(DetalleTareaPage));
    }
}