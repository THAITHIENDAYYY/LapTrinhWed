using namj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace namj.Controllers
{
    public class TRANGCHUController : Controller
    {
        phanmemtinhtiendienEntities db = new phanmemtinhtiendienEntities();
        // GET: TRANGCHU
        public ActionResult Index()
        {
            return View();
        }
    }
}