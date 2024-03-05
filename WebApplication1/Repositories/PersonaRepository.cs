using Microsoft.EntityFrameworkCore;
using WebApplicationBE.Data;
using WebApplicationBE.Interfaces;
using WebApplicationBE.Models;

namespace WebApplicationBE.Repositories
{

    public class PersonaRepository : IPersonaRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Persona>> GetAllAsync()
        {
            return await _context.Personas.ToListAsync();
        }

        public async Task<Persona> GetByIdAsync(int id)
        {
            return await _context.Personas.FindAsync(id);
        }
        public Persona GetById(int id)
        {
            return _context.Personas.FirstOrDefault(p => p.Id == id);
        }
        public async Task AddAsync(Persona persona)
        {
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Persona persona)
        {
            _context.Entry(persona).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
                await _context.SaveChangesAsync();
            }
        }

        public void DeleteByIdentificacion(string identificacion)
        {
            var persona = _context.Personas.FirstOrDefault(p => p.Identificacion == identificacion);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
                _context.SaveChanges();
            }
        }

        public async Task<Persona> FindByIdentificacion(string identificacion)
        {
            return await _context.Personas.FirstOrDefaultAsync(p => p.Identificacion == identificacion);
        }
    }
}
