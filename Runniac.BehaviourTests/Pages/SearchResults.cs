using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.BehaviourTests.Pages
{
    public class SearchResults : BasePage
    {
        public SearchResults(IWebDriver driver) : base(driver) { }

        internal void CheckIfResults()
        {
            WaitForElementByCssSelector(".eventInfo");
            var eventsDisplayed = _driver.FindElements(By.ClassName("eventInfo"));

            Assert.IsTrue(eventsDisplayed.Count > 0);
        }

        internal void CheckOnlyResultsForLocation(string location)
        {
            WaitForElementByCssSelector(".eventInfo");
            var eventsDisplayed = _driver.FindElements(By.ClassName("eventInfo"));

            foreach (var eventInfo in eventsDisplayed)
            {
                var locationName = eventInfo.FindElements(By.CssSelector("ul.list-unstyled li")).
                    First().FindElement(By.TagName("span")).Text;
                Assert.AreEqual(location, locationName);
            }
        }

        internal void CheckOnlyResultsForEventType(string eventType)
        {
            WaitForElementByCssSelector(".eventInfo");
            var eventsDisplayed = _driver.FindElements(By.ClassName("eventInfo"));

            foreach (var eventInfo in eventsDisplayed)
            {
                var type = eventInfo.FindElements(By.CssSelector("ul.list-unstyled li"))[2]
                    .FindElement(By.TagName("span")).Text;
                Assert.AreEqual(type, eventType);
            }
        }

        internal void CheckOnlyResultsInDate(string date)
        {
            WaitForElementByCssSelector(".eventInfo");
            var eventsDisplayed = _driver.FindElements(By.ClassName("eventInfo"));

            foreach (var eventInfo in eventsDisplayed)
            {
                var eventDate = eventInfo.FindElements(By.CssSelector("ul.list-unstyled li"))[1]
                    .FindElement(By.TagName("span")).Text;
                Assert.IsTrue(DateTime.ParseExact(date, "dd/MM/yyyy", null) >= DateTime.ParseExact(eventDate, "dd/MM/yyyy", null));
            }
        }

        internal void ClickOnAResult()
        {
            WaitForElementByCssSelector(".eventInfo");
            var eventsDisplayed = _driver.FindElements(By.ClassName("eventInfo"));

            eventsDisplayed.First().FindElement(By.CssSelector("blockquote a")).Click();
        }
    }
}
