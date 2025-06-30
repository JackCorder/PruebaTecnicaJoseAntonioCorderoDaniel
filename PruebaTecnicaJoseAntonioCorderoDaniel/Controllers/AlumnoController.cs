using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaJoseAntonioCorderoDaniel.DTOs;
using PruebaTecnicaJoseAntonioCorderoDaniel.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaTecnicaJoseAntonioCorderoDaniel.Controllers
{
    [ApiController]
    [Route("api/v1/alumnos")]
    public class AlumnoController : ControllerBase
    {
        private readonly IAlumnoService _alumnoService;

        public AlumnoController(IAlumnoService alumnoService)
        {
            _alumnoService = alumnoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlumnoDTO>>> ObtenerAlumnos()
        {
            var alumnos = await _alumnoService.ObtenerAlumnosAsync();
            return Ok(alumnos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlumnoDTO>> ObtenerAlumnoPorId(int id)
        {
            try
            {
                var alumno = await _alumnoService.ObtenerAlumnoPorIdAsync(id);
                return Ok(alumno);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CrearAlumno([FromBody] AlumnoCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _alumnoService.CrearAlumnoAsync(dto.Nombre, dto.Apellido, dto.FechaNacimiento, dto.NumeroIdentificacion);
                return Ok("Alumno creado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarAlumno(int id, [FromBody] AlumnoCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _alumnoService.ActualizarAlumnoAsync(id, dto.Nombre, dto.Apellido, dto.FechaNacimiento, dto.NumeroIdentificacion);
                return Ok("Alumno actualizado correctamente.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarAlumno(int id)
        {
            try
            {
                await _alumnoService.EliminarAlumnoAsync(id);
                return Ok("Alumno eliminado correctamente.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
