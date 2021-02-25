using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FasePractica.Data.Models
{
    [Table("Documentos")]
    public class Documento
    {
        public int DocumentoId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Código")]
        [Required]
        public string Codigo { get; set; }
        
        [Display(Name = "Firmado el")]
        [Required]
        public DateTime FirmadoEl { get; set; }

        [Display(Name = "Finalización")]
        public DateTime? FechaFinal { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(500)]
        [Required]
        public string Descripcion { get; set; }
        
        [Display(Name = "Almacenado en")]
        [MaxLength(500)]
        [Url]
        [Required]
        public string AlmacenadoEn { get; set; }
        
        [Required]
        public Estado Estado { get; set; }

        [Required]
        public TipoDocumento Tipo { get; set; }

        [ForeignKey("Empresa")]
        [Display(Name = "Empresa")]
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        [NotMapped]
        public string DataValueField { get { return Codigo; } }
    }
}