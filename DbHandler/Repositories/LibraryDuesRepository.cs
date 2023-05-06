using DbHandler.Data;
using DbHandler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHandler.Repositories
{
    public class LibraryDuesRepository: ILibraryDuesRepository
    {
        private readonly ApplicationDbContext _ctx;
        public LibraryDuesRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public void AddLibraryDue(LibraryDues model)
        {
            _ctx.TLibrary.Add(model);
        }
        public LibraryDues GetByStudentid(string id)
        {
            var resp = _ctx.TLibrary.Where(x => x.id == id).FirstOrDefault();
            return resp;
        }
        public LibraryDues GetByStudentidOrCstId( string cstid)
        {
            var resp = _ctx.TLibrary.Where(x =>  x.cstid == cstid).FirstOrDefault();
            return resp;
        }
        public LibraryDues GetByStudentIdAndRef(string Cid,string _ref)
        {
            return _ctx.TLibrary.Where(x => x.Reference == _ref && x.cstid==Cid).FirstOrDefault();
        }
        public void UpdateLibraryDues(LibraryDues model)
        {
            _ctx.TLibrary.Update(model);
        }
        public bool Save()
        {
            return _ctx.SaveChanges() >= 0;
        }
    }
}
