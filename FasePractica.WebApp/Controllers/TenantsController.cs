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
using FasePractica.Services;
using FasePractica.WebApp.Models;

namespace FasePractica.WebApp.Controllers
{
    [Authorize]
    public class TenantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TenantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var institutos = from u in _context.Usuarios
                             join upi in _context.UsuarioPorInstitutos on u.UsuarioId equals upi.UsuarioId
                             join i in _context.Institutos on upi.InstitutoId equals i.InstitutoId
                             where u.Correo == this.User.Identity.Name
                             select new InstitutoViewModel
                             {
                                 InstitutoId = i.InstitutoId,
                                 Nombre = i.Nombre,
                                 Actual = i.TenantName == TenantStorage.Instance().Tenant
                             };

            return View(await institutos.ToListAsync());
        }


        // POST: Tenants/Update/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int institutoId)
        {
            if (institutoId <= 0)
            {
                return NotFound();
            }

            var instituto = (from u in _context.Usuarios
                             join upi in _context.UsuarioPorInstitutos on u.UsuarioId equals upi.UsuarioId
                             join i in _context.Institutos on upi.InstitutoId equals i.InstitutoId
                             where u.Correo == this.User.Identity.Name
                             where i.InstitutoId == institutoId
                             select i).FirstOrDefault();
            if (instituto == null)
            {
                return NotFound();
            }
            TenantStorage.Remove();
            TenantStorage.Instance(instituto.TenantName, instituto.Nombre);
            // TODO: Gorky, recrear el modelbuilder pues cambió el modelo, ¿si no hay cookie tambien? también ver seccion de cookies
            ViewData["CurrentTenant"] = instituto.Nombre;
            return View();
        }
    }
}
