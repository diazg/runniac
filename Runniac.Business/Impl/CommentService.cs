using Runniac.Data;
using Runniac.Data.Repositories;
using Runniac.ExternalDataExtraction;
using Runniac.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.Business.Impl
{
    public class CommentService : AbstractService, ICommentService
    {

        public CommentService(IUnitOfWork uow)
            : base(uow)
        { }


        /// <inheritdoc />
        public void SaveComment(Comment comment)
        {            
            _uow.CommentRepository.Insert(comment);
            _uow.Save();
        }

        /// <inheritdoc />
        public IEnumerable<Comment> GetCommentsByEvent(long eventId)
        {
            var comments = _uow.CommentRepository.GetByEvent(eventId);

            foreach (var comment in comments)
            {
                comment.Ranking = this.WorkoutRanking(comment.Id);
            }

            return comments.OrderByDescending(c => c.Ranking);
        }

        /// <inheritdoc />
        public bool AddVote(Vote vote)
        {
            var voteInDb = _uow.VoteRepository.Find(vote.CommentId, vote.UserId);
            var voteAdded = false;

            if (voteInDb == null)
            {
                _uow.VoteRepository.Insert(vote);
                voteAdded = true;
            }
            else if (voteInDb.Positive != vote.Positive)
            {
                _uow.VoteRepository.Delete(voteInDb.Id);
                voteAdded = true;
            }
            _uow.Save();

            return voteAdded;
        }

        /// <inheritdoc />
        public Comment GetComment(long commentId)
        {
            return _uow.CommentRepository.GetByID(commentId);
        }

        /// <inheritdoc />
        public IEnumerable<Comment> GetCommentsByUser(int userId)
        {
            return _uow.CommentRepository.GetByUser(userId);
        }

        /// </inheritDoc>
        public int WorkoutRanking(long commentId)
        {
            var ranking = 0;
            var votes = _uow.VoteRepository.GetByComment(commentId);

            foreach (var vote in votes)
                ranking = ranking + (vote.Positive ? 1 : -1);

            return ranking;
        }

        /// </inheritDoc>
        public void DeleteComments(int userId)
        {
            var comments = _uow.CommentRepository.GetByUser(userId);

            foreach (var item in comments)
            {
                _uow.CommentRepository.Delete(item);
            }
            _uow.Save();
        }
    }
}
