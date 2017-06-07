using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PerfectInProcess.Models.DataModel;

namespace PerfectInProcess.Models.ViewModel
{
    public class EditRolesAndPermissionsViewModel
    {
        public RoleDataModel SelectedRole;
        public List<RoleDataModel> InitialRoles;
        public List<PermissionsDataModel> InitialAssignedPermissions = new List<PermissionsDataModel>();
        public List<PermissionsDataModel> InitialUnassignedPermissions = new List<PermissionsDataModel>();
        public int TableRowMax = 0;
        public int TableHeight = 5;
        public int SelectedRow = -1;
        public Boolean ChangesMade = false;
    }
}