using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Runniac.BehaviourTests.Common;

namespace Runniac.BehaviourTests.Pages
{
    public class Home : BasePage
    {
        public Home(IWebDriver driver) : base(driver) { }

        internal void Visit()
        {
            _driver.Url = CommonHelper.HomeUrl;
            _driver.Navigate();
            _driver.Manage().Window.Maximize();
        }

        internal void ClickSearchButton()
        {
            _driver.FindElement(By.CssSelector(".grayContainer form button.btn-black")).Click();
        }        

        internal void EnterLocation(string location)
        {
            var locationInput = _driver.FindElements(By.CssSelector(".grayContainer form input[type=\"text\"]")).FirstOrDefault();

            if (locationInput != null)
                locationInput.SendKeys(location);
        }

        internal void EnterEventDate(string date)
        {
            var dateInput = _driver.FindElements(By.CssSelector(".grayContainer form input[type=\"text\"]"))[1];

            if (dateInput != null)
                dateInput.SendKeys(date);
        }

        

        internal void EnterEventType(string eventType)
        {
            SelectElement locationInput = new SelectElement(_driver.FindElement(By.CssSelector(".grayContainer form select.form-control")));

            if (locationInput != null)
                locationInput.SelectByText(eventType);
        }

        
    }
}
