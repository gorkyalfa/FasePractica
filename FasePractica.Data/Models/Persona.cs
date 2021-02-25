using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FasePractica.Data.Models
{
    [Table("Personas")]
    public abstract class Persona
    {
        public int PersonaId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Nombres { get; set; }
        
        [MaxLength(50)]
        [Required]
        public string Apellidos { get; set; }
        
        [Display(Name = "Cédula")]
        [MaxLength(10)]
        [Required]
        public string Cedula { get; set; }

        [Display(Name = "Correo institucional")]
        [EmailAddress]
        [MaxLength(100)]
        [Required]
        public string CorreoInstitucional { get; set; }

        [Display(Name = "Correo personal")]
        [EmailAddress]
        [MaxLength(100)]
        [Required]
        public string CorreoPersonal { get; set; }
        
        [Display(Name = "Teléfono")]
        [Phone]
        [MaxLength(15)]
        [Required]
        public string Telefono { get; set; }
    }
}
