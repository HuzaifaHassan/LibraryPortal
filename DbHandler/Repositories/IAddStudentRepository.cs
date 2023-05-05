using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbHandler.Data;
using DbHandler.Model;
using System.Threading.Tasks;

namespace DbHandler.Repositories
{
    public interface IAddStudentRepository
    {
        public void AddStudentDets(AddStudent Model);
        public AddStudent GetByStudentId(string StudentId);
        public List<AddStudent> GetStudent();
        public AddStudent GetByStudentandCstudentId(string studentid, string cstudentid);
        public void UpdateStudentDet(AddStudent model);
        public bool Save();
    }
}
