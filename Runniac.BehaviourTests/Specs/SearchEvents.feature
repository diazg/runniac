Feature: SearchEvents
	In order to search for events
	As a website user
	I want to be given the possibility of performing a filter to get results


Scenario: Search events without parameters
	Given I have entered the home page	
	When I press search
	Then I expect to see results
	
Scenario Outline: Search events by location
	Given I have entered the home page	
	When I enter <location> as location
	And I press search
	Then I expect to see only results for the location <location>
Examples:
		|location		|
		|Pamplona		|
		|Ribaforada		|	
		
Scenario Outline: Search events by type
	Given I have entered the home page	
	When I enter <event_type> as event type
	And I press search
	Then I expect to see only results for the type <event_type>
Examples:
		|event_type		|
		|Medio maratón	|
		|Maratón		|	

Scenario Outline: Search events by date
	Given I have entered the home page	
	When I enter <date> as event date
	And I press search
	Then I expect to see only results between today and <date>
Examples:
		|date		|
		|01/08/2014	|
		|30/09/2014	|	

