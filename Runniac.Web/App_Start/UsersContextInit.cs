using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using WebMatrix.WebData;

namespace Runniac.Membership.Init
{
    public class UsersContextInit : CreateDatabaseIfNotExists<UsersContext>
    {        

        protected override void Seed(UsersContext context)
        {
            SeedMembership();
        }

        private void SeedMembership()
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection",
               "UserProfile", "UserId", "UserName", autoCreateTables: true);            
        }
    }
}
