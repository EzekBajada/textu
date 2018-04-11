using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rt_texteditor.Models
{
    public class User
    {
        public int ID { get; set; }
        public String name { get; set; }
        public String surname { get; set; }
        public String email { get; set; }
        public String password { get; set; }

        public virtual ICollection<Login> Login { get; set; }
    }
}