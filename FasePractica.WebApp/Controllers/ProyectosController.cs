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
using System.Diagnostics;
using System.Collections;

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
            if (pagina == null || pagina <= 0)
            {
                pagina = 1;
            }
            var skip = ((int)pagina - 1) * tamanoPagina;
            var proyectos = await _context.Proyectos.Include(p => p.Empresa).Include(p => p.Semestre).Include(p => p.TutorAcademico).Include(p => p.TutorEmpresarial).Skip(skip).Take(tamanoPagina).ToListAsync();
            var totalProyectos = _context.Proyectos.Count();
            int totalPaginas = totalProyectos / tamanoPagina;
            if (totalProyectos % tamanoPagina != 0)
            {
                totalPaginas += 1;
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
        public async Task<IActionResult> Create([Bind("ProyectoId,SemestreId,EmpresaId,Nombre,SituacionActual,Objetivo,Descripcion,Indicador,Meta,Beneficios,Comentario,Tecnologia,,RealizadoEl,TutorId,ContactoId,AlmacenadoEn")] Proyecto proyecto)
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
        public async Task<IActionResult> Edit(int id, [Bind("ProyectoId,SemestreId,EmpresaId,Nombre,SituacionActual,Objetivo,Descripcion,Indicador,Meta,Beneficios,Comentario,Tecnologia,RealizadoEl,TutorId,ContactoId,AlmacenadoEn")] Proyecto proyecto)
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

        public IActionResult Reporte(int? id)
        {
            var proyecto = _context.Proyectos
                .Include(p => p.Notas).ThenInclude(p => p.Estudiante)
                .SingleOrDefault(p => p.ProyectoId == id);
            var estudiantes = new List<Estudiante>();
            foreach (var nota in proyecto.Notas)
            {
                estudiantes.Add(nota.Estudiante);
            }
            estudiantes.Sort(new EstudianteComparador());
            ViewData["Estudiantes"] = estudiantes;
            return View(proyecto);
        }

        public IActionResult ReportePorEstudiante(int proyectoId, int personaId)
        {
            var proyecto = _context.Proyectos
                .Include(p => p.Notas).ThenInclude(p => p.Estudiante)
                .Include(p => p.Notas).ThenInclude(p => p.Nivel).ThenInclude(p => p.Carrera)
                .Include(p => p.Empresa)
                .Include(p => p.Semestre)
                .Include(p => p.TutorAcademico)
                .Include(p => p.TutorEmpresarial)
                .SingleOrDefault(p => p.ProyectoId == proyectoId);
            if (proyecto == null)
            {
                return NotFound();
            }
            var proyectoViewModel = new ProyectoViewModel();
            proyectoViewModel.ProyectoId = proyecto.ProyectoId;
            proyectoViewModel.NombreApellidoEstudiante = $"{proyecto.Notas.Single(n => n.Estudiante.PersonaId == personaId).Estudiante.Nombres} {proyecto.Notas.Single(n => n.Estudiante.PersonaId == personaId).Estudiante.Apellidos}";
            proyectoViewModel.NombreDeCarrera = proyecto.Notas.Single(n => n.Estudiante.PersonaId == personaId).Nivel.Carrera.Nombre;
            proyectoViewModel.NivelEstudiante = proyecto.Notas.Single(n => n.Estudiante.PersonaId == personaId).Nivel.Nombre;
            proyectoViewModel.PeriodoLectivoEstudiante = proyecto.Semestre.Nombre;
            proyectoViewModel.NombreEmpresaFormadora = proyecto.Empresa.Nombre;
            proyectoViewModel.HorasFormacionParactica = proyecto.Notas.Single(n => n.Estudiante.PersonaId == personaId).Nivel.HorasPractica;
            proyectoViewModel.NombreProyecto = proyecto.Nombre;
            proyectoViewModel.SituacionActual = proyecto.SituacionActual;
            proyectoViewModel.ObjetivoProyecto = proyecto.Objetivo;
            proyectoViewModel.Descripcion = proyecto.Descripcion;
            proyectoViewModel.Indicador = proyecto.Indicador;
            proyectoViewModel.Meta = proyecto.Meta;
            proyectoViewModel.Beneficios = proyecto.Beneficios;
            proyectoViewModel.Comentario = proyecto.Comentario;
            proyectoViewModel.FechaProyecto = proyecto.RealizadoEl;
            proyectoViewModel.NombreCoordinadorCarrera = $"{proyecto.TutorAcademico.Nombres} {proyecto.TutorAcademico.Apellidos}";
            proyectoViewModel.NombreTutorEmpresarial = $"{proyecto.TutorEmpresarial.Nombres} {proyecto.TutorEmpresarial.Apellidos}";
            proyectoViewModel.LogoMinisterio = proyecto.Notas.Single(n => n.Estudiante.PersonaId == personaId).Nivel.Carrera.Logo;

            return View(proyectoViewModel);
        }

        private bool ProyectoExists(int id)
        {
            return _context.Proyectos.Any(e => e.ProyectoId == id);
        }

    }
}
