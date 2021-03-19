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
    public class SemestresController : Controller
    {
        private readonly TenantDbContext _context;
        private IConfiguration _configuration;

        public SemestresController(TenantDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Semestres
        public async Task<IActionResult> Index(int? pagina)
        {
            var tamanoPagina = _configuration.GetValue<int>("TamanoPagina");
            if(pagina==null || pagina<=0)
            {
                pagina=1;
            }
            var skip = ((int)pagina-1)*tamanoPagina;
            var semestres = await _context.Semestres.Skip(skip).Take(tamanoPagina).ToListAsync();
            var totalSemestres= _context.Semestres.Count();
            int totalPaginas = totalSemestres/tamanoPagina;
            if(totalSemestres%tamanoPagina!=0)
            {
                totalPaginas+=1;
            }
            ViewData["PaginaActual"] = pagina;
            ViewData["TotalPaginas"] = totalPaginas;
            return View(semestres);

            //return View(await _context.Semestres.ToListAsync());
        }

        // GET: Semestres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semestre = await _context.Semestres
                .FirstOrDefaultAsync(m => m.SemestreId == id);
            if (semestre == null)
            {
                return NotFound();
            }

            return View(semestre);
        }

        // GET: Semestres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Semestres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SemestreId,FechaInicio,FechaFin,Nombre")] Semestre semestre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(semestre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(semestre);
        }

        // GET: Semestres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semestre = await _context.Semestres.FindAsync(id);
            if (semestre == null)
            {
                return NotFound();
            }
            return View(semestre);
        }

        // POST: Semestres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SemestreId,FechaInicio,FechaFin,Nombre")] Semestre semestre)
        {
            if (id != semestre.SemestreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(semestre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SemestreExists(semestre.SemestreId))
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
            return View(semestre);
        }

        // GET: Semestres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semestre = await _context.Semestres
                .FirstOrDefaultAsync(m => m.SemestreId == id);
            if (semestre == null)
            {
                return NotFound();
            }

            return View(semestre);
        }

        // POST: Semestres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var semestre = await _context.Semestres.FindAsync(id);
            _context.Semestres.Remove(semestre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SemestreExists(int id)
        {
            return _context.Semestres.Any(e => e.SemestreId == id);
        }
    }
}
