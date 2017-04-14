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
        List<PermissionsDataModel> Permissions = new List<PermissionsDataModel>();

        public RoleDataModel(int roleID)
        {

        }

        public RoleDataModel()
        {

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