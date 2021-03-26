using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FasePractica.Data.Models
{
    public class Carrera
    {
        public int CarreraId {get; set;}
        public string Nombre {get; set;}
        public string Logo {get; set;}
        public List<Estudiante> Estudiantes {get; set;}
        public List<Nivel> Niveles {get; set;}
    }
}