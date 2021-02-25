using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FasePractica.Data.Models
{
    [Table("Semestres")]
    public class Semestre
    {
        public int SemestreId { get; set; }
        [Display(Name = "Fecha inicio")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime FechaInicio { get; set; }
        [Display(Name = "Fecha fin")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime FechaFin { get; set; }
        [MaxLength(50)]
        [Required]
        public string Nombre { get; set; }
        public List<Proyecto> Proyectos { get; set; }
        [NotMapped]
        public string DataValueField { get { return $"{Nombre}"; } }
    }
}
