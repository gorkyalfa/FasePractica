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
using FasePractica.WebApp.Models;

namespace FasePractica.WebApp.Controllers
{
    [Authorize]
    public class ProyectosController : Controller
    {
        private readonly TenantDbContext _context;
        private IConfiguration _configuration;

        public ProyectosController(TenantDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Proyectos
        public async Task<IActionResult> Index(int? pagina)
        {
            var tamanoPagina = _configuration.GetValue<int>("TamanoPagina");
            if(pagina==null || pagina<=0)
            {
                pagina=1;
            }
            var skip = ((int)pagina-1)*tamanoPagina;
            var proyectos = await _context.Proyectos.Include(p => p.Empresa).Include(p => p.Semestre).Include(p => p.TutorAcademico).Include(p => p.TutorEmpresarial).Skip(skip).Take(tamanoPagina).ToListAsync();
            var totalProyectos = _context.Proyectos.Count();
            int totalPaginas = totalProyectos/tamanoPagina;
            if(totalProyectos%tamanoPagina!=0)
            {
                totalPaginas+=1;
            }
            ViewData["PaginaActual"] = pagina;
            ViewData["TotalPaginas"] = totalPaginas;
            return View(proyectos);
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

        public async Task<IActionResult> Reporte(int id)
        {
            //int indiceEstudiante = 1;
            //TODO, ANDERSON - Esto se debe proporcionar desde la vista
            //var proyecto = await _context.Proyectos.Include(p => p.Empresa).Include(p => p.Semestre).Include(p => p.TutorAcademico).Include(p => p.TutorEmpresarial).ToListAsync();

            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto == null)
            {
                return NotFound();
            }
            var proyectoViewModel = new ProyectoViewModel();
            
            //ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "Alias", proyecto.EmpresaId);

            var datosNombre = ViewData["EstudianteId"] = new SelectList(_context.Notas, "EstudianteId", "Nombre", proyecto);

            proyectoViewModel.NombreApellidoEstudiante = datosNombre.ToString();
            //List<Nota> Notas = proyecto.Notas.ToList();

            //TODO, ANDERSON - Llenar proyecto view model con los datos para visualizar
            //proyectoViewModel.NombreApellidoEstudiante = proyecto.Notas[indiceEstudiante].EstudianteId;
            //proyectoViewModel.NombreApellidoEstudiante = $"{proyecto.Notas[indiceEstudiante].Estudiante.Nombres} {proyecto.Notas[indiceEstudiante].Estudiante.Apellidos}";

            //proyectoViewModel.NombreApellidoEstudiante = Notas[indiceEstudiante];

            return View(proyectoViewModel);
        }

        private bool ProyectoExists(int id)
        {
            return _context.Proyectos.Any(e => e.ProyectoId == id);
        }
    }
}
