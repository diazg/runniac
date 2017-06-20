angular.module('runniac').controller('CreateCommentCtrl', ['$scope', '$modalInstance',
    function ($scope, $modalInstance) {
        $scope.rate = 0;
        $scope.overStar = 0;

        $scope.cancel = function () {
            $modalInstance.dismiss();
        };

        $scope.submitComment = function (valid) {
            $scope.submitted = false;

            if (valid) {
                var comment = {
                    Title: this.commentTitle,
                    Text: this.commentText,
                    Rating: this.rate
                };
                $modalInstance.close(comment);
            }
            else
                $scope.submitted = true;
        };

        $scope.hoveringOver = function (value) {
            $scope.overStar = value;
            $scope.percent = 100 * (value / 10);
        };

        $scope.cleanOverStar = function () {
            $scope.overStar = 0;
        };

    }
]);