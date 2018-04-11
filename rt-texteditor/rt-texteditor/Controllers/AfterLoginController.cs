using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rt_texteditor.Controllers
{
    public class AfterLoginController : Controller
    {
        // GET: AfterLogin
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}