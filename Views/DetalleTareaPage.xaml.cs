using ClimaApi.ViewModels;
using Microsoft.Maui.Controls;

namespace ClimaApi.Views;

public partial class DetalleTareaPage : ContentPage
{
    public DetalleTareaPage(DetalleTareaViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}