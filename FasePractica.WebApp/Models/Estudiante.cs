using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FasePractica.WebApp.Models
{
    [Table("Estudiantes")]
    public class Estudiante : Persona
    {
        public int EstudianteId { get; set; }
        [Display(Name = "Código Ignug")]
        [MaxLength(32)]
        public string CodigoIgnug { get; set; }
        [NotMapped]
        public string DataValueField { get { return $"{Apellidos} {Nombres}, {Cedula}"; } }
    }
}
