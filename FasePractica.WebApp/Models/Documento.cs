using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FasePractica.WebApp.Models
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
        [Display(Name = "Almacenado en")]
        [MaxLength(500)]
        [Url]
        [Required]
        public string AlmacenadoEn { get; set; }
        [Required]
        public Estado Estado { get; set; }
    }
}
