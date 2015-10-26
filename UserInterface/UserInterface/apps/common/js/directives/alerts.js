(function (module) {

    var alerts = function (alerting) {
        return {
            restrict: "AE",
            templateUrl: "/apps/common/templates/alerts.html",
            scope: true,
            controller: function ($scope) {
                $scope.removeAlert = function (alert) {
                    alerting.removeAlert(alert);
                };
            },
            link: function (scope) {
                scope.currentAlerts = alerting.currentAlerts;
            }
        };
    };

    module.directive("alerts", alerts);

}(angular.module("common")));