using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbHandler.Data;
using DbHandler.Model;
using System.Threading.Tasks;

namespace DbHandler.Repositories
{
    public class AdminRepository: IAdminRepository
    {
        private readonly ApplicationDbContext _ctx;
        public AdminRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public Admin GetByname(string name)
        {
            var resp = _ctx.TAdmin.Where(x => x.name == name).FirstOrDefault();
            return resp;
        }
        public Admin GetByNameandPassword(string name, string password)
        {
            var resp = _ctx.TAdmin.Where(x => x.name == name && x.password == password).FirstOrDefault();
            return resp;

        }
    }
}
