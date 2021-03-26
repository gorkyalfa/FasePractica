using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FasePractica.Data.Models
{
    [Table("Empresas")]
    public class Empresa
    {
        public int EmpresaId { get; set; }
        
        [MaxLength(100)]
        [Required]
        public string Nombre { get; set; }
        
        [MaxLength(50)]
        [Required]
        public string Alias { get; set; }
        
        [Display(Name = "Tipo de empresa")]
        [Required]
        public TipoEmpresa TipoEmpresa { get; set; }
        
        [Display(Name = "Tipo de persona")]
        [Required]
        public TipoPersona TipoPersona { get; set; }
        
        [MaxLength(13)]
        [Required]
        public string Ruc { get; set; }
        
        [Display(Name = "Teléfono")]
        [Phone]
        [MaxLength(15)]
        [Required]
        public string Telefono { get; set; }
        
        [EmailAddress]
        [MaxLength(100)]
        [Required]
        public string Correo { get; set; }
        
        [Display(Name = "Sector productivo")]
        [MaxLength(50)]
        [Required]
        public string SectorProductivo { get; set; }
        
        [Display(Name = "Dirección")]
        [MaxLength(150)]
        [Required]
        public string Direccion { get; set; }
        
        [MaxLength(15)]
        [Required]
        public string Latitud { get; set; }
        
        [MaxLength(15)]
        [Required]
        public string Longitud { get; set; }
        
        [Display(Name = "Descripción")]
        [MaxLength(150)]
        [Required]
        public string Descripcion { get; set; }

        [ForeignKey("Tutor")]
        [Display(Name = "Tutor")]
        public int TutorId { get; set; }
        public Tutor Tutor { get; set; }

        [Display(Name = "Estudiantes convenio")]
        [Required]
        public int EstudiantesConvenio { get; set; }

        [Display(Name = "Estudiantes actuales")]
        [Required]
        public int EstudiantesActuales { get; set; }

        public List<Documento> Documentos { get; set; }

        public List<Contacto> Contactos { get; set; }
        
        public List<Conversacion> Conversaciones { get; set; }

        public List<Proyecto> Proyectos { get; set; }

        [NotMapped]
        public string DataValueField { get { return $"{Alias}"; } }
    }
}
