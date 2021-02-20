using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FasePractica.WebApp.Models.Global
{
    [Table("Institutos")]
    public class Instituto
    {
        public int InstitutoId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        
        [Required]
        [MaxLength(10)]
        public string TenantName { get; set; }

        public List<UsuarioPorInstituto> UsuarioPorInstituto { get; set; }
    }
}
