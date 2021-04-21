using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FasePractica.Data.Models
{
    public class Estudiante : Persona
    {
        [Display(Name = "Código Ignug")]
        [MaxLength(32)]
        public string CodigoIgnug { get; set; }
        [ForeignKey("Carrera")]
        public int CarreraId {get; set;}
        public Carrera Carrera {get; set;}
        public List<Nota> Notas { get; set; }
        [NotMapped]
        public string DataValueField { get { return $"{Apellidos} {Nombres}, {Cedula}"; } }
    }

    public class EstudianteComparador : IComparer<Estudiante>
    {
        public int Compare([AllowNull] Estudiante x, [AllowNull] Estudiante y)
        {
            if (x == null)
            {
                if (y == null)
                    return 0;
                return 1;
            }
            else 
            {
                if (y == null)
                    return -1;
            }
            return x.DataValueField.CompareTo(y.DataValueField);
        }
    }
}
