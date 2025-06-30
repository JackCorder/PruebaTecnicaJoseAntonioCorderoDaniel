using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaJoseAntonioCorderoDaniel.Services.Interfaces;
using PruebaTecnicaJoseAntonioCorderoDaniel.DTOs;

namespace PruebaTecnicaJoseAntonioCorderoDaniel.Controllers
{
    [ApiController]
    [Route("api/v1/alumnos-profesor")]
    public class AlumnoProfesorController : ControllerBase
    {
        private readonly IAlumnoProfesorService _service;

        public AlumnoProfesorController(IAlumnoProfesorService service)
        {
            _service = service;
        }

        [HttpPost("asignar")]
        public async Task<IActionResult> AsignarProfesorAAlumno(int alumnoId, int profesorId)
        {
            try
            {
                await _service.AsignarProfesorAAlumnoAsync(alumnoId, profesorId);
                return Ok("Profesor asignado al alumno correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("eliminar")]
        public async Task<IActionResult> EliminarAsignacion(int alumnoId, int profesorId)
        {
            try
            {
                await _service.EliminarAsignacionAsync(alumnoId, profesorId);
                return Ok("Asignación eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("profesores-de-alumno/{alumnoId}")]
        public async Task<IActionResult> ObtenerProfesoresDeAlumno(int alumnoId)
        {
            try
            {
                var profesores = await _service.ObtenerProfesoresDeAlumnoAsync(alumnoId);
                return Ok(profesores);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("alumnos-de-profesor/{profesorId}")]
        public async Task<IActionResult> ObtenerAlumnosDeProfesor(int profesorId)
        {
            try
            {
                var alumnos = await _service.ObtenerAlumnosDeProfesorAsync(profesorId);
                return Ok(alumnos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

    }
}