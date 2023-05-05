using System;
using System.Collections.Generic;
using System.Linq;
using DbHandler.Data;
using DbHandler.Model;
using System.Text;
using System.Threading.Tasks;


namespace DbHandler.Repositories
{
    public interface IStudentDuesRepository
    {
        public void AddLibraryDue(StudentDues model);
        public StudentDues GetByStudentid(string id);
        public StudentDues GetByStudentIdAndRef(string id, string _ref);
        public void UpdateStudentLibraryDues(StudentDues model);
        public bool Save();
    }
}
