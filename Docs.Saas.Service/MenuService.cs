using Docs.Saas.Model.Custom;
using Docs.Saas.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Docs.Saas.Service
{
    public interface IMenuService
    {
        Task<ManuView> Menu(string user);
    }
    public class MenuService : IMenuService
    {
        private readonly ApplicationDbContext _context;

        public MenuService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ManuView> Menu(string userName)
        {
            var _menu = new ManuView();
            try
            {
                if (userName != null)
                {
                    _menu.isLogin = true;
                    var _user = await _context.Users.Include(x => x.Licencia).Where(x => x.UserName.Equals(userName)).FirstOrDefaultAsync();

                    switch (_user.Licencia.Descripcion)
                    {
                        case "Administrador":
                            _menu.CreateUser = "Clientes";
                            break;
                        case "Revendedor":
                            _menu.CreateUser = "Sub-Cliente";
                            break;
                        case "Empresa":
                            _menu.CreateUser = "Empleados";
                            break;
                        case "Usuario":

                            break;
                        default:
                            break;
                    }
                }


            }
            catch (Exception)
            {

            }

            return _menu;
        }

    }
}
