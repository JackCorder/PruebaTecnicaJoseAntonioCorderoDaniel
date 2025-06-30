using PruebaTecnicaJoseAntonioCorderoDaniel.DTOs;

namespace PruebaTecnicaJoseAntonioCorderoDaniel.Services.Interfaces
{
    public interface IProfesorService
    {
        Task CrearProfesorAsync(string nombre, string apellido, string numeroIdentificacion, int escuelaId);
        Task ActualizarProfesorAsync(int profesorId, string nombre, string apellido, string numeroIdentificacion, int escuelaId);
        Task EliminarProfesorAsync(int profesorId);
        Task<List<ProfesorDTO>> ObtenerProfesoresAsync();
        Task<ProfesorDTO> ObtenerProfesorPorIdAsync(int profesorId);
    }
}
