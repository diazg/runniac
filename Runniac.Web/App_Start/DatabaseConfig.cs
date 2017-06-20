using Runniac.Data;
using Runniac.Membership;
using Runniac.Membership.Init;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace Runniac.Web.App_Start
{
    public static class DatabaseConfig
    {

        public static void Setup()
        {
            Database.SetInitializer<UsersContext>(new UsersContextInit());
            UsersContext context = new UsersContext();
            context.Database.Initialize(true);
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                     "UserProfile", "UserId", "UserName", autoCreateTables: true);

            CreateAdminUser();

            Database.SetInitializer<MyDatabaseContext>(new CreateDatabaseIfNotExists<MyDatabaseContext>());
        }

        private static void CreateAdminUser()
        {
            var username = ConfigurationManager.AppSettings.Get("ADMIN_EMAIL");
            var password = ConfigurationManager.AppSettings.Get("ADMIN_PASS");
            var role = "Administrator";

            if (!WebSecurity.UserExists(username))
                WebSecurity.CreateUserAndAccount(username, password, new { Name = "Administrador"});                

            if (!Roles.RoleExists(role))
                Roles.CreateRole(role);

            if (!Roles.IsUserInRole(username, role))
                Roles.AddUserToRole(username, role);
        }
    }
}