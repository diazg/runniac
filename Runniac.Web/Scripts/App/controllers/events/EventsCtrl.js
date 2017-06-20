(function () {
    "use strict";

    angular.module("runniac").controller("EventsCtrl", ["$scope", "$rootScope", "$filter", "$window", "$routeParams", "$http",
        "$modal", "Event", "EventTypes",
        function ($scope, $rootScope, $filter, $window, $routeParams, $http, $modal, Event, EventTypes) {

            $scope.events = Event.query();
            $scope.distanceNames = EventTypes.get;
            $scope.editMode = false;

            $scope.filterOptions = {
                filterText: ''
            };
            $scope.totalServerItems = 0;
            $scope.pagingOptions = {
                pageSizes: [15],
                pageSize: 15,
                currentPage: 1
            };

            //Para que se muestren caracteres con acentos y demás particularidades
            var cellTextTemplate = '<div class="ngCellText"><span ng-bind-html="row.entity[col.field]"></span></div>';
            var cellCommandsTemplate = '<a href="#/editEvent/{{row.entity.Id}}" class="btn btn-success btn-xs">' +
                            '<span class="glyphicon glyphicon-edit"></span></a>' +
                            '<button type="button" class="btn btn-danger btn-xs" ng-click="deleteEvent(row.entity)">' +
                            '<span class="glyphicon glyphicon-trash"></span></button>';
            var cellCheckBoxTemplate = '<div class="text-center ngCellText"><input type="checkbox" ng-model="row.entity.Published"' +
                'ng-click="publishEvent(row.entity)"/></div>';

            $scope.gridOptions = {
                data: 'events',
                columnDefs: [
                    { field: '', width: '62px', cellTemplate: cellCommandsTemplate },
                    { field: 'Name', displayName: 'Nombre', width: '350px', cellTemplate: cellTextTemplate },
                    { field: 'EventDate', displayName: 'Fecha' },
                    { field: 'Location', displayName: 'Lugar', cellTemplate: cellTextTemplate },
                    { field: 'DistanceKms', displayName: 'Distancia' },
                    { field: 'Published', width: '100px', displayName: 'Publicada', enableCellEdit: true, cellTemplate: cellCheckBoxTemplate }
                ],
                showFilter: true,
                enablePaging: true,
                pagingOptions: $scope.pagingOptions,
                showFooter: true,
                totalServerItems: 'totalServerItems',
                plugins: [new ngGridFlexibleHeightPlugin()]
            };

            $scope.setPagingData = function (data, page, pageSize) {
                var pagedData = data.slice((page - 1) * pageSize, page * pageSize);
                $scope.events = pagedData;
                $scope.totalServerItems = data.length;
                if (!$scope.$$phase) {
                    $scope.$apply();
                }
            };

            $scope.getPagedDataAsync = function (pageSize, page, searchText) {
                setTimeout(function () {
                    var data;
                    if (searchText) {
                        var ft = searchText.toLowerCase();
                        Event.query({},
                            function success(largeLoad) {
                                data = largeLoad.filter(function (item) {
                                    return JSON.stringify(item).toLowerCase().indexOf(ft) != -1;
                                });
                                $scope.setPagingData(data, page, pageSize);
                            });
                    } else {
                        Event.query({},
                            function success(largeLoad) {
                                $scope.setPagingData(largeLoad, page, pageSize);
                            });
                    }
                }, 100);
            };

            $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage);

            $scope.$watch('pagingOptions', function (newVal, oldVal) {
                if (newVal !== oldVal && newVal.currentPage !== oldVal.currentPage) {
                    $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.filterOptions.filterText);
                }
            }, true);
            $scope.$watch('filterOptions', function (newVal, oldVal) {
                if (newVal !== oldVal) {
                    $scope.getPagedDataAsync($scope.pagingOptions.pageSize, $scope.pagingOptions.currentPage, $scope.filterOptions.filterText);
                }
            }, true);


            //Si entramos en modo edición
            if ($routeParams.eventId) {
                $scope.event = Event.get({ id: $routeParams.eventId },
                    function () {
                        if ($scope.event.CourseFileName != null)
                            $rootScope.$emit("showCourseOnMap", $scope.event.CourseFileName);
                    });
                $scope.editMode = true;
            }

            $scope.saveEvent = function (valid) {
                $scope.createEventForm.submitted = false;

                if (valid) {
                    if ($scope.editMode) {
                        Event.update($scope.event,
                            function () {
                                $rootScope.$emit("uploadCourse", $scope.event.Id);
                                toastr.success("Evento actualizado correctamente");
                                $window.location = "#/listEvents"
                            });
                    } else {
                        Event.saveEvent($scope.event,
                        function success() {
                            $rootScope.$emit("uploadCourse", $scope.event.Id);
                            toastr.success("Evento insertado correctamente");
                            $window.location = "#/listEvents"
                        },
                        function err() {
                            toastr.error("La operacion no se ha podido realizar. Vuelva a intentarlo más tarde", "Error");
                        });
                    }
                } else {
                    $scope.createEventForm.submitted = true;
                }
            };



            $scope.selectAll = function () {
                angular.forEach($scope.eventsToImport, function (data, index) {
                    data.checked = true;
                });
            };

            $scope.unselectAll = function () {
                angular.forEach($scope.eventsToImport, function (data, index) {
                    data.checked = false;
                });
            };

            $scope.searchEvents = function () {
                $scope.searchFinished = false;

                $scope.eventsToImport = Event.getExternal({ extractor: $scope.extractor },
                    function success() {
                        toastr.success("Se han encontrado " + $scope.eventsToImport.length + " carreras para importar");
                        $scope.searchFinished = true;
                    },
                    function err() {
                        toastr.error("La operacion no se ha podido realizar. Vuelva a intentarlo más tarde", "Error");
                        $scope.searchFinished = true;
                    });
            };

            $scope.selectedEvents = function () {
                $scope.selectedItems = $filter('filter')($scope.eventsToImport, { checked: true });
            }

            $scope.importEvents = function () {
                Event.importEvents({ events: $scope.selectedItems, extractor: $scope.extractor },
                    function success() {
                        toastr.success("Se han importado " + $scope.selectedItems.length + " carreras", "Importación finalizada");
                    },
                    function err() {
                        toastr.error("La operacion no se ha podido realizar. Vuelva a intentarlo más tarde", "Error");
                    });
            };

            $scope.deleteEvent = function (event) {
                $modal.open({
                    templateUrl: '/Scripts/App/templates/utils/confirmation.html',
                    backdrop: 'static',
                    keyboard: false,
                    resolve: {
                        data: function () {
                            return {
                                title: 'Borrar evento',
                                message: '¿Seguro que desea borrar el evento?'
                            };
                        }
                    },
                    controller: 'ConfirmationController'
                }).result.then(function (result) {
                    Event.delete({ id: event.Id },
                    function success() {
                        toastr.success("Evento borrado correctamente");
                        $scope.events.splice($scope.events.indexOf(event), 1);
                        $scope.totalServerItems = $scope.events.length;
                    },
                    function err() {
                        toastr.error("El evento no se ha podido borrar. Vuelva a intentarlo más tarde", "Error");
                    });
                });
            };

            $scope.publishEvent = function (event) {
                event.Published = !event.Published;
                Event.saveEvent(event,
                function success() {
                    if (event.Published)
                        toastr.success("Evento publicado correctamente");
                    else
                        toastr.success("Publicación cancelada correctamente");
                },
                function err() {
                    toastr.error("La operacion no se ha podido realizar. Vuelva a intentarlo más tarde", "Error");
                });
            };

            $scope.showEventDetails = function (event) {
                $modal.open({
                    templateUrl: '/Scripts/App/templates/events/eventDetails.html',
                    backdrop: 'static',
                    resolve: {
                        data: function () {
                            return {
                                event: event,
                                extractor: $scope.extractor
                            }
                        }
                    },
                    controller: 'ShowMoreInfoCtrl'
                })
            };

            $scope.getTowns = function (substring) {
                return $http.get('/api/towns/search', { params: { query: substring } }).then(function (res) {
                    var towns = [];
                    angular.forEach(res.data, function (item) {
                        towns.push(item);
                    });
                    return towns;
                });
            };
        }]);
})();