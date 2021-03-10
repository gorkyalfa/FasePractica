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

namespace FasePractica.WebApp.Controllers
{
    [Authorize]
    public class ProyectosController : Controller
    {
        private readonly TenantDbContext _context;

        public ProyectosController(TenantDbContext context)
        {
            _context = context;
        }

        // GET: Proyectos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Proyectos.Include(p => p.Empresa).Include(p => p.Semestre).Include(p => p.TutorAcademico).Include(p => p.TutorEmpresarial);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Proyectos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos
                .Include(p => p.Empresa)
                .Include(p => p.Semestre)
                .Include(p => p.TutorAcademico)
                .Include(p => p.TutorEmpresarial)
                .FirstOrDefaultAsync(m => m.ProyectoId == id);
            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        // GET: Proyectos/Create
        public IActionResult Create()
        {
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "Alias");
            ViewData["SemestreId"] = new SelectList(_context.Semestres, "SemestreId", "Nombre");
            ViewData["TutorId"] = new SelectList(_context.Tutores, "PersonaId", "DataValueField");
            ViewData["ContactoId"] = new SelectList(_context.Contactos, "PersonaId", "DataValueField");
            return View();
        }

        // POST: Proyectos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProyectoId,SemestreId,EmpresaId,Nombre,Descripcion,Tecnologia,TutorId,ContactoId")] Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proyecto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "Alias", proyecto.EmpresaId);
            ViewData["SemestreId"] = new SelectList(_context.Semestres, "SemestreId", "Nombre", proyecto.SemestreId);
            ViewData["TutorId"] = new SelectList(_context.Tutores, "PersonaId", "DataValueField", proyecto.TutorId);
            ViewData["ContactoId"] = new SelectList(_context.Contactos, "PersonaId", "DataValueField", proyecto.ContactoId);
            return View(proyecto);
        }

        // GET: Proyectos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto == null)
            {
                return NotFound();
            }
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "Alias", proyecto.EmpresaId);
            ViewData["SemestreId"] = new SelectList(_context.Semestres, "SemestreId", "Nombre", proyecto.SemestreId);
            ViewData["TutorId"] = new SelectList(_context.Tutores, "PersonaId", "DataValueField", proyecto.TutorId);
            ViewData["ContactoId"] = new SelectList(_context.Contactos, "PersonaId", "DataValueField", proyecto.ContactoId);
            return View(proyecto);
        }

        // POST: Proyectos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProyectoId,SemestreId,EmpresaId,Nombre,Descripcion,Tecnologia,TutorId,ContactoId")] Proyecto proyecto)
        {
            if (id != proyecto.ProyectoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proyecto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProyectoExists(proyecto.ProyectoId))
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
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "Alias", proyecto.EmpresaId);
            ViewData["SemestreId"] = new SelectList(_context.Semestres, "SemestreId", "Nombre", proyecto.SemestreId);
            ViewData["TutorId"] = new SelectList(_context.Tutores, "PersonaId", "DataValueField", proyecto.TutorId);
            ViewData["ContactoId"] = new SelectList(_context.Contactos, "PersonaId", "DataValueField", proyecto.ContactoId);
            return View(proyecto);
        }

        // GET: Proyectos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos
                .Include(p => p.Empresa)
                .Include(p => p.Semestre)
                .Include(p => p.TutorAcademico)
                .Include(p => p.TutorEmpresarial)
                .FirstOrDefaultAsync(m => m.ProyectoId == id);
            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        // POST: Proyectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);
            _context.Proyectos.Remove(proyecto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProyectoExists(int id)
        {
            return _context.Proyectos.Any(e => e.ProyectoId == id);
        }
    }
}
