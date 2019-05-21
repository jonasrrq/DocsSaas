using Docs.Saas.Model.Auth;
using Docs.Saas.Model.Domaim;
using Docs.Saas.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docs.Saas.Service
{
    public interface IPrimiumService
    {
        Task<List<Primium>> GetList(string userName);

        Task<List<PrimiumUser>> GetMisList(string userName);
    }

    public class PrimiumService : IPrimiumService
    {

        private readonly ApplicationDbContext _context;

        public PrimiumService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Primium>> GetList(string userName)
        {
            var list = new List<Primium>();

            try
            {
                ApplicationUser _user = await GetUser(userName);
                if (_user.Licencia.Descripcion != "Administrador")
                    list = await _context.Primiums.Include(x => x.Licencia).Where(x => x.Licencia.Equals(_user.Licencia) || x.Licencia.Descripcion.Equals("Usuario")).ToListAsync();
                else
                    list = await _context.Primiums.Include(x => x.Licencia).ToListAsync();

            }
            catch (Exception)
            {

            }

            return list;
        }

        private async Task<ApplicationUser> GetUser(string userName)
        {
            return await _context.Users.Include(x => x.Licencia).Where(x => x.UserName.Equals(userName)).FirstOrDefaultAsync();
        }

        public async Task<List<PrimiumUser>> GetMisList(string userName)
        {
            var list = new List<PrimiumUser>();

            try
            {
                ApplicationUser _user = await GetUser(userName);
                var _list  = _context.PrimiumUsers.Include(x => x.Primium).Where(x => x.User.Id.Equals(_user.Id)).AsQueryable(); 
              
                if (_user.Licencia.Descripcion == "Usuario")  //Cammmbiar ahi que validar si es un usuario final y es Empleado
                {
                    var _userRel = await _context.UserAplicationUsers.Include(x => x.UserRevEmp).ThenInclude(x=>x.Licencia).Where(x => x.UserId.Equals(_user.Id)).FirstOrDefaultAsync();
                    if(_userRel.UserRevEmp.Licencia.Descripcion == "Empresa")
                    _list = _context.PrimiumUsers.Include(x => x.Primium).Where(x => x.User.Id.Equals(_user.Id) || x.User.Id.Equals(_userRel.RevEmpId)).AsQueryable();
                }    
                list = await _list.ToListAsync();  
            }
            catch (Exception ex)
            {

            }

            return list;
        }


    }
}
