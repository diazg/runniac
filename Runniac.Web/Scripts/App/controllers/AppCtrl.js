(function () {
    "use strict";

    angular.module('runniac').controller('AppCtrl', ['$scope', '$location', '$window', 'Page', 'Message',
        function ($scope, $location, $window, Page, Message) {

            $scope.Page = Page;
            Page.setTitle("La red social para runners");
            $scope.Message = Message;

            $scope.$on('$viewContentLoaded', function (event) {
                $window._gaq.push(['_trackPageview', $location.path()]);
            });

            $scope.$back = function () {
                window.history.back();
            };

            $scope.location = $location;

        }]);
})();