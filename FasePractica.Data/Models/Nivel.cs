using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FasePractica.Data.Models
{
    public class Nivel
    {
        public int NivelId {get; set;}
        public string Nombre {get; set;}
        public int Numero {get; set;}
        [Display(Name = "Horas de práctica")]
        public int HorasPractica {get; set;}
        [ForeignKey("Carrera")]
        public int CarreraId {get; set;}
        public Carrera Carrera {get; set;}
        public List<Nota> Notas {get; set;}
        [NotMapped]
        public string DataValueField { get { return Nombre; }}
    }
}