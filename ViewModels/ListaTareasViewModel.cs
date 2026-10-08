using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClimaApi.Models;
using ClimaApi.Services;
using System.Collections.ObjectModel;

namespace ClimaApi.ViewModels;

public partial class ListaTareasViewModel : ObservableObject
{
    private readonly DatabaseService _dbService;

    [ObservableProperty]
    private ObservableCollection<TareaModel> tareas = new();

    [ObservableProperty]
    private bool isBusy;

    public ListaTareasViewModel(DatabaseService dbService)
    {
        _dbService = dbService;
    }

    [RelayCommand]
    public async Task CargarTareasAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            var lista = await _dbService.ObtenerTareasAsync();
            Tareas.Clear();
            foreach (var item in lista) Tareas.Add(item);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    public async Task IrANuevaTareaAsync()
    {
        await Shell.Current.GoToAsync(nameof(Views.NuevaTareaPage));
    }

    [RelayCommand]
    public async Task SeleccionarTareaAsync(TareaModel tarea)
    {
        if (tarea == null) return;
        var param = new Dictionary<string, object> { { "Tarea", tarea } };
        await Shell.Current.GoToAsync(nameof(Views.DetalleTareaPage), param);
    }
}