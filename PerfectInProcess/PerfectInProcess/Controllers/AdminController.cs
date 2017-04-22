using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfectInProcess.Models.DataModel;
using PerfectInProcess.Models.ViewModel;

namespace PerfectInProcess.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditRolesAndPermissions()
        {
            List<RoleDataModel> ROLES = RoleDataModel.GetAllRoles();
            List<PermissionsDataModel> PERMISSIONS = PermissionsDataModel.GetAllPermissions();

            return View();
        }
    }
}