﻿using System;
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
    public class ConversacionesController : Controller
    {
        private readonly TenantDbContext _context;

        public ConversacionesController(TenantDbContext context)
        {
            _context = context;
        }

        // GET: Conversaciones
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Conversaciones.Include(c => c.Empresa);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Conversaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversacion = await _context.Conversaciones
                .Include(c => c.Empresa)
                .FirstOrDefaultAsync(m => m.ConversacionId == id);
            if (conversacion == null)
            {
                return NotFound();
            }

            return View(conversacion);
        }

        // GET: Conversaciones/Create
        public IActionResult Create()
        {
            var estadosConversacion = new[]
            {
                new { Estado = (int)EstadoConversacion.PorRealizar, DataValueField = Enum.GetName(typeof(EstadoConversacion), EstadoConversacion.PorRealizar) },
                new { Estado = (int)EstadoConversacion.EnProceso, DataValueField = Enum.GetName(typeof(EstadoConversacion), EstadoConversacion.EnProceso) },
                new { Estado = (int)EstadoConversacion.Listo, DataValueField = Enum.GetName(typeof(EstadoConversacion), EstadoConversacion.Listo) }
            };
            ViewData["EstadoConversacion"] = new SelectList(estadosConversacion, "Estado", "DataValueField");
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "Alias");
            return View();
        }

        // POST: Conversaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConversacionId,RealizadoEl,EmpresaId,Observaciones,Estado")] Conversacion conversacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conversacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var estadosConversacion = new[]
            {
                new { Estado = (int)EstadoConversacion.PorRealizar, DataValueField = Enum.GetName(typeof(EstadoConversacion), EstadoConversacion.PorRealizar) },
                new { Estado = (int)EstadoConversacion.EnProceso, DataValueField = Enum.GetName(typeof(EstadoConversacion), EstadoConversacion.EnProceso) },
                new { Estado = (int)EstadoConversacion.Listo, DataValueField = Enum.GetName(typeof(EstadoConversacion), EstadoConversacion.Listo) }
            };
            ViewData["EstadoConversacion"] = new SelectList(estadosConversacion, "Estado", "DataValueField", conversacion.Estado);
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "Alias", conversacion.EmpresaId);
            return View(conversacion);
        }

        // GET: Conversaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversacion = await _context.Conversaciones.FindAsync(id);
            if (conversacion == null)
            {
                return NotFound();
            }
            var estadosConversacion = new[]
            {
                new { Estado = (int)EstadoConversacion.PorRealizar, DataValueField = Enum.GetName(typeof(EstadoConversacion), EstadoConversacion.PorRealizar) },
                new { Estado = (int)EstadoConversacion.EnProceso, DataValueField = Enum.GetName(typeof(EstadoConversacion), EstadoConversacion.EnProceso) },
                new { Estado = (int)EstadoConversacion.Listo, DataValueField = Enum.GetName(typeof(EstadoConversacion), EstadoConversacion.Listo) }
            };
            ViewData["EstadoConversacion"] = new SelectList(estadosConversacion, "Estado", "DataValueField", conversacion.Estado);
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "Alias", conversacion.EmpresaId);
            return View(conversacion);
        }

        // POST: Conversaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConversacionId,RealizadoEl,EmpresaId,Observaciones,Estado")] Conversacion conversacion)
        {
            if (id != conversacion.ConversacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conversacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConversacionExists(conversacion.ConversacionId))
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
            var estadosConversacion = new[]
            {
                new { Estado = (int)EstadoConversacion.PorRealizar, DataValueField = Enum.GetName(typeof(EstadoConversacion), EstadoConversacion.PorRealizar) },
                new { Estado = (int)EstadoConversacion.EnProceso, DataValueField = Enum.GetName(typeof(EstadoConversacion), EstadoConversacion.EnProceso) },
                new { Estado = (int)EstadoConversacion.Listo, DataValueField = Enum.GetName(typeof(EstadoConversacion), EstadoConversacion.Listo) }
            };
            ViewData["EstadoConversacion"] = new SelectList(estadosConversacion, "Estado", "DataValueField", conversacion.Estado);
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "Alias", conversacion.EmpresaId);
            return View(conversacion);
        }

        // GET: Conversaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversacion = await _context.Conversaciones
                .Include(c => c.Empresa)
                .FirstOrDefaultAsync(m => m.ConversacionId == id);
            if (conversacion == null)
            {
                return NotFound();
            }

            return View(conversacion);
        }

        // POST: Conversaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conversacion = await _context.Conversaciones.FindAsync(id);
            _context.Conversaciones.Remove(conversacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConversacionExists(int id)
        {
            return _context.Conversaciones.Any(e => e.ConversacionId == id);
        }
    }
}
