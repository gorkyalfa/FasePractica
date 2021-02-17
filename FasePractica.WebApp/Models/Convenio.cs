using System.Collections.Generic;

namespace FasePractica.WebApp.Models
{
    public class Convenio : Documento
    {
        public int ConvenioId { get; set; }
        public string FechaFinal { get; set; }
        public string Descripcion { get; set; }
        public Empresa Empresa { get; set; }
        public Documento InformeViabilidad { get; set; }
        public Documento Memorando { get; set; }
        public Documento Acta { get; set; }
        public List<Documento> Extension { get; set; }
        public int CantidadEstudiantes { get; set; }



    }
}
