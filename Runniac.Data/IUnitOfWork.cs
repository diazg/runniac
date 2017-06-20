using Runniac.Data.Repositories;
using Runniac.Data;
using System;
namespace Runniac.Data
{
    public interface IUnitOfWork
    {
        ICommentRepository CommentRepository { get; }
        IPhotoRepository PhotoRepository { get; }
        IEventRepository EventRepository { get; }
        IVoteRepository VoteRepository { get; }
        ITownRepository TownRepository { get; }
        IUserRepository UserRepository { get; }

        void Dispose();
        void Save();
    }
}
