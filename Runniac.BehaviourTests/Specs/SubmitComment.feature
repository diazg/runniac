Feature: SubmitComment
	In order to express my opinion abount an event
	As a website user
	I want to submit comments

Scenario Outline: Submit comment
	Given I have entered an event details page
	And I am logged in with <username> and <password>
	When I press the create comment button
	And I enter my comment with <title> and <text>
	Then I expect to see my comment with <title> in the page
Examples: 
	| username                         | password | title                | text                                 |
	| eugenia.perez.martinez@gmail.com | $23_Gx   | Una de mis favoritas | Muy rápida y un recorrido muy bonito |