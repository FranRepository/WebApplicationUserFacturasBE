using Microsoft.EntityFrameworkCore;
using WebApplicationBE.Data;
using WebApplicationBE.Interfaces;
using WebApplicationBE.Models;

namespace WebApplicationBE.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly ApplicationDbContext _context;

        public FacturaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Factura>> GetAllAsync()
        {
            return await _context.Facturas.ToListAsync();
        }
        public IEnumerable<Factura> GetAllByPersonaId(int personaId)
        {
            return _context.Facturas.Where(f => f.IdPersona == personaId).ToList();
        }

        public async Task<Factura> GetByIdAsync(int id)
        {
            return await _context.Facturas.FindAsync(id);
        }

        public Factura GetById(int id)
        {
            return _context.Facturas.FirstOrDefault(f => f.Id == id);
        }

        public async Task AddAsync(Factura factura)
        {
            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Factura factura)
        {
            _context.Entry(factura).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
                await _context.SaveChangesAsync();
            }
        }
    }
}
