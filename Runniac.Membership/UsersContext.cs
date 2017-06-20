using Runniac.Membership.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Membership
{
    public class UsersContext : DbContext, IUsersContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {

        }

        public IDbSet<UserInfo> UserProfiles { get; set; }
        public IDbSet<UserMembership> Memberships { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
