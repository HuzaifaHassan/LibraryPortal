using DbHandler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace LibraryPortal.Responses
{
    public class ResponseDTO
    {
       
    }
    public class RegistraiontObject
    { 
      public string StId { get; set; }

      public string StName { get; set; }  

      public string CStId { get; set; }

      public bool? IsActive { get; set; }
    
    }
}
