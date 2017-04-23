using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PerfectInProcess.Models.DataModel
{
    public class RoleDataModel : BaseDataModel
    {
        public int RoleID { get; private set; }
        public String RoleName { get; private set; }
        public int RiskLevel { get; private set; }
        public List<PermissionsDataModel> Permissions { get; private set; } = new List<PermissionsDataModel>();
        public List<String> PermissionGroups { get; private set; } = new List<string>();

        public RoleDataModel(int roleID)
        {
            try
            {
                using (SqlConnection SqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.ConnectionStrings["DefaultConnection"])))
                {
                    SqlConnection.Open();
                    using (SqlCommand command = new SqlCommand("spRole_LoadInfoByRoleID", SqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleID;
                        command.Parameters.Add("@Error", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                        SqlDataReader reader = command.ExecuteReader();

                        Permissions.Clear();
                        Boolean first = true;
                        
                        while (reader.Read())
                        {
                            Permissions.Add(new PermissionsDataModel((int)reader[3], (string)reader[4], (string)reader[5], (string)reader[6], (string)reader[7], (int)reader[8]));

                            if(!PermissionGroups.Contains((string)reader[5]))
                            {
                                PermissionGroups.Add((string)reader[5]);
                            }

                            if(first)
                            {
                                first = false;
                                RoleID = (int)reader[0];
                                RoleName = (string)reader[1];
                                RiskLevel = (int)reader[2];
                            }
                        }

                    }
                }
            }
            catch (SqlException ex)
            {
                base.SetError(ex.Message);
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


        public static List<RoleDataModel> GetAllRoles()
        {
            List<RoleDataModel> Roles = new List<RoleDataModel>();
            List<int> RoleIDs = new List<int>();
            try
            {
                using (SqlConnection SqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.ConnectionStrings["DefaultConnection"])))
                {
                    SqlConnection.Open();
                    using (SqlCommand command = new SqlCommand("spRolesSelectAll", SqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            RoleIDs.Add((int)reader[0]);
                        }
                    }

                    foreach(int roleID in RoleIDs)
                    {
                        Roles.Add(new RoleDataModel(roleID));
                    }
                }
                return Roles;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}