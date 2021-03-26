using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FasePractica.Data.Models
{
    [Table("Proyectos")]
    public class Proyecto
    {
        public int ProyectoId { get; set; }

        [Display(Name = "Semestre")]
        [ForeignKey("Semestre")]
        public int SemestreId { get; set; }
        public Semestre Semestre { get; set; }

        [Display(Name = "Empresa")]
        [ForeignKey("Empresa")]
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        [MaxLength(100)]
        [Required]
        public string Nombre { get; set; }
        
        [Display(Name = "Situación actual")]
        public string SituacionActual {get; set;}

        [Display(Name = "Objetivo")]
        public string Objetivo {get; set;}

        [Display(Name = "Descripción")]
        [MaxLength(500)]
        [Required]
        public string Descripcion { get; set; }

        [Display(Name = "Indicador")]
        public string Indicador {get; set;}
        
        [Display(Name = "Meta")]
        public string Meta {get; set;}

        [Display(Name = "Beneficios esperados")]
        public string Beneficios {get; set;}

        [Display(Name = "Comentario")]
        public string Comentario {get; set;}

        [Display(Name = "Tecnología")]
        [MaxLength(150)]
        [Required]
        public string Tecnologia { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public DateTime RealizadoEl {get; set;}

        [Display(Name = "Tutor académico")]
        [ForeignKey("TutorAcademico")]
        public int TutorId { get; set; }
        public Tutor TutorAcademico { get; set; }

        [Display(Name = "Tutor empresarial")]
        [ForeignKey("TutorEmpresarial")]
        public int ContactoId { get; set; }
        public Contacto TutorEmpresarial { get; set; }

        [Display(Name = "Almacenado en")]
        [MaxLength(500)]
        [Url]
        public string AlmacenadoEn { get; set; }

        public List<Nota> Notas { get; set; }

        [NotMapped]
        public string DataValueField { get { return $"{Nombre} {Semestre?.DataValueField} {Empresa?.DataValueField}"; } }
    }
}
