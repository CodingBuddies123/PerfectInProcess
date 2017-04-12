using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfectInProcess.Models;

namespace PerfectInProcess.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {            
            Home Visitor = new Home();
            Visitor.GetVisitorCount();

            Visitor.VisitorCount += 1;

            Visitor.SetVisitorCount();


            return View("Index");
        }
    }
}