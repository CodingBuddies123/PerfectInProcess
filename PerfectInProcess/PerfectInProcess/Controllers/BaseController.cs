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
            ViewBag.Permissions = GenerateViewBagPermissionList();
            ViewBag.PermissionGroups = GenerateViewBagPermissionGroupList();
            
        }

        private void LoadBase()
        {
            //Load Account
            if(Session["AccountDataModel"] == null)
            {
                Account = new AccountDataModel();
                Session["AccountDataModel"] = Account;
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

        protected List<String[]> GenerateViewBagPermissionList()
        {
            List<String[]> ViewBagPermission = new List<String[]>();

            foreach (PermissionsDataModel P in Account.Role.Permissions)
                ViewBagPermission.Add(new string[] { P.PermissionName, P.PermissionGroupName, P.Action, P.Controller });

            return ViewBagPermission;
        }

        protected List<String> GenerateViewBagPermissionGroupList()
        {
            List<String> ViewBagPermissionGroup = new List<String>();

            foreach (string P in Account.Role.PermissionGroups)
                ViewBagPermissionGroup.Add(P);

            return ViewBagPermissionGroup;
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