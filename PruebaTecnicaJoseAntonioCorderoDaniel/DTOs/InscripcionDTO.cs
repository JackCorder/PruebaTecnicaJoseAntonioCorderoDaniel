namespace PruebaTecnicaJoseAntonioCorderoDaniel.DTOs
{
    public class InscripcionDTO
    {
        public int InscripcionID { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public int AlumnoID { get; set; }
        public string NombreAlumno { get; set; }
        public string ApellidoAlumno { get; set; }
        public int EscuelaID { get; set; }
        public string NombreEscuela { get; set; }
    }
}
