using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rt_texteditor.Models
{
    public class Login
    {
        public string email { get; set; }
        public string password { get; set; }

        public virtual User user { get; set; }
    }
}