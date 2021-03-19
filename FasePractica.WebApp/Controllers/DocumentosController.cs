using System;
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
    public class DocumentosController : Controller
    {
        private readonly TenantDbContext _context;
        private IConfiguration _configuration;

        public DocumentosController(TenantDbContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Documentos
        public async Task<IActionResult> Index(int? pagina)
        {
            //var applicationDbContext = _context.Documentos.Include(d => d.Empresa);
            
            var tamanoPagina = _configuration.GetValue<int>("TamanoPagina");
            if(pagina==null || pagina<=0)
            {
                pagina=1;
            }
            var skip = ((int)pagina-1)*tamanoPagina;
            var documentos = await _context.Documentos.Include(d => d.Empresa).Skip(skip).Take(tamanoPagina).ToListAsync();
            var totalDocumentos = _context.Documentos.Count();
            int totalPaginas = totalDocumentos/tamanoPagina;
            if(totalDocumentos%tamanoPagina!=0)
            {
                totalPaginas+=1;
            }
            ViewData["PaginaActual"] = pagina;
            ViewData["TotalPaginas"] = totalPaginas;
            return View(documentos);

            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: Documentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documento = await _context.Documentos
                .Include(d => d.Empresa)
                .FirstOrDefaultAsync(m => m.DocumentoId == id);
            if (documento == null)
            {
                return NotFound();
            }

            return View(documento);
        }

        // GET: Documentos/Create
        public IActionResult Create()
        {
            var tiposDocumento = new[]
            {
                new { TipoDocumento = (int)TipoDocumento.Acta, DataValueField = Enum.GetName(typeof(TipoDocumento), TipoDocumento.Acta) },
                new { TipoDocumento = (int)TipoDocumento.Convenio, DataValueField = Enum.GetName(typeof(TipoDocumento), TipoDocumento.Convenio)},
                new { TipoDocumento = (int)TipoDocumento.Extension, DataValueField = Enum.GetName(typeof(TipoDocumento), TipoDocumento.Extension)},
                new { TipoDocumento = (int)TipoDocumento.InformeTecnicoViabilidad, DataValueField = Enum.GetName(typeof(TipoDocumento), TipoDocumento.InformeTecnicoViabilidad)},
                new { TipoDocumento = (int)TipoDocumento.Memorando, DataValueField = Enum.GetName(typeof(TipoDocumento), TipoDocumento.Memorando)},
                new { TipoDocumento = (int)TipoDocumento.Otro, DataValueField = Enum.GetName(typeof(TipoDocumento), TipoDocumento.Otro)}
            };
            ViewData["TipoDocumento"] = new SelectList(tiposDocumento, "TipoDocumento", "DataValueField");
            var estado = new[]
            {
                new{ Estado = (int)Estado.PorFirmar, DataValueField = Enum.GetName(typeof(Estado), Estado.PorFirmar)},
                new{ Estado = (int)Estado.Firmado, DataValueField = Enum.GetName(typeof(Estado), Estado.Firmado)},
                new{ Estado = (int)Estado.Caducado, DataValueField = Enum.GetName(typeof(Estado), Estado.Caducado)}

            };
            ViewData["Estado"] = new SelectList(estado, "Estado", "DataValueField");
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "DataValueField");
            return View();
        }

        // POST: Documentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DocumentoId,Codigo,FirmadoEl,FechaFinal,Descripcion,AlmacenadoEn,Estado,Tipo,EmpresaId")] Documento documento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(documento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var tiposDocumento = new[]
            {
                new { TipoDocumento = (int)TipoDocumento.Acta, DataValueField = Enum.GetName(typeof(TipoDocumento), TipoDocumento.Acta) },
                new { TipoDocumento = (int)TipoDocumento.Convenio, DataValueField = Enum.GetName(typeof(TipoDocumento), TipoDocumento.Convenio)},
                new { TipoDocumento = (int)TipoDocumento.Extension, DataValueField = Enum.GetName(typeof(TipoDocumento), TipoDocumento.Extension)},
                new { TipoDocumento = (int)TipoDocumento.InformeTecnicoViabilidad, DataValueField = Enum.GetName(typeof(TipoDocumento), TipoDocumento.InformeTecnicoViabilidad)},
                new { TipoDocumento = (int)TipoDocumento.Memorando, DataValueField = Enum.GetName(typeof(TipoDocumento), TipoDocumento.Memorando)},
                new { TipoDocumento = (int)TipoDocumento.Otro, DataValueField = Enum.GetName(typeof(TipoDocumento), TipoDocumento.Otro)}
            };
            ViewData["TipoDocumento"] = new SelectList(tiposDocumento, "TipoDocumento", "DataValueField", documento.Tipo);
            var estado = new[]
            {
                new{ Estado = (int)Estado.PorFirmar, DataValueField = Enum.GetName(typeof(Estado), Estado.PorFirmar)},
                new{ Estado = (int)Estado.Firmado, DataValueField = Enum.GetName(typeof(Estado), Estado.Firmado)},
                new{ Estado = (int)Estado.Caducado, DataValueField = Enum.GetName(typeof(Estado), Estado.Caducado)}

            };
            ViewData["Estado"] = new SelectList(estado, "Estado", "DataValueField", documento.Estado);
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "DataValueField", documento.EmpresaId);
            return View(documento);
        }

        // GET: Documentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documento = await _context.Documentos.FindAsync(id);
            if (documento == null)
            {
                return NotFound();
            }
            var tiposDocumento = new[]
            {
                new { TipoDocumento = (int)TipoDocumento.Acta, DataValueField = Enum.GetName(typeof(TipoDocumento), TipoDocumento.Acta) },
                new { TipoDocumento = (int)TipoDocumento.Convenio, DataValueField = Enum.GetName(typeof(TipoDocumento), TipoDocumento.Convenio)},
                new { TipoDocumento = (int)TipoDocumento.Extension, DataValueField = Enum.GetName(typeof(TipoDocumento), TipoDocumento.Extension)},
                new { TipoDocumento = (int)TipoDocumento.InformeTecnicoViabilidad, DataValueField = Enum.GetName(typeof(TipoDocumento), TipoDocumento.InformeTecnicoViabilidad)},
                new { TipoDocumento = (int)TipoDocumento.Memorando, DataValueField = Enum.GetName(typeof(TipoDocumento), TipoDocumento.Memorando)},
                new { TipoDocumento = (int)TipoDocumento.Otro, DataValueField = Enum.GetName(typeof(TipoDocumento), TipoDocumento.Otro)}
            };
            ViewData["TipoDocumento"] = new SelectList(tiposDocumento, "TipoDocumento", "DataValueField", documento.Tipo);
            var estado = new[]
            {
                new{ Estado = (int)Estado.PorFirmar, DataValueField = Enum.GetName(typeof(Estado), Estado.PorFirmar)},
                new{ Estado = (int)Estado.Firmado, DataValueField = Enum.GetName(typeof(Estado), Estado.Firmado)},
                new{ Estado = (int)Estado.Caducado, DataValueField = Enum.GetName(typeof(Estado), Estado.Caducado)}

            };
            ViewData["Estado"] = new SelectList(estado, "Estado", "DataValueField", documento.Estado);
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "DataValueField", documento.EmpresaId);
            return View(documento);
        }

        // POST: Documentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DocumentoId,Codigo,FirmadoEl,FechaFinal,Descripcion,AlmacenadoEn,Estado,Tipo,EmpresaId")] Documento documento)
        {
            if (id != documento.DocumentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentoExists(documento.DocumentoId))
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
            var tiposDocumento = new[]
            {
                new { TipoDocumento = (int)TipoDocumento.Acta, DataValueField = Enum.GetName(typeof(TipoDocumento), TipoDocumento.Acta) },
                new { TipoDocumento = (int)TipoDocumento.Convenio, DataValueField = Enum.GetName(typeof(TipoDocumento), TipoDocumento.Convenio)},
                new { TipoDocumento = (int)TipoDocumento.Extension, DataValueField = Enum.GetName(typeof(TipoDocumento), TipoDocumento.Extension)},
                new { TipoDocumento = (int)TipoDocumento.InformeTecnicoViabilidad, DataValueField = Enum.GetName(typeof(TipoDocumento), TipoDocumento.InformeTecnicoViabilidad)},
                new { TipoDocumento = (int)TipoDocumento.Memorando, DataValueField = Enum.GetName(typeof(TipoDocumento), TipoDocumento.Memorando)},
                new { TipoDocumento = (int)TipoDocumento.Otro, DataValueField = Enum.GetName(typeof(TipoDocumento), TipoDocumento.Otro)}
            };
            ViewData["TipoDocumento"] = new SelectList(tiposDocumento, "TipoDocumento", "DataValueField", documento.Tipo);
            var estado = new[]
            {
                new{ Estado = (int)Estado.PorFirmar, DataValueField = Enum.GetName(typeof(Estado), Estado.PorFirmar)},
                new{ Estado = (int)Estado.Firmado, DataValueField = Enum.GetName(typeof(Estado), Estado.Firmado)},
                new{ Estado = (int)Estado.Caducado, DataValueField = Enum.GetName(typeof(Estado), Estado.Caducado)}

            };
            ViewData["Estado"] = new SelectList(estado, "Estado", "DataValueField", documento.Estado);
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "DataValueField", documento.EmpresaId);
            return View(documento);
        }

        // GET: Documentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documento = await _context.Documentos
                .Include(d => d.Empresa)
                .FirstOrDefaultAsync(m => m.DocumentoId == id);
            if (documento == null)
            {
                return NotFound();
            }

            return View(documento);
        }

        // POST: Documentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var documento = await _context.Documentos.FindAsync(id);
            _context.Documentos.Remove(documento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentoExists(int id)
        {
            return _context.Documentos.Any(e => e.DocumentoId == id);
        }
    }
}
