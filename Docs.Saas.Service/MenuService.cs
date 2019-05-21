using Docs.Saas.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Saas.Service
{
    public interface IMenuService
    {
      

    }
    public class MenuService  : IMenuService
    {
        private readonly ApplicationDbContext _context;

        public MenuService(ApplicationDbContext context)
        {
            _context = context;           
        }

     

    }
}
