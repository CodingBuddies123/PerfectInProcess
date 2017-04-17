using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PerfectInProcess.Models.DataModel;

namespace PerfectInProcess.Controllers
{
    public class BaseController : Controller
    {
        protected AccountDataModel Account;
        string Controller;
        string Action;

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.LoadBase();
            this.VerifyPermissions();

            ViewBag.Account = Account;
        }

        private void LoadBase()
        {
            //Load Account
            if(Session["AccountDataModel"] == null)
            {
                Session["AccountDataModel"] = new AccountDataModel();
                Account = new AccountDataModel();
            }
            else
            {
                try
                {
                    Account = (AccountDataModel)Session["AccountDataModel"];
                }
                catch
                {
                    Session["AccountDataModel"] = new AccountDataModel();
                }
            }
        }

        protected void SaveBase()
        {
            Session["AccountDataModel"] = Account;
        }

        protected void SaveToSession(Object obj, string key)
        {
            Session[key] = obj;
        }

        protected Object LoadFromSession(string key)
        {
            return Session[key];
        }

        protected void SaveToTempData(Object obj, string key)
        {
            TempData[key] = obj;
        }

        protected Object LoadFromTempData(string key)
        {
            return TempData[key];
        }


        private void VerifyPermissions()
        {
            Controller = this.ControllerContext.RouteData.Values["controller"].ToString();
            Action = this.ControllerContext.RouteData.Values["action"].ToString();

            if (!Account.Role.VerifyPermission(Controller, Action))
            {
                //redirect
            }

        }
    }
}