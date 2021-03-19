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
    public class NotasController : Controller
    {
        private readonly TenantDbContext _context;
        private IConfiguration _configuration;

        public NotasController(TenantDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Notas
        public async Task<IActionResult> Index(int? pagina)
        {
            //var applicationDbContext = _context.Notas.Include(n => n.Proyecto).Include(n => n.Estudiante);
            
            var tamanoPagina = _configuration.GetValue<int>("TamanoPagina");
            if(pagina==null || pagina<=0)
            {
                pagina=1;
            }
            var skip = ((int)pagina-1)*tamanoPagina;
            var notas = await _context.Notas.Include(n => n.Proyecto).Include(n => n.Estudiante).Skip(skip).Take(tamanoPagina).ToListAsync();
            var totalNotas = _context.Notas.Count();
            int totalPaginas = totalNotas/tamanoPagina;
            if(totalNotas%tamanoPagina!=0)
            {
                totalPaginas+=1;
            }
            ViewData["PaginaActual"] = pagina;
            ViewData["TotalPaginas"] = totalPaginas;
            return View(notas);

            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: Notas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas
                .Include(n => n.Proyecto)
                .Include(n => n.Estudiante)
                .FirstOrDefaultAsync(m => m.NotaId == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // GET: Notas/Create
        public IActionResult Create()
        {
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos.Include(p=>p.Empresa).Include(p => p.Semestre), "ProyectoId", "DataValueField");
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "PersonaId", "DataValueField");

            return View();
        }

        // POST: Notas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NotaId,EstudianteId,ProyectoId,Calificacion,Aprueba")] Nota nota)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos.Include(p => p.Empresa).Include(p => p.Semestre), "ProyectoId", "DataValueField", nota.ProyectoId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "PersonaId", "DataValueField", nota.EstudianteId);
            return View(nota);
        }

        // GET: Notas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas
                .Include(n => n.Proyecto)
                .Include(n => n.Estudiante)
                .FirstOrDefaultAsync(n=>n.NotaId == id);
            if (nota == null)
            {
                return NotFound();
            }
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos.Include(p => p.Empresa).Include(p => p.Semestre), "ProyectoId", "DataValueField", nota.ProyectoId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "PersonaId", "DataValueField", nota.EstudianteId);
            return View(nota);
        }

        // POST: Notas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NotaId,EstudianteId,ProyectoId,Calificacion,Aprueba")] Nota nota)
        {
            if (id != nota.NotaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotaExists(nota.NotaId))
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
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos.Include(p => p.Empresa).Include(p => p.Semestre), "ProyectoId", "DataValueField", nota.ProyectoId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "PersonaId", "DataValueField", nota.EstudianteId);
            return View(nota);
        }

        // GET: Notas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nota = await _context.Notas
                .Include(n => n.Proyecto)
                .Include(n => n.Estudiante)
                .FirstOrDefaultAsync(m => m.NotaId == id);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);
        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nota = await _context.Notas.FindAsync(id);
            _context.Notas.Remove(nota);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotaExists(int id)
        {
            return _context.Notas.Any(e => e.NotaId == id);
        }
    }
}
