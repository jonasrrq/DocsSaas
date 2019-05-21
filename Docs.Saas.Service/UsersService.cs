using Docs.Saas.Model.Auth;
using Docs.Saas.Model.Custom;
using Docs.Saas.Persistence;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Saas.Service
{

    public interface IUsersService
    {
        Task<List<string>> Login(LoginView login);
        void Logout();

    }
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public UsersService(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<List<string>> Login(LoginView login)
        {
            List<string> _resul = new List<string>();
            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user != null)
            {
                if (user.Baja)
                {
                    if (await _userManager.CheckPasswordAsync(user, login.Password))
                    {
                        string _msg = await GetBloqueoUser(user);
                        if (_msg != string.Empty) _resul.Add(_msg);
                        else
                        {
                            await _signInManager.SignInAsync(user, login.RememberMe);
                            // Cuando el token se verifica correctamente, borre el recuento fallido de acceso utilizado para el bloqueo.
                            await _userManager.ResetAccessFailedCountAsync(user);
                        }
                    }
                    else
                    {
                        string _msg = await GetBloqueoUser(user);
                        if (_msg != string.Empty) _resul.Add(_msg);
                        else
                        {
                            await _userManager.AccessFailedAsync(user);
                            int accessFailedCount = await _userManager.GetAccessFailedCountAsync(user);
                            int MaxFailedAccess = _signInManager.Options.Lockout.MaxFailedAccessAttempts;
                            _resul.Add($"Credenciales no válidas. Tiene {MaxFailedAccess - accessFailedCount} más intentos antes de que su cuenta se bloquee.");
                        }
                    }
                }
                else _resul.Add("Credenciales no válidas!, comuniquese con el administrador del sistema!");
            }
            else _resul.Add("Credenciales no válidas!");
            return _resul;
        }

        public async void Logout()
        {
            await _signInManager.SignOutAsync();
        }

        private async Task<string> GetBloqueoUser(ApplicationUser user)
        {
            string _result = string.Empty;
            if (await _userManager.IsLockedOutAsync(user))
            {
                var Fin = await _userManager.GetLockoutEndDateAsync(user);
                var Actual = DateTime.Now;
                TimeSpan ts = Fin.Value - Actual;
                _result = $@"Su cuenta ha sido bloqueada, debido a múltiples intentos fallidos de inicio de sesión. {Environment.NewLine}
                            Intentelo de nuevo en {ts.Minutes} minutos.";
            }
            return _result;
        }
    }
}
