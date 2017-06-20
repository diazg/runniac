using Moq;
using Runniac.Data;
using Runniac.Data.Repositories;
using Runniac.ExternalDataExtraction;
using Runniac.Tests.Fakes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Runniac.Tests.Mocks
{
    public static class MockingFactory
    {

        public static Mock<IPhotoRepository> SetupPhotoRepositoryMock()
        {
            var mockPhotoRepository = new Mock<IPhotoRepository>();
            mockPhotoRepository.Setup(m => m.GetByUser(It.IsAny<int>())).Returns(FakeData.GetPhotos());

            return mockPhotoRepository;
        }

        public static Mock<ICommentRepository> SetupCommentRepositoryMock()
        {
            var mockCommentRepository = new Mock<ICommentRepository>();
            mockCommentRepository.Setup(m => m.Insert(It.IsAny<Comment>())).Verifiable();
            mockCommentRepository.Setup(m => m.GetByEvent(It.IsAny<long>())).Returns(FakeData.GetComments());

            return mockCommentRepository;
        }

        public static Mock<IVoteRepository> SetupVoteRepositoryMock()
        {
            var mockVoteRepository = new Mock<IVoteRepository>();

            return mockVoteRepository;
        }

        public static Mock<IEventRepository> SetupEventRepositoryMock()
        {
            var mockEventRepository = new Mock<IEventRepository>();
            mockEventRepository.Setup(m => m.Insert(It.IsAny<Event>())).Verifiable();
            mockEventRepository.Setup(m => m.Find(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime?>())).
                Returns(FakeData.GetEvents());
            mockEventRepository.Setup(m => m.GetLocations(It.IsAny<string>())).Returns(FakeData.GetLocations());
            mockEventRepository.Setup(m => m.Get(It.IsNotNull<Expression<Func<Event, bool>>>(),
                        It.IsAny<Func<IQueryable<Event>, IOrderedQueryable<Event>>>(),
                        It.IsAny<string>(), It.IsAny<int>())).Returns(FakeData.GetEvents());
            mockEventRepository.Setup(m => m.GetAllOrdered()).Returns(FakeData.GetEvents());

            return mockEventRepository;
        }

        public static Mock<ITownRepository> SetupTownRepositoryMock()
        {
            var mockTownRepository = new Mock<ITownRepository>();
            mockTownRepository.Setup(m => m.GetByName(It.IsAny<string>())).Returns(FakeData.GetTowns().First());

            return mockTownRepository;
        }

        public static Mock<IUserRepository> SetupUserRepositoryMock()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(m => m.GetById(It.IsAny<Int64>())).Returns(FakeData.GetUser());

            return mockUserRepository;
        }
        
        public static Mock<IUnitOfWork> SetupUowMock()
        {
            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(m => m.CommentRepository).Returns(SetupCommentRepositoryMock().Object);
            mockUow.Setup(m => m.EventRepository).Returns(SetupEventRepositoryMock().Object);
            mockUow.Setup(m => m.PhotoRepository).Returns(SetupPhotoRepositoryMock().Object);
            mockUow.Setup(m => m.VoteRepository).Returns(SetupVoteRepositoryMock().Object);
            mockUow.Setup(m => m.TownRepository).Returns(SetupTownRepositoryMock().Object);
            mockUow.Setup(m => m.UserRepository).Returns(SetupUserRepositoryMock().Object);
            mockUow.Setup(m => m.Save()).Verifiable();

            return mockUow;
        }

        internal static Mock<ExternalDataExtraction.IMultiExtractor> SetupEventsExtractorMock()
        {
            var extractorMock = new Mock<IMultiExtractor>();
            extractorMock.Setup(m => m.GetExtraInfo(It.IsAny<Event>(), It.IsAny<string>()))
                .Returns(new Event());

            return extractorMock;
        }
    }
}
