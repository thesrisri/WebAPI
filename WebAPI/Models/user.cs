using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class user
    {
        public int UserId { get; set; }

        public string UserName { get; set; }
        public string email { get; set; }

        public int age { get; set; }
        public string encryptedPassword { get; set; }
        public Int64 registeredAt { get; set;  }
        public string photoFileName{ get; set; }

        public string photoFilePath { get; set; }

    }

   
}
