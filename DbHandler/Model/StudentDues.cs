using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHandler.Model
{
    [Table("t_StudentDue")]
    public class StudentDues
    {
        [Key]
        public string Id { get; set; }
        public string cstid { get; set; }
        public string Reference { get; set; }
        public string LibraryDues { get; set; }
        public bool IsCleared { get; set; }

    }
}
