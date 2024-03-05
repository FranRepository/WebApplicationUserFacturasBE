using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationBE.Interfaces;
using WebApplicationBE.Models;

namespace WebApplicationBE.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaRestServiceController : ControllerBase
    {
        private readonly IFacturaRepository _facturaRepository;

        public FacturaRestServiceController(IFacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>>> GetAllFacturas()
        {
            var facturas = await _facturaRepository.GetAllAsync();
            return Ok(facturas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Factura>> GetFactura(int id)
        {
            var factura = await _facturaRepository.GetByIdAsync(id);
            if (factura == null)
            {
                return NotFound();
            }
            return factura;
        }

        [HttpPost]
        public async Task<ActionResult<Factura>> CreateFactura(Factura factura)
        {
            try
            {
                await _facturaRepository.AddAsync(factura);
                return CreatedAtAction(nameof(GetFactura), new { id = factura.Id }, factura);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFactura(int id, Factura factura)
        {
            if (id != factura.Id)
            {
                return BadRequest();
            }

            try
            {
                await _facturaRepository.UpdateAsync(factura);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactura(int id)
        {
            try
            {
                await _facturaRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
