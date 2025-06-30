using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaJoseAntonioCorderoDaniel.DTOs
{
    public class AlumnoCreateDTO
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string Apellido { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [MaxLength(20)]
        public string NumeroIdentificacion { get; set; }
    }
}
