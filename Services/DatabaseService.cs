using SQLite;
using ClimaApi.Models;

namespace ClimaApi.Services;

public class DatabaseService
{
    private SQLiteAsyncConnection? _database;

    private async Task InitAsync()
    {
        if (_database is not null) return;
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "tareas_clima.db3");
        _database = new SQLiteAsyncConnection(dbPath);
        await _database.CreateTableAsync<TareaModel>();
    }

    public async Task<List<TareaModel>> ObtenerTareasAsync()
    {
        await InitAsync();
        return await _database!.Table<TareaModel>().OrderByDescending(t => t.Fecha).ToListAsync();
    }

    public async Task<int> GuardarTareaAsync(TareaModel tarea)
    {
        await InitAsync();
        if (tarea.Id != 0)
            return await _database!.UpdateAsync(tarea);
        return await _database!.InsertAsync(tarea);
    }
}