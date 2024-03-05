using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationBE.Models
{
    [Table("Personas")]
    public class Persona
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Identificacion { get; set; }

      //  Agrega la siguiente propiedad para representar las facturas asociadas a una persona
          public List<Factura>? Facturas { get; set; }
    }
}
