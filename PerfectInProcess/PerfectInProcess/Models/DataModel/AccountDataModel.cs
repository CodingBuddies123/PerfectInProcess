using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PerfectInProcess.Models.DataModel
{
    public class AccountDataModel : BaseDataModel
    {
        List<PermissionsDataModel> Permissions = new List<PermissionsDataModel>();

        public Boolean VerifyPermission(string controller, string action)
        {
            foreach(PermissionsDataModel permission in Permissions)
            {
                if (permission.Controller.ToLower() == controller.ToLower() && permission.Action.ToLower() == action.ToLower())
                    return true;
            }

            return false;
        }
    }
}