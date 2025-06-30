using PruebaTecnicaJoseAntonioCorderoDaniel.DTOs;

namespace PruebaTecnicaJoseAntonioCorderoDaniel.Services.Interfaces
{
    public interface IAlumnoService
    {
        Task CrearAlumnoAsync(string nombre, string apellido, DateTime fechaNacimiento, string numeroIdentificacion);
        Task ActualizarAlumnoAsync(int alumnoId, string nombre, string apellido, DateTime fechaNacimiento, string numeroIdentificacion);
        Task EliminarAlumnoAsync(int alumnoId);
        Task<List<AlumnoDTO>> ObtenerAlumnosAsync();
        Task<AlumnoDTO> ObtenerAlumnoPorIdAsync(int alumnoId);
    }
}
