(function () {
    "use strict";

    angular.module("runniac").controller("SearchEventsCtrl", ["$scope", "$http", "$filter", "$window", "$routeParams", "$modal",
        "Event", "EventTypes", "MapUtils", "Town",
        function ($scope, $http, $filter, $window, $routeParams, $modal, Event, EventTypes, MapUtils, $timeout) {
            //Pagination settings
            $scope.pagination = {};
            $scope.pagination.currentPage = 1;
            $scope.pagination.maxSize = 5;
            $scope.pagination.eventsPerPage = 10;


            $scope.$watch('pagination.currentPage + pagination.eventsPerPage', function () {
                paginateResults();
            });

            var paginateResults = function () {
                var begin = (($scope.pagination.currentPage - 1) * $scope.pagination.eventsPerPage);
                var end = begin + $scope.pagination.eventsPerPage;

                if ($scope.events !== undefined)
                    $scope.filteredEvents = $scope.events.slice(begin, end);
            }


            $scope.distanceNames = EventTypes.get;
            $scope.searchParams = [];
            $scope.loading = false;
            $scope.displayList = true;
            $scope.markers = [];

            var cache = $window.sessionStorage.getItem("data");
            var cacheMarkers = $window.sessionStorage.getItem("markers");
            if (cache) {
                $scope.events = JSON.parse(cache);
            }
            if (cacheMarkers) {
                $scope.markers = JSON.parse(cacheMarkers);
            }

            var createMarkersForEvents = function (events) {
                var markers = [];

                angular.forEach(events, function (value, key) {
                    if (value.Town && value.Town.Latitude) {
                        var marker = MapUtils.getMarkerInThisPosition(markers, value.Town.Latitude, value.Town.Longitude);
                        if (!marker) {
                            markers.push(MapUtils.createMarker(value));
                        } else {
                            MapUtils.addPopupContent(marker, value);
                        }
                    }
                });

                return markers;
            };

            //Datepicker configuration logic
            $scope.datePickerMinDate = new Date();

            $scope.dateOptions = {
                'year-format': "'yy'",
                'starting-day': 1
            };

            $scope.getLocations = function (substring) {
                return $http.get('/api/events/getDifferentLocations', { params: { query: substring } }).then(function (res) {
                    var locations = [];
                    angular.forEach(res.data, function (item) {
                        locations.push(item);
                    });
                    return locations;
                });
            };

            $scope.searchEvents = function () {
                $scope.loading = true;
                $scope.events = Event.search(
                    {
                        location: $scope.searchParams.Location,
                        eventType: $scope.searchParams.Type,
                        eventDate: $filter('date')($scope.searchParams.dateTo, $scope.datepickerFormat)
                    },
                    function success(response) {
                        $window.sessionStorage.setItem("data", JSON.stringify($scope.events));
                        toastr.success("Se han encontrado " + $scope.events.length + " carreras");
                        $scope.homePage = false;
                        $window.location = "#/searchResults";

                        if ($scope.pagination.currentPage == 1) {
                            paginateResults();
                        } else {
                            $scope.pagination.currentPage = 1;
                        }

                        $scope.markers = [];
                        $scope.markers = createMarkersForEvents($scope.events);
                        $window.sessionStorage.setItem("markers", JSON.stringify($scope.markers));
                        MapUtils.zoomToMarkers();
                        $scope.loading = false;
                    },
                    function err() {
                        toastr.error("La operacion no se ha podido realizar. Vuelva a intentarlo más tarde", "Error");
                        $scope.loading = false;
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

            $scope.zoomImage = function (imageUrl) {
                $modal.open({
                    templateUrl: '/Scripts/App/templates/events/imageExpanded.html',
                    resolve: {
                        data: function () {
                            return {
                                imageUrl: imageUrl
                            };
                        }
                    },
                    controller: 'ConfirmationController'
                });
            };

            $scope.showList = function () {
                $scope.displayList = true;
            };

            $scope.showMap = function () {
                $scope.displayList = false;
                MapUtils.zoomToMarkers();
            };

            $scope.center = {
                lat: 36.984829500000000000,
                lon: -3.927377799999931000,
                zoom: 5
            };

            $scope.bing = MapUtils.bing;
            $scope.bingBaseLayers = MapUtils.bingBaseLayers;

        }]);
})();