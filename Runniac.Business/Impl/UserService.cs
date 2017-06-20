using Runniac.Data;
using Runniac.Data.Repositories;
using Runniac.ExternalDataExtraction;
using Runniac.Utils;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Business.Impl
{
    public class UserService : AbstractService, IUserService
    {
        private ICommentService _commentService;
        private IPhotoService _photoService;

        public UserService(IUnitOfWork uow, ICommentService commentService, IPhotoService photoService)
            : base(uow)
        {
            _commentService = commentService;
            _photoService = photoService;
        }


        /// <inheritdoc />
        public int GetPoints(int userId)
        {
            var points = 0;
            var userComments = _uow.CommentRepository.Get(c => c.UserId == userId);

            foreach (var comment in userComments)
                points += _commentService.WorkoutRanking(comment.Id);

            points += _photoService.GetPhotosByUser(userId).Count();

            return points;
        }

        /// <inheritdoc />
        public User GetById(long userId)
        {
            return _uow.UserRepository.GetById(userId);
        }

        /// <inheritdoc />
        public User GetByUserName(string userName)
        {
            return _uow.UserRepository.GetByUserName(userName);
        }

        /// <inheritdoc />
        public void SaveUser(User user)
        {
            _uow.UserRepository.Insert(user);
            _uow.Save();
        }
    }
}
