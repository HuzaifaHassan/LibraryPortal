﻿using System;
using System.Collections.Generic;
using System.Linq;
using DbHandler.Data;
using DbHandler.Model;
using System.Text;
using System.Threading.Tasks;


namespace DbHandler.Repositories
{
    public interface ILibraryDuesRepository
    {
        public void AddLibraryDue(LibraryDues model);
        public LibraryDues GetByStudentid(string id);
        public LibraryDues GetByStudentIdAndRef(string Cid,string _ref);
        public void UpdateLibraryDues(LibraryDues model);
        public LibraryDues GetByStudentidOrCstId( string cstid);
        public bool Save();

    }
}
