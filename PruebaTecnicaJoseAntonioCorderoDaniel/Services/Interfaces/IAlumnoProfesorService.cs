using PruebaTecnicaJoseAntonioCorderoDaniel.DTOs;

namespace PruebaTecnicaJoseAntonioCorderoDaniel.Services.Interfaces
{
    public interface IAlumnoProfesorService
    {
        Task AsignarProfesorAAlumnoAsync(int alumnoId, int profesorId);
        Task EliminarAsignacionAsync(int alumnoId, int profesorId);
        Task<List<ProfesorRelacionDTO>> ObtenerProfesoresDeAlumnoAsync(int alumnoId);
        Task<List<AlumnoRelacionDTO>> ObtenerAlumnosDeProfesorAsync(int profesorId);
        Task<List<AlumnoConEscuelaDTO>> ObtenerAlumnosInscritosPorProfesorAsync(int profesorId);
        Task<List<EscuelaConAlumnosDTO>> ObtenerEscuelasYAlumnosDeProfesorAsync(int profesorId);
    }
}
