using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FasePractica.Data.Models
{
    public class Estudiante : Persona
    {
        [Display(Name = "Código Ignug")]
        [MaxLength(32)]
        public string CodigoIgnug { get; set; }
        [ForeignKey("Carrera")]
        public int CarreraId {get; set;}
        public Carrera Carrera {get; set;}
        public List<Nota> Notas { get; set; }
        [NotMapped]
        public string DataValueField { get { return $"{Apellidos} {Nombres}, {Cedula}"; } }
    }
}
