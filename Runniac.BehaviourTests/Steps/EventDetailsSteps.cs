using Runniac.BehaviourTests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Runniac.BehaviourTests.Steps
{
    [Binding]
    public class EventDetailsSteps
    {
        [Given(@"I have entered an event details page")]
        public void GivenIHaveEnteredAnEventDetailsPage()
        {
            CommonHelper.Instance.Home.Visit();
            CommonHelper.Instance.Home.ClickSearchButton();
            CommonHelper.Instance.SearchResults.ClickOnAResult();
        }

        [Given(@"I am logged in with (.*) and (.*)")]
        public void GivenIAmLoggedIn(string username, string password)
        {
            CommonHelper.Instance.EventDetails.ClickLoginLink();
            CommonHelper.Instance.EventDetails.EnterCredentials(username, password);
        }

        [When(@"I press the create comment button")]
        public void WhenIPressTheCreateCommentButton()
        {
            CommonHelper.Instance.EventDetails.ClickCommentButton();
        }

        [When(@"I enter my comment with (.*) and (.*)")]
        public void WhenIEnterMyComment(string title, string commentText)
        {
            CommonHelper.Instance.EventDetails.CreateComment(title, commentText);
        }

        [Then(@"I expect to see my comment with (.*) in the page")]
        public void ThenIExpectToSeeMyCommentInThePage(string title)
        {
            CommonHelper.Instance.EventDetails.CheckCommentIsDisplayed(title);
        }

        [Then(@"I expect to see a page with more info")]
        public void ThenIExpectToSeeAPageWithMoreInfo()
        {
            CommonHelper.Instance.EventDetails.CheckImInAnEventDetailsPage();
        }

    }
}
