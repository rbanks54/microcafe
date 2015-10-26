(function (common) {

    var alerting = function ($timeout, $rootScope) {

        var currentAlerts = [];
        var alertTypes = ["success", "info", "warning", "danger"];

        
        // Basic methods for interacting with the current alerts
        var removeAlert = function (alert) {
            for (var i = 0; i < currentAlerts.length; i++) {
                if (currentAlerts[i] === alert) {
                    currentAlerts.splice(i, 1);
                    break;
                }
            }
        };

        var addAlert = function(type, title, message) {
            var alert = { type: type, message: message, title: title };
            currentAlerts.push(alert);

            $rootScope.$broadcast('alertAddedEvent');

            if (type !== 'danger') {
                $timeout(function() {
                    removeAlert(alert);
                }, 5000);
            }
        };

        // Alert type methods
        var addSuccess = function (title, message) {
            addAlert("success", title, message);
        };

        var addWarning = function (title, message) {
            addAlert("warning", title, message);
        };

        var addInformation = function (title, message) {
            addAlert("info", title, message);
        };

        var addDanger = function (title, message) {
            addAlert("danger", title, message);
        };

        var errorHandler = function (title, description) {
            return function () {
                addDanger(title, description);
            };
        };

        // add alerts for $http errorCallback response
        var addResponseError = function (response, title, description) {
            // when not a response for validation errors
            if (!(response.data && angular.isObject(response.data) && response.data.modelState)) {
                if (response.status === 503 && response.data && angular.isString(response.data)) {
                    addDanger(title, response.data); // service unavailable
                }
                else if (description) {
                    addDanger(title, description);
                }
                else if (response.data && angular.isString(response.data)) {
                    addDanger(title, response.data);
                }
            }
        };

        // return array of validation errors throw by ModelState validation in apicontroller $http errorCallback response
        var getValidationErrors = function (response) {
            if (response.data && angular.isObject(response.data) && response.data.modelState) {
                var validationErrors = [];
                for (var key in response.data.modelState) {
                    for (var i = 0; i < response.data.modelState[key].length; i++) {
                        if (validationErrors.indexOf(response.data.modelState[key][i]) === -1) {
                            validationErrors.push(response.data.modelState[key][i]);
                        }
                    }
                }
                return validationErrors;
            }

            return null;
        };
        
        return {
            addSuccess: addSuccess,
            addInformation: addInformation,
            addWarning: addWarning,
            addDanger: addDanger,

            addAlert: addAlert,
            removeAlert: removeAlert,
            errorHandler: errorHandler,
            currentAlerts: currentAlerts,

            addResponseError: addResponseError,
            getValidationErrors: getValidationErrors
        };

    };

    common.factory("alerting", alerting);

}(angular.module("common")))