using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectInProcess.Models.DataModel
{
    public class PermissionsDataModel : BaseDataModel
    {
        public int PermissionID { get; private set; }
        public string PermissionName { get; private set; }
        public string PermissionGroupName { get; private set; }
        public string Controller { get; private set; }
        public string Action { get; private set; }
        public Boolean Hidden { get; private set; }

        public PermissionsDataModel(int permissionID, string permissionName, string permissionGroupName, string controller, string action)
        {
            PermissionID = permissionID;
            PermissionName = permissionName;
            PermissionGroupName = permissionGroupName;
            Controller = controller;
            Action = action;
            Hidden = permissionGroupName.ToLower() == "hidden";
        }

        public PermissionsDataModel()
        {

        }

        public Boolean CreatePermission(string permissionName, string permissionGroupName, string controller, string action)
        {
            return false;
        }

        public Boolean EditPermission(string permissionName, string permissionGroupName, string controller, string action)
        {
            return false;
        }

        public Boolean DeletePermission()
        {
            return false;
        }
    }
}