using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using Runniac.BehaviourTests.Pages;

namespace Runniac.BehaviourTests.Common
{
    public class CommonHelper
    {
        private static string _homeUrl = ConfigurationManager.AppSettings["HomeUrl"];
        private static CommonHelper _instance;
        private IWebDriver _driver;

        private CommonHelper()
        {
            _driver = new SeleniumTestBase().StartBrowser();
        }

        public void CleanNavigationData()
        {
            _driver.Manage().Cookies.DeleteAllCookies();
        }

        public void CloseBrowser()
        {
            _driver.Quit();
        }

        public static CommonHelper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CommonHelper();

                return _instance;
            }
        }

        private static Home _Home;

        public Home Home
        {
            get
            {
                if (_Home == null)
                    _Home = new Home(_driver);

                return _Home;
            }
        }

        private static SearchResults _SearchResults;

        public SearchResults SearchResults
        {
            get
            {
                if (_SearchResults == null)
                    _SearchResults = new SearchResults(_driver);

                return _SearchResults;
            }
        }

        private static EventDetails _EventDetails;

        public EventDetails EventDetails
        {
            get
            {
                if (_EventDetails == null)
                    _EventDetails = new EventDetails(_driver);

                return _EventDetails;
            }
        }
        public static string HomeUrl
        {
            get
            {
                return _homeUrl;
            }
        }

    }
}
