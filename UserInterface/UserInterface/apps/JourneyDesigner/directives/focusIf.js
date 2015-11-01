(function (module) {
    module.directive('focusIf', ['$timeout', function ($timeout) {
        return function focusIf(scope, element, attr) {
            scope.$watch(attr.focusIf, function (newVal) {
                if (newVal) {
                    $timeout(function () {
                        element[0].focus();
                    });
                }
            });
        }
    }]);
}(angular.module("microcafe-app")))