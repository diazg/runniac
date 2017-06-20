using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Runniac.BehaviourTests.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver _driver;

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void WaitForElementByCssSelector(string cssSelector)
        {
            WaitForElement(By.CssSelector(cssSelector));            
        }

        public void WaitForElementByXpath(string xpath)
        {
            WaitForElement(By.XPath(xpath));
        }

        private void WaitForElement(By byClause)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            IWebElement title = wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(byClause);
            });
        }
    }
}
