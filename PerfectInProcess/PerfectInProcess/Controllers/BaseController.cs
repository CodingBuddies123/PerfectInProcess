using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PerfectInProcess.Controllers
{
    public class BaseController : Controller
    {
        private String Controller;
        private String Action;
        public BaseController(String controller, String action)
        {
            Controller = controller;
            Action = action;
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);


        }
    }
}