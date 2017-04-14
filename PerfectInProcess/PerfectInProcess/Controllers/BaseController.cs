using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PerfectInProcess.Models;

namespace PerfectInProcess.Controllers
{
    public class BaseController : Controller
    {
        private String Controller;
        private String Action;
        private AccountDataModel Account;

        public BaseController()
        {
  
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            Load_BaseInfo();

            if (!CheckPermissions())
            {
                //Redirect to Access Denied Page
            }
        }

        protected void Save_BaseInfo()
        {
            Session["AccountModel"] = Account;
        }

        private void Load_BaseInfo()
        {
            try
            {
                if (Session["AccountModel"] == null)
                    Account = new AccountDataModel();
                else
                    Account = (AccountDataModel)Session["AccountModel"];

            }
            catch
            {
                Account = new AccountDataModel();
            }
        } 

        private Boolean CheckPermissions()
        {
            string Action = this.ControllerContext.RouteData.Values["action"].ToString();
            string Controller = this.ControllerContext.RouteData.Values["controller"].ToString();
            return Account.CheckPermissions(Controller, Action);
        }
    }
}