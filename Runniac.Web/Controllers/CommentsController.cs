using AutoMapper;
using log4net;
using Runniac.Business;
using Runniac.Data;
using Runniac.ExternalDataExtraction;
using Runniac.Membership;
using Runniac.Membership.Models;
using Runniac.Web.ViewModels;
using Runniac.Web.WebAppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebMatrix.WebData;

namespace Runniac.Web.Controllers
{
    public class CommentsController : ApiController
    {
        private ICommentService _commentService;
        private IUserService _userService;
        private IWebSecurityService _securityService;
        private IUsersContext _usersDbContext;

        public CommentsController(ICommentService commentService, IWebSecurityService securityService, IUsersContext usersDbContext,
            IUserService userService)
        {
            _commentService = commentService;
            _securityService = securityService;
            _usersDbContext = usersDbContext;
            _userService = userService;
        }


        // POST api/<controller>/saveComment
        [System.Web.Mvc.RequireHttps]
        [Authorize]
        [HttpPost]
        [ActionName("saveComment")]
        public CommentVM Post(CommentVM comment)
        {
            if (comment != null && ModelState.IsValid)
            {
                comment.UserId = _securityService.CurrentUserId;
                comment.CommentDate = String.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
                var commentModel = Mapper.Map<CommentVM, Comment>(comment);
                _commentService.SaveComment(commentModel);

                comment.User = Mapper.Map<User, UserVM>(_userService.GetById(comment.UserId));
                comment.User.Points = _userService.GetPoints(comment.UserId);
                return comment;
            }
            else
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);
        }

        [System.Web.Mvc.RequireHttps]
        [Authorize]
        [HttpPost]
        public void SubmitVote(VoteVM vote)
        {
            if (vote != null)
            {
                if (userVotingOwnComment(vote.CommentId))
                    throw new HttpResponseException(HttpStatusCode.Forbidden);

                vote.UserId = _securityService.CurrentUserId;
                var voteSubmitted = _commentService.AddVote(Mapper.Map<VoteVM, Vote>(vote));

                if (!voteSubmitted)
                    throw new HttpResponseException(HttpStatusCode.NotAcceptable);
            }
            else
                throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        private bool userVotingOwnComment(long commentId)
        {
            var comment = _commentService.GetComment(commentId);
            return comment.UserId == _securityService.CurrentUserId;
        }
    }
}