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
using AutoMapper;
using System.Collections.Generic;
using Runniac.Tests.Mocks;

namespace Runniac.Tests
{
    [TestClass]
    public class EventsControllerTest
    {
        EventsController _controller;        
        Mock<IUnitOfWork> _uow;
        

        [TestInitialize]
        public void Init()
        {            
            _uow = MockingFactory.SetupUowMock();            
            
            var commentService = new CommentService(_uow.Object);
            var eventService = new EventService(_uow.Object, commentService);
            var townService = new TownService(_uow.Object);
            var photoService = new PhotoService(_uow.Object);
            var userService = new UserService(_uow.Object, commentService, photoService);
            var eventsExtractor = MockingFactory.SetupEventsExtractorMock();
            _controller = new EventsController(eventService, commentService, townService, userService, eventsExtractor.Object);

            AutoMapperWebConfiguration.Configure();
        }
        

        [TestMethod]
        public void Save_Event_Test()
        {
            //Arrange
            var eventObj = new EventVM();

            //Act
            _controller.PostEvent(eventObj);

            //Assert            
            _uow.Verify(r => r.Save(), Times.AtMostOnce());
        }

        [TestMethod]
        public void Delete_Event_Test()
        {
            //Arrange
            var eventId = 1;

            //Act
            _controller.Delete(eventId);

            //Assert
            _uow.Verify(r => r.Save(), Times.AtMostOnce());
        }

        [TestMethod]
        public void Import_Events_Test()
        {
            //Arrange
            var eventsVM = Mapper.Map<IEnumerable<Event>, IEnumerable<EventVM>>(FakeData.GetEvents().AsEnumerable());

            //Act
            _controller.ImportEvents(new ImportEventsVM { Events = eventsVM, Extractor = "todocarreras" });

            //Assert
            _uow.Verify(r => r.Save(), Times.Exactly(eventsVM.Count()));            
        }


        [TestMethod]
        public void Get_Test()
        {
            //Arrange
            var eventId = 1;

            //Act
            var eventVM = _controller.Get(eventId);

            //Assert
            Assert.IsTrue(eventVM.Id == eventId);
        }



        [TestMethod]
        public void List_All_Events_Test()
        {
            //Act
            var events = _controller.Get();

            //Assert            
            Assert.IsTrue(events.Count() == 5);
        }

        [TestMethod]
        public void List_Events_With_Distance_Test()
        {
            //Act
            var events = _controller.Get();

            //Assert
            Assert.IsTrue(events.Where(e => e.Type == "Marathon").Count() == 2);
        }

        [TestMethod]
        public void Search_Events_Without_Params_Test()
        {
            //Act
            var events = _controller.SearchEvents("", "", "");

            //Assert
            Assert.IsTrue(events.Count() == 5);
        }

        [TestMethod]
        public void Get_Events_Locations_Test()
        {
            //Act
            var locations = _controller.GetDifferentLocations("test");

            //Assert
            Assert.IsTrue(locations.Count() == 4);
        }
    }
}
