(function () {
    "use strict";

    angular.module("runniac").controller("EventDetailCtrl", ["$scope", "$rootScope", "$routeParams", "$modal", "$location", "Event",
        "Comment", "User", "Photo", "Page", "$timeout",
    function ($scope, $rootScope, $routeParams, $modal, $location, Event, Comment, User, Photo, Page, $timeout) {

        $scope.event = Event.get({ id: $routeParams.eventId },
            function (success) {
                Page.setTitle($scope.event.Name);
                if ($scope.event.CourseFileName != null)
                    $timeout(function () {
                        $rootScope.$broadcast("showCourseOnMap", $scope.event.CourseFileName, 'miniMap');
                    }, 500);;
            });

        User.isUserLoggedIn(
            function success(response) {
                $scope.isUserLoggedIn = response.result;
            });        

        $scope.showCommentDialog = function () {
            $modal.open({
                templateUrl: '/Scripts/App/templates/comments/createComment.html',
                controller: 'CreateCommentCtrl'
            }).result.then(function (comment) {
                save(comment);
            });
        };

        var save = function (comment) {
            $scope.comment = comment;
            $scope.comment.EventId = $scope.event.Id;
            Comment.saveComment($scope.comment,
            function success(response) {
                toastr.success("Comentario enviado correctamente");
                $scope.event.Comments.push(response);
            },
            function err() {
                toastr.error("La operacion no se ha podido realizar. Vuelva a intentarlo más tarde", "Error");
            });
        };

        $scope.showOnMap = function (event) {
            $modal.open({
                templateUrl: '/Scripts/App/templates/events/map.html',
                controller: 'ShowMapCtrl',
                resolve: {
                    event: function () {
                        return event;
                    }
                }
            });
        };

        $scope.expandMap = function (event) {
            $modal.open({
                templateUrl: '/Scripts/App/templates/events/mapExpanded.html',
                controller: 'ExpandMapController',
                windowClass: 'app-modal-window',
                resolve: {
                    event: function () {
                        return event;
                    }
                }
            });
        };
    }]);
})();