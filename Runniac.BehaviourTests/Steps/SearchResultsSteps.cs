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
    public class SearchResultsSteps
    {
        [When(@"I click on a result")]
        public void WhenIClickOnAResult()
        {
            CommonHelper.Instance.SearchResults.ClickOnAResult();
        }

        [Then(@"I expect to see only results for the location (.*)")]
        public void ThenIExpectToSeeOnlyResultsForThatLocation(string location)
        {
            CommonHelper.Instance.SearchResults.CheckOnlyResultsForLocation(location);
        }

        [Then(@"I expect to see only results for the type (.*)")]
        public void ThenIExpectToSeeOnlyResultsForThatEventType(string eventType)
        {
            CommonHelper.Instance.SearchResults.CheckOnlyResultsForEventType(eventType);
        }

        [Then(@"I expect to see results")]
        public void ThenIExpectToSeeResults()
        {
            CommonHelper.Instance.SearchResults.CheckIfResults();
        }

        [Then(@"I expect to see only results between today and (.*)")]
        public void ThenIExpectToSeeOnlyResultsBetweenTodayAnd(string date)
        {
            CommonHelper.Instance.SearchResults.CheckOnlyResultsInDate(date);
        }

    }
}
