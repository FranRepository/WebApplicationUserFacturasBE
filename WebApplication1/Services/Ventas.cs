using WebApplicationBE.Interfaces;
using WebApplicationBE.Models;

namespace WebApplicationBE.Services
{
    public class Ventas
    {
        private readonly IFacturaRepository _facturaRepository;
        private readonly IPersonaRepository _personaRepository;

        public Ventas(IFacturaRepository facturaRepository, IPersonaRepository personaRepository)
        {
            _facturaRepository = facturaRepository;
            _personaRepository = personaRepository;
        }

        public async Task<List<Factura>> FindFacturasByPersona(int idPersona)
        {
            // Aquí puedes implementar la lógica para buscar facturas por persona
            var facturas = await _facturaRepository.GetAllAsync();
            return facturas.Where(f => f.IdPersona == idPersona).ToList();
        }

        public async Task StoreFactura(Factura factura)
        {
            // Aquí puedes implementar la lógica para almacenar una factura
            await _facturaRepository.AddAsync(factura);
        }
    }

}
