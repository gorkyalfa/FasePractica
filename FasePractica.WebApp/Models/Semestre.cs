using System;
using System.Collections.Generic;

namespace FasePractica.WebApp.Models
{
    public class Semestre
    {
        public int SemestreId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Nombre { get; set; }
        public List<Proyecto> Proyectos { get; set; }
    }
}
