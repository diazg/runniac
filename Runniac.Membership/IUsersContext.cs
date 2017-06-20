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
    public interface IUsersContext
    {
        IDbSet<UserInfo> UserProfiles { get; set; }
        IDbSet<UserMembership> Memberships { get; set; }
    }
}
