using Microsoft.Maui.Devices.Sensors;

namespace ClimaApi.Services;

public class UbicacionService
{
    public async Task<Location?> ObtenerUbicacionActualAsync()
    {
        try
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            return await Geolocation.Default.GetLocationAsync(request);
        }
        catch
        {
            return null;
        }
    }
}