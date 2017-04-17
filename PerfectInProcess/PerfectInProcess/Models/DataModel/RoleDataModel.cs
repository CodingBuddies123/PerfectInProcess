using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectInProcess.Models.DataModel
{
    public class RoleDataModel : BaseDataModel
    {
        int RoleID;
        String RoleName;
        public List<PermissionsDataModel> Permissions { get; private set; } = new List<PermissionsDataModel>();
        public List<String> PermissionGroups { get; private set; } = new List<string>();

        public RoleDataModel(int roleID)
        {
            
        }

        public RoleDataModel()
        {
            //static data
            Permissions.Add(new PermissionsDataModel(1, "Register Account", "Account", "Account", "Register"));
            Permissions.Add(new PermissionsDataModel(2, "Login", "Account", "Account", "Login"));
            Permissions.Add(new PermissionsDataModel(3, "Home", "Home", "Home", "Home"));
            Permissions.Add(new PermissionsDataModel(4, "Contact Us", "Home", "Home", "Contact Us"));
            Permissions.Add(new PermissionsDataModel(5, "About Us", "Home", "Home", "About Us"));

            foreach(PermissionsDataModel p in Permissions)
            {
                if (!PermissionGroups.Contains(p.PermissionGroupName))
                    PermissionGroups.Add(p.PermissionGroupName);
            }
        }

        public Boolean VerifyPermission(string controller, string action)
        {
            foreach (PermissionsDataModel permission in Permissions)
            {
                if (permission.Controller.ToLower() == controller.ToLower() && permission.Action.ToLower() == action.ToLower())
                    return true;
            }

            return false;
        }

        public Boolean CreateRole(string rolename)
        {
            return false;
        }

        public Boolean EditRole(string rolename)
        {
            return false;
        }

        public Boolean RemoveRole()
        {
            return false;
        }

        public Boolean AddPermission(PermissionsDataModel permission)
        {
            return false;
        }

        public Boolean RemovePermission(PermissionsDataModel permission)
        {
            return false;
        }

        
    }
}