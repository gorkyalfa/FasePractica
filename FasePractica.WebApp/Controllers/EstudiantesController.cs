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
    public class EstudiantesController : Controller
    {
        private readonly TenantDbContext _context;
        private IConfiguration _configuration;

        public EstudiantesController(TenantDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Estudiantes
        public async Task<IActionResult> Index(int? pagina)
        {
            var tamanoPagina = _configuration.GetValue<int>("TamanoPagina");
            if (pagina == null || pagina <= 0)
            {
                pagina = 1;
            }
            var skip = ((int)pagina - 1) * tamanoPagina;
            var estudiantes = await _context.Estudiantes.Include(e => e.Carrera).Skip(skip).Take(tamanoPagina).ToListAsync();
            var totalEstudiantes = _context.Estudiantes.Count();
            int totalPaginas = totalEstudiantes / tamanoPagina;
            if (totalEstudiantes % tamanoPagina != 0)
            {
                totalPaginas += 1;
            }
            ViewData["PaginaActual"] = pagina;
            ViewData["TotalPaginas"] = totalPaginas;
            return View(estudiantes);
        }

        // GET: Estudiantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiantes
                .Include(e => e.Carrera)
                .FirstOrDefaultAsync(m => m.PersonaId == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // GET: Estudiantes/Create
        public IActionResult Create()
        {
            ViewData["CarreraId"] = new SelectList(_context.Carreras, "CarreraId", "Nombre");
            return View();
        }

        // POST: Estudiantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoIgnug,CarreraId,PersonaId,Nombres,Apellidos,Cedula,CorreoInstitucional,CorreoPersonal,Telefono")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarreraId"] = new SelectList(_context.Carreras, "CarreraId", "Nombre", estudiante.CarreraId);
            return View(estudiante);
        }

        // GET: Estudiantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }
            ViewData["CarreraId"] = new SelectList(_context.Carreras, "CarreraId", "Nombre", estudiante.CarreraId);
            return View(estudiante);
        }

        // POST: Estudiantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodigoIgnug,CarreraId,PersonaId,Nombres,Apellidos,Cedula,CorreoInstitucional,CorreoPersonal,Telefono")] Estudiante estudiante)
        {
            if (id != estudiante.PersonaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudiante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteExists(estudiante.PersonaId))
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
            ViewData["CarreraId"] = new SelectList(_context.Carreras, "CarreraId", "Nombre", estudiante.CarreraId);
            return View(estudiante);
        }

        // GET: Estudiantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiantes
                .Include(e => e.Carrera)
                .FirstOrDefaultAsync(m => m.PersonaId == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // POST: Estudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudianteExists(int id)
        {
            return _context.Estudiantes.Any(e => e.PersonaId == id);
        }

        public IActionResult Consultar()
        {
            ViewData["Semestres"] = _context.Semestres.OrderByDescending(p => p.FechaInicio).ToList();
            var tiposDocumentos = new[]
            {
                new { TipoDocumento = 0, DataValueField = "Planeacion proyecto empresarial"},
                new { TipoDocumento = 1, DataValueField = "Plan marco de formacion"},
                new { TipoDocumento = 2, DataValueField = "Plan marco de rotacion"},
                new { TipoDocumento = 3, DataValueField = "Informe semanal"},
                new { TipoDocumento = 4, DataValueField = "Reporte nota fase practica"}
            };
            ViewData["TipoDocumento"] = new SelectList(tiposDocumentos, "TipoDocumento", "DataValueField");
            return View();
        }

        public IActionResult ConsultarDetalles(int personaId, int[] semestres, int[] documentos)
        {
            var estudiante = _context.Estudiantes.Find(personaId);
            if (estudiante == null)
            {
                return NotFound();
            }
            //TODO - CONSULTAR LOS DATOS DE LOS SEMESTRES QUE ESTAN INDICADOS EN LA
            ////VARIARIBLE SEMESTRE Y LOS DOCUMENTOS QUE CORRESPONDAN AL ESTUDIANTE
            ViewData["Estudiante"] = estudiante.DataValueField;
            var registros = new List<DocumentoConsulta>();
            for (int i = 0; i < semestres.Length; i++)
            {
                var semestre = _context.Semestres.Find(semestres[i]);
                for (int j = 0; j < documentos.Length; j++)
                {
                    var documento = string.Empty;
                    var controllerAction = string.Empty;
                    if(documentos[j] == 0)
                    {
                        var url = Url.Action("ReportePorEstudiante", "Proyectos");
                        documento = "Planeacion proyecto empresarial";
                        controllerAction = url;
                    }
                    else if (documentos[j] == 1)
                    {
                        documento = "Plan marco de formacion";
                        controllerAction = "";
                    }
                    else if (documentos[j] == 2)
                    {
                        documento = "Plan marco de rotacion";
                        controllerAction = "debo construir la url del controlador";
                    }
                    else if (documentos[j] == 3)
                    {
                        documento = "Informe semanal";
                        controllerAction = "debo construir la url del controlador";
                    }
                    else if (documentos[j] == 4)
                    {
                        documento = "Reporte nota fase practica";
                        controllerAction = "debo construir la url del controlador";
                    }
                    registros.Add(new DocumentoConsulta
                    {
                        Semestre = semestre.DataValueField,
                        Documento = documento,
                        Url = $"{controllerAction}?semestreId={semestres[i]}&personaId={personaId}"
                    });
                }
            }
            return View(registros);
        }
    }
}
