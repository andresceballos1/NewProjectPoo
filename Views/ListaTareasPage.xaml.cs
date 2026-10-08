using ClimaApi.ViewModels;
using Microsoft.Maui.Controls;

namespace ClimaApi.Views;

public partial class ListaTareasPage : ContentPage
{
    private readonly ListaTareasViewModel _vm;

    public ListaTareasPage(ListaTareasViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.CargarTareasCommand.ExecuteAsync(null);
    }
}