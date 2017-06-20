(function () {
    "use strict";

    angular.module("runniac").controller("VotesCtrl", ["$scope", "Comment", "User",
    function ($scope, Comment, User) {

        $scope.upVote = function (comment) {
            var vote = { CommentId: comment.Id, Positive: true };
            Comment.submitVote(vote,
                function (successResult) {
                    toastr.success("Voto enviado correctamente");
                    comment.Ranking++;
                }, function (errorResult) {
                    if (errorResult.status == 406) {
                        toastr.warning("Ya has votado este comentario. Sólo se permite votar una vez cada comentario", "Voto duplicado");
                    } else if (errorResult.status == 403) {
                        toastr.warning("No puedes votar tus propios comentarios", "Voto no permitido");
                    } else {
                        toastr.error("No se ha podido enviar su voto. Vuelva a intentarlo más tarde", "Error");
                    }
                });
        };

        $scope.downVote = function (comment) {
            var vote = { CommentId: comment.Id, Positive: false };
            Comment.submitVote(vote,
                function (successResult) {
                    toastr.success("Voto enviado correctamente");
                    comment.Ranking--;
                }, function (errorResult) {
                    if (errorResult.status == 406) {
                        toastr.warning("Ya has votado este comentario. Sólo se permite votar una vez cada comentario", "Voto duplicado");
                    } else if (errorResult.status == 403) {
                        toastr.warning("No puedes votar tus propios comentarios", "Voto no permitido");
                    } else {
                        toastr.error("No se ha podido enviar su voto. Vuelva a intentarlo más tarde", "Error");
                    }
                });
        };
    }]);
})();