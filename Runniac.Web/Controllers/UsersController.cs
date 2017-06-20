using AutoMapper;
using log4net;
using Runniac.Business;
using Runniac.Email;
using Runniac.Data;
using Runniac.ExternalDataExtraction;
using Runniac.Membership;
using Runniac.Membership.Models;
using Runniac.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using WebMatrix.WebData;
using WebApi.OutputCache.V2;

namespace Runniac.Web.Controllers
{    
    public class UsersController : ApiController
    {
        private ILog _logger = LogManager.GetLogger(typeof(MvcApplication));
        private IUsersContext _usersDbContext;
        private IEmailSender _emailSender;
        private ICommentService _commentsService;

        public UsersController(IUsersContext usersDbContext, IEmailSender emailSender, ICommentService commentsService)
        {
            _usersDbContext = usersDbContext;
            _emailSender = emailSender;
            _commentsService = commentsService;
        }

        [System.Web.Mvc.RequireHttps]
        [Authorize]
        [InvalidateCacheOutput("Get")]
        public void Delete(int id)
        {
            var user = _usersDbContext.UserProfiles.FirstOrDefault(u => u.UserId == id);

            if (user == null)
                return;

            try
            {
                _commentsService.DeleteComments(id);
                if (Roles.GetRolesForUser(user.UserName).Count() > 0)
                {
                    Roles.RemoveUserFromRoles(user.UserName, Roles.GetRolesForUser(user.UserName));
                }

                ((SimpleMembershipProvider)System.Web.Security.Membership.Provider).DeleteAccount(user.UserName); // deletes record from webpages_Membership table
                ((SimpleMembershipProvider)System.Web.Security.Membership.Provider).DeleteUser(user.UserName, true); // deletes record from UserProfile table

                return;
            }
            catch
            {
                return;
            }
        }

        // GET api/<controller>
        [System.Web.Mvc.RequireHttps]
        [Authorize]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IEnumerable<UserVM> Get()
        {
            var users = _usersDbContext.UserProfiles.ToList();
            return Mapper.Map<IEnumerable<UserInfo>, IEnumerable<UserVM>>(users);
        }

        [HttpGet]
        [ActionName("isUserLoggedIn")]
        public dynamic IsUserLoggedIn()
        {
            return new { result = User.Identity.IsAuthenticated };
        }
    }
}