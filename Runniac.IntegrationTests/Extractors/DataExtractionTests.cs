using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Runniac.ExternalDataExtraction;
using Runniac.Web.Controllers;
using Runniac.Web.App_Start;

namespace Runniac.IntegrationTests
{
    [TestClass]
    public class DataExtractionTests
    {
        IMultiExtractor _extractor;
        EventsController _controller;
        private const string FED_NAV_EXTRACTOR_NAME = "fedNav";
        private const string TODOCARRERAS_EXTRACTOR_NAME = "todoCarreras";
        private const string VAMOSACORRER_EXTRACTOR_NAME = "vamosACorrer";

        [TestInitialize]
        public void Init()
        {
            _extractor = new EventsMultiSourceExtractor();
            _controller = new EventsController(null, null, null, null, _extractor);
            AutoMapperWebConfiguration.Configure();
        }

        [TestMethod]
        [TestCategory("ExternalEventsExtraction")]
        public void Connection_To_FederacionNavarra_Test()
        {
            ConnectToExternalSource(FED_NAV_EXTRACTOR_NAME);
        }

        [TestMethod]
        [TestCategory("ExternalEventsExtraction")]
        public void Connection_To_TodoCarreras_Test()
        {
            ConnectToExternalSource(TODOCARRERAS_EXTRACTOR_NAME);
        }

        [TestMethod]
        [TestCategory("ExternalEventsExtraction")]
        public void Connection_To_VamosACorrer_Test()
        {
            ConnectToExternalSource(VAMOSACORRER_EXTRACTOR_NAME);
        }

        [TestMethod]
        [TestCategory("ExternalEventsExtraction")]
        public void Connection_To_Fed_Navarra_Filling_Parameters_Test()
        {
            Check_Events_Have_Attributes_Filled(FED_NAV_EXTRACTOR_NAME);
        }

        [TestMethod]
        [TestCategory("ExternalEventsExtraction")]
        public void Connection_To_TodoCarreras_Filling_Parameters_Test()
        {
            Check_Events_Have_Attributes_Filled(TODOCARRERAS_EXTRACTOR_NAME);
        }

        [TestMethod]
        [TestCategory("ExternalEventsExtraction")]
        public void Connection_To_TodoCarreras_Returns_Results_Test()
        {
            var events = _controller.GetEventsFromExternalSource(TODOCARRERAS_EXTRACTOR_NAME);

            Assert.IsTrue(events.Count() > 0);
        }

        [TestMethod]
        [TestCategory("ExternalEventsExtraction")]
        public void Connection_To_VamosACorrer_Returns_Results_Test()
        {
            var events = _controller.GetEventsFromExternalSource(VAMOSACORRER_EXTRACTOR_NAME);

            Assert.IsTrue(events.Count() > 0);
        }

        private void Check_Events_Have_Attributes_Filled(string extractorParams)
        {
            var events = _controller.GetEventsFromExternalSource(extractorParams).ToList();

            Assert.IsFalse(String.IsNullOrEmpty(events[3].Name));
            Assert.IsFalse(String.IsNullOrEmpty(events[5].Location));
            Assert.IsFalse(String.IsNullOrEmpty(events[2].EventDate));
        }

        private void ConnectToExternalSource(string extractorParams)
        {
            var events = _controller.GetEventsFromExternalSource(extractorParams);

            Assert.IsTrue(events.Count() > 0);
        }
    }
}
