using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FasePractica.WebApp.Models
{
    public class Tutor : Persona
    {
        [Display(Name = "Título profesional")]
        [MaxLength(50)]
        [Required]
        public string TituloProfesional { get; set; }

        public List<Empresa> Empresas { get; set; }
        
        [Display(Name = "Código Ignug")]
        [MaxLength(32)]
        public string CodigoIgnug { get; set; }
        
        [NotMapped]
        public string DataValueField { get { return $"{Apellidos} {Nombres}, {TituloProfesional}"; } }
    }
}
