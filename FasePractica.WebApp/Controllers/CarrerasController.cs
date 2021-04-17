using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FasePractica.Data;
using FasePractica.Data.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace FasePractica.WebApp.Controllers
{
    public class CarrerasController : Controller
    {
        private readonly TenantDbContext _context;
        private IConfiguration _configuration;
        private readonly Dictionary<string, List<byte[]>> _fileSignature =
            new Dictionary<string, List<byte[]>>
        {
            { ".png", new List<byte[]>
                {
                    new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }
                }
            },
        };


        public CarrerasController(TenantDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Carreras
        public async Task<IActionResult> Index()
        {
            return View(await _context.Carreras.ToListAsync());
        }

        // GET: Carreras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrera = await _context.Carreras
                .FindAsync(id);
            if (carrera == null)
            {
                return NotFound();
            }

            return View(carrera);
        }

        // GET: Carreras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carreras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarreraId,Nombre")] Carrera carrera, IFormFile logoFile)
        {
            if (ModelState.IsValid)
            {
                if (logoFile.FileName.Substring(logoFile.FileName.LastIndexOf('.')).ToLower() != ".png")
                {
                    return BadRequest("Solo se aceptan archivos .png");
                }
                using (var memoryStream = new MemoryStream())
                {

                    logoFile.CopyTo(memoryStream);
                    if (memoryStream.Length > _configuration.GetValue<int>("FileSizeLimit"))
                    {
                        return BadRequest("Tamano maximo logo 200KB");
                    }
                    var buffer = memoryStream.ToArray();
 
                    using (var reader = new BinaryReader(memoryStream))
                    {
                        var signatures = _fileSignature[".png"];
                        memoryStream.Position = 0;

                        var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

                        var isValidSignature = signatures.Any(signature =>
                            headerBytes.Take(signature.Length).SequenceEqual(signature));
                        if (!isValidSignature)
                        {
                            return BadRequest("La firma no coincide con la extension del archivo.");
                        }
                    }                  
                    carrera.Logo = Convert.ToBase64String(buffer);
                }

                _context.Add(carrera);
 
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carrera);
        }

        // GET: Carreras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrera = await _context.Carreras.FindAsync(id);
            if (carrera == null)
            {
                return NotFound();
            }
            return View(carrera);
        }

        // POST: Carreras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarreraId,Nombre,Logo")] Carrera carrera, IFormFile logoFile)
        {
            if (id != carrera.CarreraId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarreraExists(carrera.CarreraId))
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
            return View(carrera);
        }

        // GET: Carreras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrera = await _context.Carreras
                .FirstOrDefaultAsync(m => m.CarreraId == id);
            if (carrera == null)
            {
                return NotFound();
            }

            return View(carrera);
        }

        // POST: Carreras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carrera = await _context.Carreras.FindAsync(id);
            _context.Carreras.Remove(carrera);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarreraExists(int id)
        {
            return _context.Carreras.Any(e => e.CarreraId == id);
        }
    }
}
