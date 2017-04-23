﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfectInProcess.Models.DataModel;
using PerfectInProcess.Models.ViewModel;

namespace PerfectInProcess.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditRolesAndPermissions(EditRolesAndPermissionsViewModel view)
        {
            view.InitialRoles = RoleDataModel.GetAllRoles();
            view.SelectedRole = view.InitialRoles.First();
            foreach (PermissionsDataModel p in PermissionsDataModel.GetAllPermissions())
            {
                if (view.SelectedRole.Permissions.Find(x => x.PermissionID == p.PermissionID) != null)
                {
                    view.InitialAssignedPermissions.Add(p);
                }
                else
                {
                    view.InitialUnassignedPermissions.Add(p);
                }
            }
            view.TableRowMax = Math.Max(view.InitialRoles.Count, Math.Max(view.InitialAssignedPermissions.Count, view.InitialUnassignedPermissions.Count));
            TempData["PreviousView"] = view;
            return View("EditRolesAndPermissions", view);
        }

        public ActionResult EditRolesAndPermissionsRoleRowSelection(EditRolesAndPermissionsViewModel view)
        {
            view = (EditRolesAndPermissionsViewModel)TempData["PreviousView"];
            int roleID = int.Parse(Request.QueryString["roleID"]);
            view.SelectedRole = view.InitialRoles.Find(x => x.RoleID == roleID);
            view.InitialAssignedPermissions.Clear();
            view.InitialUnassignedPermissions.Clear();

            foreach (PermissionsDataModel p in PermissionsDataModel.GetAllPermissions())
            {
                if (view.SelectedRole.Permissions.Find(x => x.PermissionID == p.PermissionID) != null)
                {
                    view.InitialAssignedPermissions.Add(p);
                }
                else
                {
                    view.InitialUnassignedPermissions.Add(p);
                }
            }

            view.TableRowMax = Math.Max(view.InitialRoles.Count, Math.Max(view.InitialAssignedPermissions.Count, view.InitialUnassignedPermissions.Count));
            TempData["PreviousView"] = view;

            return View("EditRolesAndPermissions", view);
        }

        public ActionResult EditRolesAndPermissionsSubmitChanges(EditRolesAndPermissionsViewModel view)
        {
            view = (EditRolesAndPermissionsViewModel)TempData["PreviousView"];
            List<PermissionsDataModel> ToAdd = new List<PermissionsDataModel>();
            List<PermissionsDataModel> ToRemove = new List<PermissionsDataModel>();

            foreach(PermissionsDataModel permissionAssigned in view.InitialAssignedPermissions)
            {
                if(view.SelectedRole.Permissions.Find(x => x.PermissionID == permissionAssigned.PermissionID) == null)
                {
                    ToAdd.Add(permissionAssigned);
                }
            }

            foreach (PermissionsDataModel permissionUnassigned in view.InitialUnassignedPermissions)
            {
                if (view.SelectedRole.Permissions.Find(x => x.PermissionID == permissionUnassigned.PermissionID) != null)
                {
                    ToRemove.Add(permissionUnassigned);
                }
            }

            if (ToAdd.Count > 0)
                view.SelectedRole.AssignPermissions(ToAdd);

            if (ToRemove.Count > 0)
                view.SelectedRole.UnassignPermissions(ToRemove);


            return View("EditRolesAndPermissions", view);
        }

        public ActionResult EditRolesAndPermissionsUnassignedPermissionsRowSelection(EditRolesAndPermissionsViewModel view)
        {
            view = (EditRolesAndPermissionsViewModel)TempData["PreviousView"];
            int AssignedPermission = int.Parse(Request.QueryString["AssignedPermissionsID"]);
            view.InitialAssignedPermissions.Add(view.InitialUnassignedPermissions.Find(x => x.PermissionID == AssignedPermission));
            view.InitialUnassignedPermissions.Remove(view.InitialUnassignedPermissions.Find(x => x.PermissionID == AssignedPermission));
            TempData["PreviousView"] = view;
            return View("EditRolesAndPermissions", view);
        }

        public ActionResult EditRolesAndPermissionsAssignedPermissionsRowSelection(EditRolesAndPermissionsViewModel view)
        {
            view = (EditRolesAndPermissionsViewModel)TempData["PreviousView"];
            int UnasignedPermission = int.Parse(Request.QueryString["UnassignedPermissionsID"]);
            view.InitialUnassignedPermissions.Add(view.InitialAssignedPermissions.Find(x => x.PermissionID == UnasignedPermission));
            view.InitialAssignedPermissions.Remove(view.InitialAssignedPermissions.Find(x => x.PermissionID == UnasignedPermission));
            TempData["PreviousView"] = view;
            return View("EditRolesAndPermissions", view);
        }
    }
}