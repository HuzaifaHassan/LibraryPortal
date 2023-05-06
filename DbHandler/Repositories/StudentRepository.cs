using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbHandler.Data;
using DbHandler.Model;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbHandler.Repositories
{
    public  class StudentRepository: IStudentRepositories
    {
        private readonly ApplicationDbContext _ctx;
        public StudentRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public void StudentDetails(StudentDetails model)
        {
            _ctx.TStudentDet.Add(model);


        }
        public StudentDetails GetByStudentId(string StudentId)
        {
            return _ctx.TStudentDet.Where(x => x.stId == StudentId).FirstOrDefault();


        }
        public StudentDetails GetByNameandPassword(string name, string password)
        {
            var resp = _ctx.TStudentDet.Where(x => x.Name == name && x.Password== password).FirstOrDefault();
            return resp;

        }
        public List<StudentDetails> GetStudent()
        {
            return _ctx.TStudentDet.Where(x => x.IsActive == true).ToList();
        }
        public StudentDetails GetByStudentandCstudentId(string studentid, string cstudentid)
        {
            return _ctx.TStudentDet.Where(x => x.Id == studentid && x.cstID == cstudentid).FirstOrDefault();

        }
        public void UpdateStudentDet(StudentDetails model)
        {
            _ctx.TStudentDet.Update(model);

        }
        public bool Save()
        {
            return _ctx.SaveChanges() >= 0;
        }
    }
}
