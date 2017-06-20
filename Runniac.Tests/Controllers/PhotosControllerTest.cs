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
    public class PhotosControllerTest
    {
        PhotosController _controller;
        Mock<IUnitOfWork> _uow;

        [TestInitialize]
        public void Init()
        {
            _uow = new Mock<IUnitOfWork>();
            
            var photoService = new PhotoService(_uow.Object);
            var commentService = new CommentService(_uow.Object);            
            var context = new FakeUserContext();

            var mockWebSecurityService = new Mock<IWebSecurityService>();
            mockWebSecurityService.Setup(m => m.CurrentUserId).Returns(1);           

            _controller = new PhotosController(photoService, mockWebSecurityService.Object, context);

            AutoMapperWebConfiguration.Configure();
        }

        //[TestMethod]
        //[ExpectedException(typeof(HttpResponseException))]
        //public void Photos_Not_Returned_If_No_Event_Passed_Test()
        //{
        //    //Act
        //    _controller.GetForEvent(-1);
        //}
    }
}
