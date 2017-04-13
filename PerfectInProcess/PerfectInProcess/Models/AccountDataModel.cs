using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectInProcess.Models
{
    public class AccountDataModel
    {
        List<PermissionDataModel> Permissions = new List<PermissionDataModel>();

        public Boolean CheckPermissions(String Controller, String Action)
        {
            foreach(PermissionDataModel permission in Permissions)
            {
                if (permission.Action.ToLower() == Action.ToLower() && permission.Controller.ToLower() == Controller.ToLower())
                    return true;
            }

            return false;
        }
    }

    public class PermissionDataModel
    {
        public String Controller { get; private set; }
        public String Action { get; private set; }

        public PermissionDataModel(string action, string controller)
        {
            Controller = controller;
            Action = action;
        }
    }
}