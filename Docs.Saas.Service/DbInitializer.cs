using Docs.Saas.Model.Auth;
using Docs.Saas.Model.Domaim;
using Docs.Saas.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Docs.Saas.Service
{
    public interface IDbInitializer
    {
        void Initialize();
    }
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceProvider _serviceProvider;

        public DbInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async void Initialize()
        {
            using (var serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                //create database schema if none exists
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                var _Licencia = context.Licencias.ToList();
                if (_Licencia.Count == 0)
                {
                    context.Add(new Licencia { Descripcion = "Administrador" });
                    context.Add(new Licencia { Descripcion = "Revendedor" });
                    context.Add(new Licencia { Descripcion = "Empresa" });
                    context.Add(new Licencia { Descripcion = "Usuario" });
                }
                await context.SaveChangesAsync();

                var _Lic = context.Licencias.ToList();

                var _Primiums = context.Primiums.ToList();
                var Pri1 = new Primium { Descripcion = "Temas Personalizados" };
                var Pri5 = new Primium { Descripcion = "Compartir" };
                if (_Primiums.Count == 0)
                {

                   
                    Pri1.Licencia = _Lic.Where(x => x.Descripcion.Equals("Usuario")).FirstOrDefault<Licencia>();
                    context.Add(Pri1);

                    var Pri2 = new Primium { Descripcion = "Configuración de Empleados" };
                    Pri2.Licencia = _Lic.Where(x => x.Descripcion.Equals("Empresa")).FirstOrDefault<Licencia>();
                    context.Add(Pri2);

                    var Pri3 = new Primium { Descripcion = "Configuración de Clientes" };
                    Pri3.Licencia = _Lic.Where(x => x.Descripcion.Equals("Revendedor")).FirstOrDefault<Licencia>();
                    context.Add(Pri3);

                    var Pri4 = new Primium { Descripcion = "Personalizar Logotipo" };
                    Pri4.Licencia = _Lic.Where(x => x.Descripcion.Equals("Revendedor")).FirstOrDefault<Licencia>();
                    context.Add(Pri4);

                  
                    Pri5.Licencia = _Lic.Where(x => x.Descripcion.Equals("Usuario")).FirstOrDefault<Licencia>();
                    context.Add(Pri5);

                    var Pri6 = new Primium { Descripcion = "Primium Usuarilo final 0001" };
                    Pri6.Licencia = _Lic.Where(x => x.Descripcion.Equals("Usuario")).FirstOrDefault<Licencia>();
                    context.Add(Pri6);

                    var Pri7 = new Primium { Descripcion = "Primium Usuarilo final 0002" };
                    Pri7.Licencia = _Lic.Where(x => x.Descripcion.Equals("Usuario")).FirstOrDefault<Licencia>();
                    context.Add(Pri7);

                    var Pri8 = new Primium { Descripcion = "Primium Usuarilo final 0003" };
                    Pri8.Licencia = _Lic.Where(x => x.Descripcion.Equals("Usuario")).FirstOrDefault<Licencia>();
                    context.Add(Pri8);
                }


                var _userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                var userAdmin = new ApplicationUser()
                {
                    Nombres = "Jonás Requena",
                    Email = "jonasrq@hotmail.com",
                    UserName = "jonasrq@hotmail.com",
                    EmailConfirmed = true,
                    Baja = true,
                    Licencia = _Lic.Where(x => x.Descripcion.Equals("Administrador")).FirstOrDefault<Licencia>()
                };
                string password = "123456";
                await _userManager.CreateAsync(userAdmin, password);                  

               
                var userEmpr = new ApplicationUser()
                {
                    Nombres = "La comañia ca",
                    Email = "compnya@hotmail.com",
                    UserName = "companya@hotmail.com",
                    EmailConfirmed = true,
                    Baja = true,
                    Licencia = _Lic.Where(x => x.Descripcion.Equals("Empresa")).FirstOrDefault<Licencia>()
                };
                var succesEmpr = await _userManager.CreateAsync(userEmpr, password);
                if (succesEmpr.Succeeded)
                {
                    context.Add(new LicenciaUser
                    {
                        FechaInicio = DateTime.Now.Date,
                        FechaFin = DateTime.Now.AddYears(1).Date,
                        Keys = 10,
                        User = userEmpr
                    });
                    context.Add(new PrimiumUser
                    {
                        FechaInicio = DateTime.Now.Date,
                        FechaFin = DateTime.Now.AddYears(1).Date,
                        User = userEmpr,
                        Primium = Pri5
                    });
                }

                var userRev = new ApplicationUser()
                {
                    Nombres = "El Revendedor sl",
                    Email = "revendedor@hotmail.com",
                    UserName = "revendedor@hotmail.com",
                    EmailConfirmed = true,
                    Baja = true,
                    Licencia = _Lic.Where(x => x.Descripcion.Equals("Revendedor")).FirstOrDefault<Licencia>(),
                    
                };
                var succesRev = await _userManager.CreateAsync(userRev, password);

                if (succesRev.Succeeded)
                {
                    context.Add(new LicenciaUser
                    {
                        FechaInicio = DateTime.Now.Date,
                        FechaFin = DateTime.Now.AddYears(1).Date,
                        Keys = 50,
                        User = userRev
                    });
                    context.Add(new PrimiumUser
                    {
                        FechaInicio = DateTime.Now.Date,
                        FechaFin = DateTime.Now.AddYears(1).Date,
                        User = userRev,
                        Primium = Pri1
                    });
                }


                var _rolUser = _Lic.Where(x => x.Descripcion.Equals("Usuario")).FirstOrDefault<Licencia>();
                for (int i = 0; i < 10; i++)
                {
                    var user = new ApplicationUser()
                    {
                        Nombres = $"User 00{i}",
                        Email = $"User00{i}@hotmail.com",
                        UserName = $"User00{i}@hotmail.com",
                        EmailConfirmed = true,
                        Baja = true,
                        Licencia = _rolUser
                    };
                    var success = await _userManager.CreateAsync(user, password);

                    if (success.Succeeded)
                    {
                        if ((i % 2) == 0)
                        {
                            context.Add(new UserApplicationUser { UserRevEmp = userEmpr, UserFinal = user});
                        }
                        else
                        {
                            context.Add(new UserApplicationUser { UserRevEmp = userRev, UserFinal = user });
                        }
                    }
                }
                await context.SaveChangesAsync();  

            }
        }
    }
}
