using PruebaTecnicaJoseAntonioCorderoDaniel.DTOs;

namespace PruebaTecnicaJoseAntonioCorderoDaniel.Services.Interfaces
{
    public interface IInscripcionService
    {
        Task<List<InscripcionDTO>> ObtenerInscripcionesAsync();
        Task<InscripcionDTO> ObtenerInscripcionPorIdAsync(int inscripcionId);
        Task CrearInscripcionAsync(InscripcionCreateDTO dto);
        Task ActualizarInscripcionAsync(int inscripcionId, InscripcionCreateDTO dto);
        Task EliminarInscripcionAsync(int inscripcionId);
    }

}
