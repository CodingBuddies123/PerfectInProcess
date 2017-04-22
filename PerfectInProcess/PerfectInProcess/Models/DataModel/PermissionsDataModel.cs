using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PerfectInProcess.Models.DataModel
{
    public class PermissionsDataModel : BaseDataModel
    {
        public int PermissionID { get; private set; }
        public string PermissionName { get; private set; }
        public string PermissionGroupName { get; private set; }
        public string Controller { get; private set; }
        public string Action { get; private set; }
        public int RiskLevel { get; private set; }
        public Boolean Hidden { get; private set; }

        public PermissionsDataModel(int permissionID, string permissionName, string permissionGroupName, string controller, string action, int riskLevel)
        {
            PermissionID = permissionID;
            PermissionName = permissionName;
            PermissionGroupName = permissionGroupName;
            Controller = controller;
            Action = action;
            Hidden = permissionGroupName.ToLower() == "hidden";
            RiskLevel = riskLevel;
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

        public static List<PermissionsDataModel> GetAllPermissions()
        {
            List<PermissionsDataModel> Permissions = new List<PermissionsDataModel>();
            try
            {
                using (SqlConnection SqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.ConnectionStrings["DefaultConnection"])))
                {
                    SqlConnection.Open();
                    using (SqlCommand command = new SqlCommand("spPermissionsSelectAll", SqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Permissions.Add(new PermissionsDataModel((int)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], (string)reader[4], (int)reader[5]));
                        }
                    }
                }
                return Permissions;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}