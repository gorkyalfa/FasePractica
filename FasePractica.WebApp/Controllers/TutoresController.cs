using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FasePractica.Data;
using FasePractica.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace FasePractica.WebApp.Controllers
{
    [Authorize]
    public class TutoresController : Controller
    {
        private readonly TenantDbContext _context;
        private IConfiguration _configuration;

        public TutoresController(TenantDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Tutores
        public async Task<IActionResult> Index(int? pagina)
        {
            var tamanoPagina = _configuration.GetValue<int>("TamanoPagina");
            if(pagina==null || pagina<=0)
            {
                pagina=1;
            }
            var skip = ((int)pagina-1)*tamanoPagina;
            var tutores = await _context.Tutores.Skip(skip).Take(tamanoPagina).ToListAsync();
            var totalTutores = _context.Tutores.Count();
            int totalPaginas = totalTutores/tamanoPagina;
            if(totalTutores%tamanoPagina!=0)
            {
                totalPaginas+=1;
            }
            ViewData["PaginaActual"] = pagina;
            ViewData["TotalPaginas"] = totalPaginas;
            return View(tutores);
        }

        // GET: Tutores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutor = await _context.Tutores
                .FirstOrDefaultAsync(m => m.PersonaId == id);
            if (tutor == null)
            {
                return NotFound();
            }

            return View(tutor);
        }

        // GET: Tutores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tutores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TituloProfesional,CodigoIgnug,PersonaId,Nombres,Apellidos,Cedula,CorreoInstitucional,CorreoPersonal,Telefono")] Tutor tutor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tutor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tutor);
        }

        // GET: Tutores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutor = await _context.Tutores.FindAsync(id);
            if (tutor == null)
            {
                return NotFound();
            }
            return View(tutor);
        }

        // POST: Tutores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TituloProfesional,CodigoIgnug,PersonaId,Nombres,Apellidos,Cedula,CorreoInstitucional,CorreoPersonal,Telefono")] Tutor tutor)
        {
            if (id != tutor.PersonaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tutor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TutorExists(tutor.PersonaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tutor);
        }

        // GET: Tutores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutor = await _context.Tutores
                .FirstOrDefaultAsync(m => m.PersonaId == id);
            if (tutor == null)
            {
                return NotFound();
            }

            return View(tutor);
        }

        // POST: Tutores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tutor = await _context.Tutores.FindAsync(id);
            _context.Tutores.Remove(tutor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TutorExists(int id)
        {
            return _context.Tutores.Any(e => e.PersonaId == id);
        }
    }
}
