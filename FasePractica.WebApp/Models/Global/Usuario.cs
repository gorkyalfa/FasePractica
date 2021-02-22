using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FasePractica.WebApp.Models.Global
{
    [Table("Usuarios")]
    public class Usuario
    {
        public int UsuarioId { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Nombres y apellidos")]
        public string NombresApellidos { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Correo { get; set; }

        public List<UsuarioPorInstituto> Institutos { get; set; }
    }
}
