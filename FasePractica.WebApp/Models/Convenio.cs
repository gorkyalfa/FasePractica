using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FasePractica.WebApp.Models
{
    [Table("Convenios")]
    public class Convenio : Documento
    {
        public int ConvenioId { get; set; }
        [Display(Name = "Finalización")]
        [Required]
        public DateTime FechaFinal { get; set; }
        [Display(Name = "Descripción")]
        [MaxLength(500)]
        [Required]
        public string Descripcion { get; set; }
        [ForeignKey("Empresa")]
        [Display(Name = "Empresa")]
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        [ForeignKey("InformeViabilidad")]
        [Display(Name = "Informe viabilidad")]
        public int InformeViabilidadId { get; set; }
        public Documento InformeViabilidad { get; set; }
        [ForeignKey("Memorando")]
        [Display(Name = "Memorando")]
        public int MemorandoId { get; set; }
        public Documento Memorando { get; set; }
        [ForeignKey("Acta")]
        [Display(Name = "Acta")]
        public int ActaId { get; set; }
        public Documento Acta { get; set; }
        public List<Documento> ExtensionAlConvenio { get; set; }
        [Display(Name = "Estudiantes convenio")]
        [Required]
        public int EstudiantesConvenio { get; set; }
        [Display(Name = "Estudiantes actuales")]
        [Required]
        public int EstudiantesActuales { get; set; }
        [NotMapped]
        public string DataValueField { get { return Codigo; } }
    }
}
