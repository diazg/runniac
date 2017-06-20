(function () {
    "use strict";

    angular.module("runniac").controller("PhotoUploaderCtrl", ["$scope", "$upload",
        function ($scope, $upload) {
            $scope.onFileSelect = function ($files) {
                for (var i = 0; i < $files.length; i++) {
                    var file = $files[i];
                    $scope.upload = $upload.upload({
                        url: 'api/photos/upload',
                        method: 'POST',
                        data: { eventId: $scope.event.Id },
                        file: file, // or list of files: $files for html5 only                    
                    }).success(function (data, status, headers, config) {
                        toastr.success("Foto " + config.file.name + " subida correctamente");
                    }).error(function (errorResult) {
                        if (typeof (errorResult) != 'undefined') {
                            toastr.error(errorResult, "Error");
                        } else {
                            toastr.error("La operacion no se ha podido realizar. Vuelva a intentarlo más tarde", "Error");
                        }
                    });
                }
            };
        }]);
})();