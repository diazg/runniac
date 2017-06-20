(function () {
    "use strict";

    angular.module("runniac").directive('selfNullIfEmpty', [function() {
        return {
            require: 'ngModel',
            link: function(scope, elm, attr, ctrl) {
                ctrl.$parsers.unshift(function(value) {
                    return value === '' ? null : value;
                });
            }
        };
    }]
);
})();