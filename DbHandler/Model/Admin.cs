using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHandler.Model
{
    [Table("t_Admin")]
    public class Admin
    {
        [Key]
        public string id { get; set; }
        public string name { get; set; }
        public string password { get; set; }


    }
}
