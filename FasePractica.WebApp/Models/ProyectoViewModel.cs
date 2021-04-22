using System;

namespace FasePractica.WebApp.Models
{
    public class ProyectoViewModel
    {

        public int ProyectoId { get; set; }
        public string NombreApellidoEstudiante { get; set; }
        public string NombreDeCarrera { get; set; }
        public string NivelEstudiante { get; set; }
        public string PeriodoLectivoEstudiante { get; set; }
        public string NombreEmpresaFormadora { get; set; }
        public int HorasFormacionParactica { get; set; }
        public string NombreProyecto { get; set; }
        public string SituacionActual { get; set; }
        public string ObjetivoProyecto { get; set; }
        public string Descripcion { get; set; }
        public string Indicador { get; set; }
        public string Meta { get; set; }
        public string Beneficios { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaProyecto { get; set; }
        public string NombreCoordinadorCarrera { get; set; }
        public string NombreTutorEmpresarial { get; set; }
        public string LogoMinisterio { get; set; }
    }
}
