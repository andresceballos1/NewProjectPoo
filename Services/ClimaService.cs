using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;

namespace ClimaApi.Services;

public class ClimaService
{
    private readonly HttpClient _httpClient = new();
    private readonly string _baseUrl;

    public ClimaService(IConfiguration configuration)
    {
        _baseUrl = configuration["ApiSettings:OpenMeteoBaseUrl"] ?? "https://api.open-meteo.com/v1/forecast";
    }

    public async Task<(double Temperatura, string Descripcion)> ObtenerClimaAsync(double lat, double lon)
    {
        try
        {
            var url = $"{_baseUrl}?latitude={lat}&longitude={lon}&current_weather=true";
            var response = await _httpClient.GetFromJsonAsync<OpenMeteoResponse>(url);

            if (response?.CurrentWeather != null)
            {
                double temp = response.CurrentWeather.Temperature;
                string desc = ObtenerDescripcionClima(response.CurrentWeather.WeatherCode);
                return (temp, desc);
            }
        }
        catch { }

        return (20.0, "Clima no disponible");
    }

    private string ObtenerDescripcionClima(int code) => code switch
    {
        0 => "Cielo Despejado",
        1 or 2 or 3 => "Parcialmente Nublado",
        45 or 48 => "Neblina",
        51 or 53 or 55 => "Llovizna",
        61 or 63 or 65 => "Lluvia",
        _ => "Clima Templado"
    };
}

public class OpenMeteoResponse
{
    [JsonPropertyName("current_weather")]
    public CurrentWeatherResponse? CurrentWeather { get; set; }
}

public class CurrentWeatherResponse
{
    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }

    [JsonPropertyName("weathercode")]
    public int WeatherCode { get; set; }
}