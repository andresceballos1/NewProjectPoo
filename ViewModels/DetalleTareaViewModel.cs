using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClimaApi.Models;

namespace ClimaApi.ViewModels;

[QueryProperty(nameof(Tarea), "Tarea")]
public partial class DetalleTareaViewModel : ObservableObject
{
    [ObservableProperty]
    private TareaModel tarea = new();

    [RelayCommand]
    public async Task VolverAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}