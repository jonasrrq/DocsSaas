using Docs.Saas.Model.Auth;
using Docs.Saas.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Docs.Saas.Service
{
    public class Startup
    {
        public static void ConfigureStar(ref IServiceCollection services, IConfiguration Configuration)
        {
            //configuro conexion            
            //services.AddDbContext<ApplicationDbContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            //        );

            //Cambio la conexion por la Conexione de PostgreSQL
            services.AddEntityFrameworkNpgsql().AddDbContext<PersistencePostgreSQL.ApplicationDbContext>(options =>
                  options.UseNpgsql(Configuration.GetConnectionString("ConnectionPostgreSQL"))
                  );

            //Configuro Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders()
                    .AddErrorDescriber<MyErrorDescribe>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;                      //Requiere un número entre 0-9 en la contraseña.
                options.Password.RequiredLength = 6;                            //La longitud mínima de la contraseña.
                options.Password.RequireNonAlphanumeric = false;  //Requiere un carácter que no sean alfanuméricos en la contraseña.
                options.Password.RequireUppercase = false;        //Requiere un carácter en mayúsculas en la contraseña.
                options.Password.RequireLowercase = false;        //Requiere un carácter en minúscula en la contraseña.
                options.Password.RequiredUniqueChars = 2;         //Requiere el número de caracteres distintos de la contraseña.

                // Configuraciones de bloqueo
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Users/Login"; // Si LoginPath no está configurado aquí, ASP.NET Core se configurará por defecto en / Account / Login
                options.LogoutPath = "/Users/Logout"; // Si la Ruta de cierre de sesión no está configurada aquí, ASP.NET Core se configurará por defecto en / Account / Logout
                options.AccessDeniedPath = "/Home"; // Si la ruta de acceso denegada no se establece aquí, ASP.NET Core se configurará por defecto a / Account / AccessDenied
                options.SlidingExpiration = true;
            });



            #region	Singleton


            #endregion

            #region	Transient


            #endregion

            #region Scoped            
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IPrimiumService,PrimiumService>();

            #endregion

        }
    }
}
