(function () {
    "use strict";

    angular.module('runniac').controller('LandingCtrl', ['$scope', '$css', '$location', 'anchorSmoothScroll', 'geolocation', 'Event',
        '$timeout',
        function ($scope, $css, $location, anchorSmoothScroll, geolocation, Event, $timeout) {
            $css.bind({
                href: '/Content/grayscale.css'
            }, $scope);            

            geolocation.getLocation().then(function (data) {
                $scope.coords = { lat: data.coords.latitude, long: data.coords.longitude };

                Event.getClosest({ lat: $scope.coords.lat, lon: $scope.coords.long },
                            function success(events) {
                                $scope.topClosestEvents = events;
                            });
            });

            Event.getTopRated(function success(events) {
                $scope.topRatedEvents = events;                
            });

            $timeout(function () {
                if (($(window).height() + 100) < $(document).height()) {
                    $('#top-link-block').removeClass('hidden').affix({
                        // how far to scroll down before link "slides" into view
                        offset: { top: 100 }
                    });
                }
            }, 1000);
        }]);
})();