using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runniac.BehaviourTests.Pages
{
    public class EventDetails : BasePage
    {
        public EventDetails(IWebDriver driver) : base(driver) { }

        internal void CheckImInAnEventDetailsPage()
        {
            Assert.IsTrue(_driver.Url.Contains("eventDetail"));
        }

        internal void ClickLoginLink()
        {
            WaitForElementByCssSelector("#loginToComment");
            _driver.FindElement(By.CssSelector("#loginToComment a")).Click();
        }

        internal void EnterCredentials(string username, string password)
        {
            _driver.FindElement(By.Id("UserName")).SendKeys(username);
            _driver.FindElement(By.Id("Password")).SendKeys(password);
            _driver.FindElement(By.CssSelector("input[type=submit]")).Click();
        }

        internal void ClickCommentButton()
        {
            WaitForElementByCssSelector("#btnWriteComment");
            _driver.FindElement(By.Id("btnWriteComment")).Click();
        }

        internal void CreateComment(string title, string commentText)
        {
            WaitForElementByCssSelector(".modal-body span i");
            _driver.FindElements(By.CssSelector(".modal-body span i"))[5].Click();
            _driver.FindElement(By.Id("title")).SendKeys(title);
            _driver.FindElement(By.Id("commentText")).SendKeys(commentText);
            _driver.FindElements(By.CssSelector(".modal-footer button.btn-primary"))[1].Click();
        }

        internal void CheckCommentIsDisplayed(string title)
        {
            var commentSelector = String.Format("//div[@id='commentsList']//p[contains(@class, 'commentTitle')]/em[contains(., '{0}')]", title);
            WaitForElementByXpath(commentSelector);
            var comments = _driver.FindElements(By.XPath(commentSelector));

            Assert.IsTrue(comments.Count > 0);
        }
    }
}
