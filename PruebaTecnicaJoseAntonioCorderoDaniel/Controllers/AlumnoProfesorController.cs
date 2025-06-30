using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaJoseAntonioCorderoDaniel.Services.Interfaces;
using PruebaTecnicaJoseAntonioCorderoDaniel.DTOs;
using PruebaTecnicaJoseAntonioCorderoDaniel.Services.Implements;

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



        [HttpGet("alumnos-inscritos-por-profesor/{profesorId}")]
        public async Task<ActionResult<List<AlumnoConEscuelaDTO>>> ObtenerAlumnosInscritosPorProfesor(int profesorId)
        {
            try
            {
                var resultado = await _service.ObtenerAlumnosInscritosPorProfesorAsync(profesorId);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex.Message });
            }
        }


        [HttpGet("escuelas-alumnos-profesor/{profesorId}")]
        public async Task<ActionResult<List<EscuelaConAlumnosDTO>>> ObtenerEscuelasYAlumnosDeProfesor(int profesorId)
        {
            try
            {
                var resultado = await _service.ObtenerEscuelasYAlumnosDeProfesorAsync(profesorId);

                if (resultado == null || !resultado.Any())
                    return NotFound($"No se encontraron alumnos asignados para el profesor con ID {profesorId}.");

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al obtener los datos: {ex.Message}" });
            }
        }

    }
}