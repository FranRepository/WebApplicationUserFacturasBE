using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationBE.Models
{
    [Table("Factura")]
    public class Factura
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }

        // Propiedad de navegación para la relación con Persona
        public int IdPersona { get; set; }
        public Persona Persona { get; set; }
    }
}
