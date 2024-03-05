using WebApplicationBE.Interfaces;
using WebApplicationBE.Models;

namespace WebApplicationBE.Services
{
    public class Directorio
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IFacturaRepository _facturaRepository;

        public Directorio(IPersonaRepository personaRepository, IFacturaRepository facturaRepository)
        {
            _personaRepository = personaRepository;
            _facturaRepository = facturaRepository;
        }

        public async Task<Persona> FindPersonaByIdentificacion(string identificacion)
        {
            // Implementa la lógica para encontrar una persona por identificación
            return await _personaRepository.FindByIdentificacion(identificacion);
        }

        public async Task<List<Persona>> FindPersonas()
        {
            // Implementa la lógica para obtener todas las personas
            return await _personaRepository.GetAllAsync();
        }

        public async Task DeletePersonaByIdentificacion(string identificacion)
        {
            // Implementa la lógica para eliminar una persona y sus facturas asociadas
            var persona = await _personaRepository.FindByIdentificacion(identificacion);
            if (persona != null)
            {
                await _personaRepository.DeleteAsync((int)persona.Id);
            }
        }

        public async Task StorePersona(Persona persona)
        {
            // Implementa la lógica para almacenar una nueva persona
            await _personaRepository.AddAsync(persona);
        }
    }

}
