using Antlr.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Gestionale.Models
{
    public class CustomRoles : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            List<string> ruoli = new List<string>();
            SqlConnection sql= Shared.GetConnection();
            try { 
                sql.Open();
                SqlCommand command = Shared.GetCommand("select Ruolo from Utente where Username=@Username", sql);
                command.Parameters.AddWithValue("@Username", username);
                SqlDataReader reader= command.ExecuteReader();
                if(reader.HasRows) { 
                while(reader.Read()) {
                        ruoli.Add(reader["Ruolo"].ToString());
                    }
                }
            }catch(Exception ex) {
                return null; }
            finally { 
                sql.Close(); 
            }
            return ruoli.ToArray();
            
      
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}