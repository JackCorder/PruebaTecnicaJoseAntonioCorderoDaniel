using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaJoseAntonioCorderoDaniel.DTOs
{
    public class EscuelaCreateDTO
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(150)]
        public string Descripcion { get; set; }
    }
}
