using ClimaApi.ViewModels;
using Microsoft.Maui.Controls;

namespace ClimaApi.Views;

public partial class NuevaTareaPage : ContentPage
{
    public NuevaTareaPage(NuevaTareaViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}