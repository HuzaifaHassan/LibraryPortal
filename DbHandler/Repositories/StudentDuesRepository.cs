using DbHandler.Data;
using DbHandler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHandler.Repositories
{
    public class StudentDuesRepository: IStudentDuesRepository
    {
        private readonly ApplicationDbContext _ctx;
        public StudentDuesRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public void AddLibraryDue(StudentDues model)
        {
            _ctx.TStudentDue.Add(model);
        }
        public StudentDues GetByStudentid(string id)
        {
            var resp = _ctx.TStudentDue.Where(x => x.Id == id).FirstOrDefault();
            return resp;
        }
        public StudentDues GetByStudentIdAndRef(string id, string _ref)
        {
            return _ctx.TStudentDue.Where(x => x.Id == id && x.Reference == _ref).FirstOrDefault();
        }
        public void UpdateStudentLibraryDues(StudentDues model)
        {
            _ctx.TStudentDue.Update(model);
        }
        public bool Save()
        {
            return _ctx.SaveChanges() >= 0;
        }

    }
}
