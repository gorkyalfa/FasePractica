using System.Collections.Generic;

namespace FasePractica.WebApp.Models
{
    public class Empresa
    {
        public int EmpresaId { get; set; }
        public string Nombre { get; set; }
        public string Alias { get; set; }
        public TipoEmpresa TipoEmpresa { get; set; }
        public Persona Persona { get; set; }
        public string Ruc { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string SectorProductivo { get; set; }
        public string Direccion { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Descripcion { get; set; }
        public List<Contacto> Contactos { get; set; }
        

    }
}
