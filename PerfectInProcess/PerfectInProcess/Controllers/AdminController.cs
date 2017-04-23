using System;
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

        public ActionResult EditRolesAndPermissions()
        {
            EditRolesAndPermissionsViewModel view = new EditRolesAndPermissionsViewModel();
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
            return View("EditRolesAndPermissions", view);
        }

        public ActionResult EditRolesAndPermissionsRoleRowSelection(EditRolesAndPermissionsViewModel view)
        {

            return View("EditRolesAndPermissions", view);
        }

        public ActionResult EditRolesAndPermissionsSubmit(EditRolesAndPermissionsViewModel view)
        {
            return View("EditRolesAndPermissions", view);
        }
    }
}