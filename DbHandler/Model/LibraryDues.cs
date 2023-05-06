using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHandler.Model
{
    [Table("t_LibraryDues")]
    public class LibraryDues
    {
        [Key]
        public string id { get; set; }
        public string cstid { get; set; }
        public string Reference { get; set; }
        public string LibraryDue { get; set; }
        public bool IsCleared { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime daysoverDue { get; set; }
    }
}
