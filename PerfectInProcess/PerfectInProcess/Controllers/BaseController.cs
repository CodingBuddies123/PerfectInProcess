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
        private Object Account;

        public BaseController()
        {
  
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            string Action = this.ControllerContext.RouteData.Values["action"].ToString();
            string Controller = this.ControllerContext.RouteData.Values["controller"].ToString();


        }
    }
}