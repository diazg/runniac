(function () {
    "use strict";

    angular.module("runniac").controller("UsersCtrl", ["$scope", "$filter", "$window", "$routeParams", "$modal", "User",
        function ($scope, $filter, $window, $routeParams, $modal, User) {

            $scope.users = User.query();

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
            var cellCommandsTemplate = '<button type="button" class="btn btn-danger btn-xs" ng-click="deleteUser(row.entity)">' +
                            '<span class="glyphicon glyphicon-trash"></span></button>';

            $scope.gridOptions = {
                data: 'users',
                columnDefs: [
                    { field: '', width: '31px', cellTemplate: cellCommandsTemplate },
                    { field: 'Email', displayName: 'Email' },
                    { field: 'Name', displayName: 'Nombre', cellTemplate: cellTextTemplate },
                    { field: 'Lastname', displayName: 'Apellidos', cellTemplate: cellTextTemplate },
                    { field: 'Points', displayName: 'Puntuacion' },
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
                        User.query({},
                            function success(largeLoad) {
                                data = largeLoad.filter(function (item) {
                                    return JSON.stringify(item).toLowerCase().indexOf(ft) != -1;
                                });
                                $scope.setPagingData(data, page, pageSize);
                            });
                    } else {
                        User.query({},
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


            $scope.deleteUser = function (user) {
                $modal.open({
                    templateUrl: '/Scripts/App/templates/utils/confirmation.html',
                    backdrop: 'static',
                    keyboard: false,
                    resolve: {
                        data: function () {
                            return {
                                title: 'Borrar usuario',
                                message: '¿Seguro que desea borrar el usuario? Sus comentarios también serán borrados.'
                            };
                        }
                    },
                    controller: 'ConfirmationController'
                }).result.then(function (result) {
                    User.delete({ id: user.Id },
                    function success() {
                        toastr.success("Usuario borrado correctamente");
                        $scope.users.splice($scope.users.indexOf(user), 1);
                        $scope.totalServerItems = $scope.users.length;
                    },
                    function err() {
                        toastr.error("El usuario no se ha podido borrar. Vuelva a intentarlo más tarde", "Error");
                    });
                });
            };
        }]);
})();