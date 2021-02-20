using System.ComponentModel.DataAnnotations.Schema;

namespace FasePractica.WebApp.Models.Global
{
    [Table("UsuariosPorInstitutos")]
    public class UsuarioPorInstituto
    {
        public int UsuarioPorInstitutoId { get; set; }
        
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        [ForeignKey("Instituto")]
        public int InstitutoId { get; set; }
        public Instituto Instituto { get; set; }
    }
}
