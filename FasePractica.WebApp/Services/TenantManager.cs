using System.Linq;
using System.Security.Claims;
using FasePractica.Data;
using FasePractica.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FasePractica.WebApp.Services
{
    public class TenantManager
    {
        private readonly ApplicationDbContext _dbContext;
        Microsoft.AspNetCore.Http.HttpContext _httpContext;
        public TenantManager(ApplicationDbContext dbContext, Microsoft.AspNetCore.Http.HttpContext httpContext)
        {
            _dbContext = dbContext;
            _httpContext = httpContext;
        }

        public void CreateTenantStorage(string email)
        {
            var usuario = _dbContext.Usuarios.Include(u => u.Institutos).FirstOrDefault(u => u.Correo == email);
            if (usuario != null)
            {
                var usuarioPorInstituto = usuario.Institutos.FirstOrDefault();
                var instituto = _dbContext.Institutos.FirstOrDefault(i => i.InstitutoId == usuarioPorInstituto.InstitutoId);
                if (instituto != null)
                {
                    _httpContext.Response.Cookies.Append("tenant", instituto.TenantName);
                    TenantStorage.Instance(instituto.TenantName);
                }
            }
        }

        public bool EsCorreoPermitido(ExternalLoginInfo info)
        {
            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                var correo = info.Principal.FindFirstValue(ClaimTypes.Email);
                return EsCorreoPermitido(correo);
            }
            return false;
        }

        public bool EsCorreoPermitido(string correo)
        {
            if (string.IsNullOrEmpty(correo))
                return false;

            var usuario = _dbContext.Usuarios.FirstOrDefault(u => u.Correo == correo);
            return usuario != null;
        }
    }
}
