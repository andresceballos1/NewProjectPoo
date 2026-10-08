using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClimaApi.Models;
using ClimaApi.Services;

namespace ClimaApi.ViewModels;

public partial class NuevaTareaViewModel : ObservableObject
{
    private readonly DatabaseService _dbService;
    private readonly UbicacionService _ubicacionService;
    private readonly ClimaService _climaService;

    [ObservableProperty] private string titulo = string.Empty;
    [ObservableProperty] private string descripcion = string.Empty;
    [ObservableProperty] private string estadoClimaGPS = "Presiona 'Obtener Clima' para capturar GPS";
    [ObservableProperty] private bool isBusy;

    private double _lat;
    private double _lon;
    private double _temp;
    private string _climaDesc = string.Empty;

    public NuevaTareaViewModel(DatabaseService dbService, UbicacionService ubicacionService, ClimaService climaService)
    {
        _dbService = dbService;
        _ubicacionService = ubicacionService;
        _climaService = climaService;
    }

    [RelayCommand]
    public async Task CapturarUbicacionYClimaAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            EstadoClimaGPS = "Obteniendo GPS...";

            var loc = await _ubicacionService.ObtenerUbicacionActualAsync();
            if (loc == null)
            {
                await Shell.Current.DisplayAlert("GPS Error", "No se obtuvo la ubicación.", "OK");
                EstadoClimaGPS = "Sin acceso a GPS";
                return;
            }

            _lat = loc.Latitude;
            _lon = loc.Longitude;

            EstadoClimaGPS = "Consultando API de clima...";
            var (temp, desc) = await _climaService.ObtenerClimaAsync(_lat, _lon);
            _temp = temp;
            _climaDesc = desc;

            EstadoClimaGPS = $"Lat: {_lat:F2}, Lon: {_lon:F2} | {desc}, {temp}°C";
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
    public async Task GuardarTareaAsync()
    {
        if (string.IsNullOrWhiteSpace(Titulo))
        {
            await Shell.Current.DisplayAlert("Atención", "Ingresa un título para la tarea.", "OK");
            return;
        }

        try
        {
            IsBusy = true;
            var nuevaTarea = new TareaModel
            {
                Titulo = Titulo,
                Descripcion = Descripcion,
                Fecha = DateTime.Now,
                Latitud = _lat,
                Longitud = _lon,
                Temperatura = _temp,
                ClimaDescripcion = string.IsNullOrEmpty(_climaDesc) ? "Sin datos" : _climaDesc
            };

            await _dbService.GuardarTareaAsync(nuevaTarea);
            await Shell.Current.DisplayAlert("Éxito", "Tarea guardada.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error SQLite", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}