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
    public class EmpresasController : Controller
    {
        private readonly TenantDbContext _context;

        public EmpresasController(TenantDbContext context)
        {
            _context = context;
        }

        // GET: Empresas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Empresas.Include(e => e.Tutor).ToListAsync());
        }

        // GET: Empresas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas.Include(e => e.Tutor)
                .FirstOrDefaultAsync(m => m.EmpresaId == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // GET: Empresas/Create
        public IActionResult Create()
        {
            var tiposEmpresa = new[]
            {
                new { TipoEmpresa = (int)TipoEmpresa.Privado, DataValueField = Enum.GetName(typeof(TipoEmpresa), TipoEmpresa.Privado) },
                new { TipoEmpresa = (int)TipoEmpresa.Publico, DataValueField = Enum.GetName(typeof(TipoEmpresa), TipoEmpresa.Publico) }
            };
            ViewData["TipoEmpresa"] = new SelectList(tiposEmpresa, "TipoEmpresa", "DataValueField");

            var tipoPersona = new[]
            {
                new { TipoPersona = (int)TipoPersona.Juridica, DataValueField = Enum.GetName(typeof(TipoPersona), TipoPersona.Juridica) },
                new { TipoPersona = (int)TipoPersona.Natural, DataValueField = Enum.GetName(typeof(TipoPersona), TipoPersona.Natural) }
            };
            ViewData["TipoPersona"] = new SelectList(tipoPersona, "TipoPersona", "DataValueField");
            ViewData["TutorId"] = new SelectList(_context.Tutores, "PersonaId", "DataValueField");
            return View();
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpresaId,Nombre,Alias,TipoEmpresa,TipoPersona,Ruc,Telefono,Correo,SectorProductivo,Direccion,Latitud,Longitud,Descripcion,TutorId,EstudiantesConvenio,EstudiantesActuales")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empresa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var tiposEmpresa = new[]
            {
                new { TipoEmpresa = (int)TipoEmpresa.Privado, DataValueField = Enum.GetName(typeof(TipoEmpresa), TipoEmpresa.Privado) },
                new { TipoEmpresa = (int)TipoEmpresa.Publico, DataValueField = Enum.GetName(typeof(TipoEmpresa), TipoEmpresa.Publico) }
            };
            ViewData["TipoEmpresa"] = new SelectList(tiposEmpresa, "TipoEmpresa", "DataValueField", empresa.TipoEmpresa);

            var tipoPersona = new[]
            {
                new { TipoPersona = (int)TipoPersona.Juridica, DataValueField = Enum.GetName(typeof(TipoPersona), TipoPersona.Juridica) },
                new { TipoPersona = (int)TipoPersona.Natural, DataValueField = Enum.GetName(typeof(TipoPersona), TipoPersona.Natural) }
            };
            ViewData["TipoPersona"] = new SelectList(tipoPersona, "TipoPersona", "DataValueField", empresa.TipoPersona);
            ViewData["TutorId"] = new SelectList(_context.Tutores, "PersonaId", "DataValueField", empresa.TutorId);
            return View(empresa);
        }

        // GET: Empresas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }
            var tiposEmpresa = new[]
            {
                new { TipoEmpresa = (int)TipoEmpresa.Privado, DataValueField = Enum.GetName(typeof(TipoEmpresa), TipoEmpresa.Privado) },
                new { TipoEmpresa = (int)TipoEmpresa.Publico, DataValueField = Enum.GetName(typeof(TipoEmpresa), TipoEmpresa.Publico) }
            };
            ViewData["TipoEmpresa"] = new SelectList(tiposEmpresa, "TipoEmpresa", "DataValueField", empresa.TipoEmpresa);

            var tipoPersona = new[]
            {
                new { TipoPersona = (int)TipoPersona.Juridica, DataValueField = Enum.GetName(typeof(TipoPersona), TipoPersona.Juridica) },
                new { TipoPersona = (int)TipoPersona.Natural, DataValueField = Enum.GetName(typeof(TipoPersona), TipoPersona.Natural) }
            };
            ViewData["TipoPersona"] = new SelectList(tipoPersona, "TipoPersona", "DataValueField", empresa.TipoPersona);
            ViewData["TutorId"] = new SelectList(_context.Tutores, "PersonaId", "DataValueField", empresa.TutorId);
            return View(empresa);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpresaId,Nombre,Alias,TipoEmpresa,TipoPersona,Ruc,Telefono,Correo,SectorProductivo,Direccion,Latitud,Longitud,Descripcion,TutorId,EstudiantesConvenio,EstudiantesActuales")] Empresa empresa)
        {
            if (id != empresa.EmpresaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empresa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaExists(empresa.EmpresaId))
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
            var tiposEmpresa = new[]
            {
                new { TipoEmpresa = (int)TipoEmpresa.Privado, DataValueField = Enum.GetName(typeof(TipoEmpresa), TipoEmpresa.Privado) },
                new { TipoEmpresa = (int)TipoEmpresa.Publico, DataValueField = Enum.GetName(typeof(TipoEmpresa), TipoEmpresa.Publico) }
            };
            ViewData["TipoEmpresa"] = new SelectList(tiposEmpresa, "TipoEmpresa", "DataValueField", empresa.TipoEmpresa);

            var tipoPersona = new[]
            {
                new { TipoPersona = (int)TipoPersona.Juridica, DataValueField = Enum.GetName(typeof(TipoPersona), TipoPersona.Juridica) },
                new { TipoPersona = (int)TipoPersona.Natural, DataValueField = Enum.GetName(typeof(TipoPersona), TipoPersona.Natural) }
            };
            ViewData["TipoPersona"] = new SelectList(tipoPersona, "TipoPersona", "DataValueField", empresa.TipoPersona);
            ViewData["TutorId"] = new SelectList(_context.Tutores, "PersonaId", "DataValueField", empresa.TutorId);
            return View(empresa);
        }

        // GET: Empresas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .FirstOrDefaultAsync(m => m.EmpresaId == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresaExists(int id)
        {
            return _context.Empresas.Any(e => e.EmpresaId == id);
        }
    }
}
