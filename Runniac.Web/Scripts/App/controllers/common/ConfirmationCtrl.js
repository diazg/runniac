angular.module('runniac').controller('ConfirmationController', ['$scope', '$modalInstance', 'data',
    function ($scope, $modalInstance, data) {

        $scope.dialogData = data;

        $scope.ok = function () {
            $modalInstance.close();
        };

        $scope.close = function () {
            $modalInstance.close();
        };

        $scope.cancel = function () {
            $modalInstance.dismiss();
        };        
    }
]);