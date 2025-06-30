using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaJoseAntonioCorderoDaniel.DTOs
{
    public class ProfesorCreateDTO
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(50)]
        public string Apellido { get; set; }
        [Required]
        [MaxLength(20)]
        public string NumeroIdentificacion { get; set; }
        [Required]
        public int EscuelaID { get; set; }
    }

}
