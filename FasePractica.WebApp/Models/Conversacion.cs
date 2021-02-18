using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FasePractica.WebApp.Models
{
    [Table("Conversaciones")]
    public class Conversacion
    {
        public int ConversacionId { get; set; }
        [Display(Name = "Realizado el")]
        [Required]
        public DateTime RealizadoEl { get; set; }
        [ForeignKey("Empresa")]
        [Display(Name = "Empresa")]
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        [MaxLength(500)]
        [Required]
        public string Observaciones { get; set; }
        [Required]
        public EstadoConversacion Estado { get; set; }
    }
}
