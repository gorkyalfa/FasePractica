using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FasePractica.WebApp.Models
{
    [NotMapped]
    public abstract class Persona
    {
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
        [EmailAddress]
        [MaxLength(100)]
        [Required]
        public string CorreoInstitucional { get; set; }
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
