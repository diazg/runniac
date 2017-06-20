angular.module('runniac').controller('ShowMoreInfoCtrl', ['$scope', '$modalInstance', 'data', 'Event',
    function ($scope, $modalInstance, data, Event) {
        var info = data;
        $scope.extraInfo = data.event;
        $scope.loading = true;

        Event.getExtraInfo({ Event: info.event, Extractor: info.extractor },
            function (eventInfo) {
                $scope.extraInfo = eventInfo;
                $scope.loading = false;
            });

        $scope.ok = function () {
            $modalInstance.close();
        };

        $scope.close = function () {
            $modalInstance.dismiss();
        };
    }
]);