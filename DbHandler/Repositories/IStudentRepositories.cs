using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbHandler.Model;
using DbHandler.Data;
using System.Threading.Tasks;
namespace DbHandler.Repositories
{
    public interface IStudentRepositories
    {
        public void StudentDetails(StudentDetails model);
        public StudentDetails GetByStudentId(string StudentId);
        public List<StudentDetails> GetStudent();
        public StudentDetails GetByStudentandCstudentId(string studentid, string cstudentid);
        public void UpdateStudentDet(StudentDetails model);
        public StudentDetails GetByNameandPassword(string name, string password);
        public bool Save();

    }
}
