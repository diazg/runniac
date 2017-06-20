using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;


namespace Runniac.BehaviourTests
{
    public class SeleniumTestBase
    {
        private FirefoxProfile _ffp;
        private IWebDriver _driver;

        public IWebDriver StartBrowser()
        {
            var navegador = ConfigurationManager.AppSettings["Browser"];

            switch (navegador)
            {
                case "firefox":
                    _ffp = new FirefoxProfile();
                    _ffp.AcceptUntrustedCertificates = true;
                    _driver = new FirefoxDriver(_ffp);
                    break;
                case "iexplorer":
                    _driver = new InternetExplorerDriver();
                    break;
            }

            return _driver;
        }
    }
}
