using Runniac.Membership;
using Runniac.Membership.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Tests.Fakes
{
    public class FakeUserContext : IUsersContext
    {
        public FakeUserContext()
        {
            this.UserProfiles = new FakeDbSet<UserInfo>(){                
                new UserInfo { UserId = 1, UserName = "eugenia@runniac.com" },
                new UserInfo { UserId = 2, UserName = "cesar@runniac.com" },
                new UserInfo { UserId = 2, UserName = "alberto@runniac.com" }
            };
        }

        public IDbSet<UserInfo> UserProfiles { get; set; }
        public IDbSet<UserMembership> Memberships { get; set; }
    }
}
