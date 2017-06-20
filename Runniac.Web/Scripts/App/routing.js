(function () {
    "use strict";


    angular.module("runniac").config(["$routeProvider", function ($routeProvider) {
        //ADMIN AREA
        $routeProvider.when('/importEvents', {
            templateUrl: '/Scripts/App/templates/events/importEvents.html',
            controller: 'EventsCtrl'
        });
        $routeProvider.when('/createEvent', {
            templateUrl: '/Scripts/App/templates/events/createEvent.html',
            controller: 'EventsCtrl'
        });
        $routeProvider.when('/editEvent/:eventId', {
            templateUrl: '/Scripts/App/templates/events/createEvent.html',
            controller: 'EventsCtrl'
        });
        $routeProvider.when('/listEvents', {
            templateUrl: '/Scripts/App/templates/events/listEvents.html',
            controller: 'EventsCtrl'
        });
        $routeProvider.when('/listUsers', {
            templateUrl: '/Scripts/App/templates/users/listUsers.html',
            controller: 'UsersCtrl'
        });

        //PUBLIC AREA
        $routeProvider.when('/', {
            templateUrl: '/Scripts/App/templates/search/banner.html',            
        });
        $routeProvider.when('/_=_', {
            templateUrl: '/Scripts/App/templates/search/banner.html',            
        });        
        $routeProvider.when('/searchResults', {
            templateUrl: '/Scripts/App/templates/search/eventsList.html',
            controller: 'SearchEventsCtrl'
        });
        $routeProvider.when('/eventDetail/:eventId', {
            templateUrl: '/Scripts/App/templates/search/eventDetail.html',            
        });
        $routeProvider.when('/suggestEvent', {
            templateUrl: '/Scripts/App/templates/events/suggestEvent.html',            
        });
        $routeProvider.when('/success', {
            templateUrl: '/Scripts/App/templates/utils/success.html'
        });
    }]);

    angular.module("runniac").config(['$compileProvider', function ($compileProvider) {
        $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|file):|mailto|data:image|data.text|javascript:void\//);
    }]);

})();