using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FasePractica.Data;
using FasePractica.Data.Models;

namespace FasePractica.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesApiController : ControllerBase
    {
        private readonly TenantDbContext _context;

        public EstudiantesApiController(TenantDbContext context)
        {
            _context = context;
        }

        // GET: api/EstudiantesApi
        [HttpGet]
        public async Task<ActionResult<Object>> GetEstudiantes(string criterios)
        {
            criterios = criterios.ToLower();
            var estudiantes = await _context.Estudiantes.Where(e => e.Apellidos.ToLower().Contains(criterios)
            || e.Nombres.ToLower().Contains(criterios)
            || e.Cedula.Contains(criterios)).ToListAsync();
            return estudiantes.Select(e => new { e.PersonaId, e.DataValueField}).ToArray();
        }

        // GET: api/EstudiantesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estudiante>> GetEstudiante(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);

            if (estudiante == null)
            {
                return NotFound();
            }

            return estudiante;
        }

        // PUT: api/EstudiantesApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstudiante(int id, Estudiante estudiante)
        {
            if (id != estudiante.PersonaId)
            {
                return BadRequest();
            }

            _context.Entry(estudiante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstudianteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EstudiantesApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Estudiante>> PostEstudiante(Estudiante estudiante)
        {
            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstudiante", new { id = estudiante.PersonaId }, estudiante);
        }

        // DELETE: api/EstudiantesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstudiante(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }

            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstudianteExists(int id)
        {
            return _context.Estudiantes.Any(e => e.PersonaId == id);
        }
    }
}
