Feature: EventDetails
	In order to get more information of an event I am interested in
	As a website user
	I want to be redirected to a page with detailed information of events

Scenario: Go to an event details page
	Given I have entered the home page	
	When I press search
	And I click on a result
	Then I expect to see a page with more info
