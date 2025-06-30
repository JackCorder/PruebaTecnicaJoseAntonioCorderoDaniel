using PruebaTecnicaJoseAntonioCorderoDaniel.DTOs;
using PruebaTecnicaJoseAntonioCorderoDaniel.Models;

namespace PruebaTecnicaJoseAntonioCorderoDaniel.Services.Interfaces
{
    public interface IEscuelaService
    {
        Task CrearEscuelaAsync(string nombre, string descripcion);
        Task ActualizarEscuelaAsync(int id, string nombre, string descripcion);
        Task EliminarEscuelaAsync(int id);
        Task<List<EscuelaDTO>> ObtenerEscuelasAsync();
        Task<EscuelaDTO> ObtenerEscuelaPorIdAsync(int id);
    }
}
