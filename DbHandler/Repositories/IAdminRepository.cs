using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbHandler.Data;
using DbHandler.Model;
using System.Threading.Tasks;

namespace DbHandler.Repositories
{
    public interface IAdminRepository
    {
       
        public Admin GetByname(string name);
        public Admin GetByNameandPassword(string name, string password);

    }
}
