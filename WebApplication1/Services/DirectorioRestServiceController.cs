using Microsoft.AspNetCore.Mvc;
using WebApplicationBE.Interfaces;
using WebApplicationBE.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplicationBE.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorioRestServiceController : ControllerBase
    {
        private readonly IPersonaRepository _personaRepository;

        public DirectorioRestServiceController(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetAllPersonas()
        {
            var personas = await _personaRepository.GetAllAsync();
            return Ok(personas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> GetPersona(int id)
        {
            var persona = await _personaRepository.GetByIdAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return persona;
        }

        [HttpPost]
        public async Task<ActionResult<Persona>> CreatePersona(Persona persona)
        {
          
            try
            {
                await _personaRepository.AddAsync(persona);
                return CreatedAtAction(nameof(GetPersona), new { id = persona.Id }, persona);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message +" "+ e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersona(int id, [FromBody] Persona persona)
        {
            try
            {
                if (id != persona.Id)
                {
                    return BadRequest("El ID proporcionado en la ruta no coincide con el ID de la persona proporcionada");
                }

                var existingPersona = await _personaRepository.GetByIdAsync(id);
                if (existingPersona == null)
                {
                    return NotFound("La persona no fue encontrada");
                }

                await _personaRepository.UpdateAsync(persona);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            try
            {
                await _personaRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
