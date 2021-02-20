using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FasePractica.WebApp.Models
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

        [Display(Name = "Descripción")]
        [MaxLength(500)]
        [Required]
        public string Descripcion { get; set; }
        [Display(Name = "Tecnología")]
        [MaxLength(150)]
        [Required]
        public string Tecnologia { get; set; }

        [Display(Name = "Tutor académico")]
        [ForeignKey("TutorAcademico")]
        public int TutorId { get; set; }
        public Tutor TutorAcademico { get; set; }

        [Display(Name = "Tutor empresarial")]
        [ForeignKey("TutorEmpresarial")]
        public int ContactoId { get; set; }
        public Contacto TutorEmpresarial { get; set; }

        public List<Nota> Notas { get; set; }

        [NotMapped]
        public string DataValueField { get { return $"{Semestre.DataValueField} {Nombre} {Empresa.DataValueField}"; } }
    }
}
