using System;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Runniac.Web;
using Runniac.Business.Impl;
using Runniac.Data.Repositories;
using Moq;
using Runniac.Data;
using Runniac.Web.ViewModels;
using Runniac.Web.App_Start;
using Runniac.Tests.Fakes;
using System.Linq;
using Runniac.ExternalDataExtraction;
using Runniac.Web.Controllers;
using System.Web.Http;
using Runniac.Web.WebAppServices;
using Runniac.Membership;
using Runniac.Membership.Models;
using Runniac.Tests.Mocks;

namespace Runniac.Tests
{
    [TestClass]
    public class CommentsControllerTest
    {
        CommentsController _controller;
        Mock<IUnitOfWork> _uow;

        [TestInitialize]
        public void Init()
        {
            _uow = MockingFactory.SetupUowMock();

            var commentService = new CommentService(_uow.Object);
            var photoService = new PhotoService(_uow.Object);
            var userService = new UserService(_uow.Object, commentService, photoService);
            var context = new FakeUserContext();

            var mockWebSecurityService = new Mock<IWebSecurityService>();
            mockWebSecurityService.Setup(m => m.CurrentUserId).Returns(1);

            _controller = new CommentsController(commentService, mockWebSecurityService.Object, context, userService);

            AutoMapperWebConfiguration.Configure();
        }        

        [TestMethod]
        public void Save_Comment_Test()
        {
            //Arrange
            var comment = new CommentVM();

            //Act
            _controller.Post(comment);

            //Assert
            _uow.Verify(r => r.Save(), Times.AtMostOnce());
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void Comment_Not_Saved_If_Validation_Error_Test()
        {
            //Arrange
            var comment = new CommentVM();
            _controller.ModelState.AddModelError("test", "foo");

            //Act
            _controller.Post(comment);
        }

        //[TestMethod]
        //[ExpectedException(typeof(HttpResponseException))]
        //public void Comment_Not_Returned_If_No_Event_Passed_Test()
        //{
        //    //Act
        //    _controller.GetByEventId(-1);
        //}

        //[TestMethod]
        //public void Comments_Returned_With_Attached_Event_Test()
        //{
        //    //Act
        //    var comments = _controller.GetByEventId(1);

        //    //Assert
        //    Assert.AreEqual(3, comments.Count());
        //    Assert.IsTrue(comments.ToList()[1].EventId > 0);
        //}

        //[TestMethod]
        //public void Comments_Returned_With_Attached_User_Test()
        //{
        //    //Act
        //    var comments = _controller.GetByEventId(2);

        //    //Assert
        //    Assert.IsTrue(comments.ToList()[0].User.Name == "Eugenia");
        //}
    }
}
