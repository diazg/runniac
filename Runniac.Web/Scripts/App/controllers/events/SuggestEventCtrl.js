(function () {
    "use strict";

    angular.module("runniac").controller("SuggestEventCtrl", ["$scope", "$rootScope", "$window", "Event", "EventTypes", "User", "Message",
        function ($scope, $rootScope, $window, Event, EventTypes, User, Message) {
            $scope.distanceNames = EventTypes.get;
            $scope.suggestMode = true;

            User.isUserLoggedIn(
                function success(response) {
                    $scope.isUserLoggedIn = response.result;
                });

            $scope.saveEvent = function (valid) {
                $scope.createEventForm.submitted = false;

                if (valid) {
                    //Los eventos enviados por los usuarios no se publican automáticamente
                    $scope.event.Published = false;

                    Event.saveEvent($scope.event,
                    function success(event) {
                        $rootScope.$emit("uploadCourse", event.Id);
                        toastr.success("Evento enviado correctamente");
                        Message.setMessage("Hemos recibido tu evento correctamente. En las próximas horas lo revisaremos y estudiaremos su publicación en nuestro buscador");
                        $window.location = "#/success";
                    },
                    function err() {
                        toastr.error("La operacion no se ha podido realizar. Vuelva a intentarlo más tarde", "Error");
                    });
                } else {
                    $scope.createEventForm.submitted = true;
                }
            };
        }]);
})();