﻿using PerfectInProcess.Models.DataModel;
using System;
using System.Collections.Generic;

namespace PerfectInProcess.Models.ViewModel
{
    public class EditRolesAndPermissionsViewModel
    {
        public RoleDataModel SelectedRole;
        public List<RoleDataModel> InitialRoles;
        public List<PermissionsDataModel> InitialAssignedPermissions = new List<PermissionsDataModel>();
        public List<PermissionsDataModel> InitialUnassignedPermissions = new List<PermissionsDataModel>();
        public int TableRowMax = 0;
        public int TableHeight = 8;
        public int SelectedRow = -1;
        public Boolean ChangesMade = false;
    }
}