using SQLite;

namespace ClimaApi.Models;

public class TareaModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime Fecha { get; set; } = DateTime.Now;
    public double Latitud { get; set; }
    public double Longitud { get; set; }
    public double Temperatura { get; set; }
    public string ClimaDescripcion { get; set; } = string.Empty;
}