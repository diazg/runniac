using Runniac.BehaviourTests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Runniac.BehaviourTests.Steps
{
    [Binding]
    public class HomePageSteps
    {
        [Given(@"I have entered the home page")]
        public void GivenIHaveEnteredTheHomePage()
        {
            CommonHelper.Instance.Home.Visit();
        }

        [When(@"I press search")]
        public void WhenIPressSearch()
        {
            CommonHelper.Instance.Home.ClickSearchButton();
        }

        [When(@"I enter (.*) as location")]
        public void WhenIEnterLocationAsLocation(string location)
        {
            CommonHelper.Instance.Home.EnterLocation(location);
        }

        [When(@"I enter (.*) as event type")]
        public void WhenIEnterEventTypeAsEventType(string eventType)
        {
            CommonHelper.Instance.Home.EnterEventType(eventType);
        }

        [When(@"I enter (.*) as event date")]
        public void WhenIEnterAsEventDate(string date)
        {
            CommonHelper.Instance.Home.EnterEventDate(date);
        }        
    }
}
