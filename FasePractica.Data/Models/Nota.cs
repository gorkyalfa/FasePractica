using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FasePractica.Data.Models
{
    [Table("Notas")]
    public class Nota
    {
        public int NotaId { get; set; }
        [ForeignKey("Estudiante")]
        [Display(Name = "Estudiante")]
        public int EstudianteId { get; set; }
        public Estudiante Estudiante { get; set; }
        [ForeignKey("Proyecto")]
        [Display(Name = "Proyecto")]
        public int ProyectoId { get; set; }
        public Proyecto Proyecto { get; set; }
        [Display(Name = "Calificación")]
        [Range(0,100)]
        [Required]
        public float Calificacion { get; set; }
        [Required]
        public bool Aprueba { get; set; }

    }
}
