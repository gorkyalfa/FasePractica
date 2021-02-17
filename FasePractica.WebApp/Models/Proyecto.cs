using System.Collections.Generic;

namespace FasePractica.WebApp.Models
{
    public class Proyecto
    {
        public int ProyectoId { get; set; }
        public List<Nota> Notas { get; set; }
        public Empresa Empresa { get; set; }
        public Semestre Semestre { get; set; }
        public string Descripcion { get; set; }
        public string Tecnologia { get; set; }
        public Tutor TutorAcademico { get; set; }
        public Contacto TutorEmpresarial { get; set; }
    }
}
