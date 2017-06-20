(function () {
    "use strict";

    angular.module("runniac").factory("Event", ["$resource", function ($resource) {
        return $resource("/api/events/:action:id", { id: '@id', action: '@action' },
            {
                getExternal: { method: 'GET', isArray: true, params: { action: 'getExternal' } },
                importEvents: { method: 'POST', params: { action: 'import' } },
                saveEvent: { method: 'POST', params: { action: 'saveEvent' } },
                search: {
                    method: 'GET', isArray: true, params: { action: 'search' },
                    cache: true
                },
                getEventsLocations: { method: 'GET', isArray: true, params: { action: 'getDifferentLocations' } },
                getExtraInfo: { method: 'POST', params: { action: 'getExtraInfo' } },
                update: { method: 'POST', params: { action: 'update' } },
                getClosest: { method: 'GET', isArray: true, params: { action: 'getClosest' } },
                getTopRated: { method: 'GET', isArray: true, params: { action: 'getTopRated' } }
            });
    }]);

    angular.module("runniac").factory("Comment", ["$resource", function ($resource) {
        return $resource("/api/comments/:action:id", { id: '@id', action: '@action' },
            {
                saveComment: { method: 'POST', params: { action: 'saveComment' } },
                getForEvent: { method: 'GET', isArray: true, params: { action: 'getForEvent' } },
                submitVote: { method: 'POST', params: { action: 'submitVote' } }
            });
    }]);

    angular.module("runniac").factory("Photo", ["$resource", function ($resource) {
        return $resource("/api/photos/:action:id", { id: '@id', action: '@action' },
            {
                getForEvent: { method: 'GET', isArray: true, params: { action: 'getForEvent' } }
            });
    }]);

    angular.module("runniac").factory("User", ["$resource", function ($resource) {
        return $resource("/api/users/:action:id", { id: '@id', action: '@action' },
            {
                isUserLoggedIn: { method: 'GET', params: { action: 'isUserLoggedIn' } }
            });
    }]);

    angular.module("runniac").factory("Town", ["$resource", function ($resource) {
        return $resource("/api/towns/:action:id", { id: '@id', action: '@action' },
            {
                getByName: { method: 'GET', params: { action: 'getByName' } }
            });
    }]);

    angular.module("runniac").factory("EventTypes", function () {
        var _eventTypes = ["Km urbano", "Milla", "5 km", "10 km", "Carrera popular", "Medio maratón", "Maratón",
            "Ultramaratón", "100 km", "Carrera de montaña", "Campo a través", "Triatlón"
        ];
        return {
            get: _eventTypes
        }
    });
    
    angular.module("runniac").factory('Page', function () {
        var title = '';
        return {
            title: function () { return title; },
            setTitle: function (newTitle) { title = newTitle; }
        };
    });

    angular.module("runniac").factory('Message', function () {
        var message = '';
        return {
            getMessage: function () { return message; },
            setMessage: function (newMessage) { message = newMessage; }
        };
    });

    angular.module("runniac").factory('notificationFactory', function () {

        return {
            success: function (text) {
                toastr.success(text);
            },
            error: function (text) {
                toastr.error(text, "Error!");
            }
        };
    });
})();