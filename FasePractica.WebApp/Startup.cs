using FasePractica.Data;
using FasePractica.Services;
using FasePractica.WebApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FasePractica.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Set the active provider via configuration
            var provider = Configuration.GetValue("Provider", "SqlServer");

            services.AddDbContext<ApplicationDbContext>(
                    options => _ = provider switch
                    {
                        "Npgsql" => options.UseNpgsql(
                            Configuration.GetConnectionString("NpgsqlSeguridadConnection"),
                            x => x.MigrationsAssembly("FasePractica.Data")),

                        "SqlServer" => options.UseSqlServer(
                            Configuration.GetConnectionString("SqlServerSeguridadConnection"),
                            x => x.MigrationsAssembly("SqlServerMigrations")),

                        _ => throw new Exception($"Unsupported provider: {provider}")
                    });

            services.AddDbContext<TenantDbContext>(
                    options => _ = provider switch
                    {
                        "Npgsql" => options.UseNpgsql(
                            Configuration.GetConnectionString("NpgsqlNegocioConnection"),
                            x => x.MigrationsAssembly("FasePractica.Data")),

                        "SqlServer" => options.UseSqlServer(
                            Configuration.GetConnectionString("SqlServerNegocioConnection"),
                            x => x.MigrationsAssembly("SqlServerMigrations")),

                        _ => throw new Exception($"Unsupported provider: {provider}")
                    });

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<DataProtectionTokenProviderOptions>(o =>
            o.TokenLifespan = TimeSpan.FromHours(3));

            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.AddControllersWithViews();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    IConfigurationSection googleAuthNSection =
                        Configuration.GetSection("Authentication:Google");

                    options.ClientId = googleAuthNSection["ClientId"];
                    options.ClientSecret = googleAuthNSection["ClientSecret"];
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    if (context.Request.Cookies.TryGetValue("Tenant", out string tenant))
                    {
                        var partes = tenant.Split('-');
                        var nombreInstituto = string.Empty;
                        if (partes.Length > 1)
                            nombreInstituto = partes[1];
                        _ = TenantStorage.Instance(partes[0], nombreInstituto);
                    }
                }
                else
                {
                    if (context.Request.Cookies.ContainsKey("Tenant"))
                    {
                        context.Response.Cookies.Delete("Tenant");
                    }
                    TenantStorage.Remove();
                }

                await next.Invoke();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
