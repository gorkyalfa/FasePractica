using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FasePractica.WebApp.Models
{
    public class Contacto : Persona
    {
        [Display(Name = "Título profesional")]
        [Required]
        [MaxLength(50)]
        public string TituloProfesional { get; set; }

        [Display(Name = "Cargo en la empresa")]
        [Required]
        [MaxLength(50)]
        public string CargoEmpresa { get; set; }      

        [ForeignKey("Empresa")]
        [Display(Name = "Empresa")]
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        
        [NotMapped]
        public string DataValueField { get { return $"{Apellidos} {Nombres}, {TituloProfesional}"; } }
    }
}