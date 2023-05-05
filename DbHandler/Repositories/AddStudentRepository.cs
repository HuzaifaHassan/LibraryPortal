using DbHandler.Data;
using DbHandler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace DbHandler.Repositories
{
    public class AddStudentRepository
    {
        private readonly ApplicationDbContext _ctx;
        public AddStudentRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public void AddStudentDets(AddStudent Model)
        {
            _ctx.TStudent.Add(Model);
        }
        public AddStudent GetByStudentId(string StudentId)
        {
            return _ctx.TStudent.Where(x => x.Id == StudentId).FirstOrDefault();


        }
        public List<AddStudent> GetStudent()
        {

            return _ctx.TStudent.Where(x => x.IsActive == true).ToList();
        }
        public AddStudent GetByStudentandCstudentId(string studentid, string cstudentid)
        {
            return _ctx.TStudent.Where(x => x.Id == studentid && x.cstID == cstudentid).FirstOrDefault();

        }
        public void UpdateStudentDet(AddStudent model)
        {
            _ctx.TStudent.Update(model);

        }
        public bool Save()
        {
            return _ctx.SaveChanges() >= 0;
        }
    }
}
