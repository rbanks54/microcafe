(function (masterData) {

    var operatorService = function ($http, alerting) {

        var serviceBase = '/api/masterdata/operators';
        var emptyGuid = "00000000-0000-0000-0000-000000000000";
        var obj = {};

        obj.getOperators = function (successCallback, errorCallback) {
            return $http.get(serviceBase)
                .then(successCallback, errorCallback)
                .catch(alerting.errorHandler('API Error', "Failed to retrieve the list of Operators."));
        };

        obj.getOperator = function(operatorId) {
            return $http.get(serviceBase + '/' + operatorId);
        };

        obj.insertOperator = function (operator, successCallback, errorCallback) {
            return $http.post(serviceBase, operator)
                .then(successCallback, errorCallback)
                .catch(alerting.errorHandler('API Error', "Failed to insert a new Operator."));
        };

        obj.updateOperator = function (operatorId, operator, successCallback, errorCallback) {
            return $http.put(serviceBase + "/" + operatorId, operator)
                .then(successCallback, errorCallback)
                .catch(alerting.errorHandler('API Error', "Failed to Update Operator."));
        };

        obj.archiveOperator = function (operatorId, operator, successCallback, errorCallback) {
            return $http.put(serviceBase + "/" + operatorId + "/archive", { "archive": true, "version": operator.version })
                .then(successCallback, errorCallback)
                .catch(alerting.errorHandler('API Error', "Failed to Archive Operator."));
        };

        obj.activateOperator = function (operatorId, operator, successCallback, errorCallback) {
            return $http.put(serviceBase + "/" + operatorId + "/archive", { "archive": false, "version": operator.version })
                .then(successCallback, errorCallback)
                .catch(alerting.errorHandler('API Error', "Failed to Activate Operator."));
        };

        obj.deleteOperator = function (operatorId, operator, successCallback, errorCallback) {
            return $http.delete(serviceBase + '/' + operatorId, {
                headers: {
                    'If-Match': function (config) {
                        return '"' + operator.version + '"';
                    }
                }
            })
            .then(successCallback, errorCallback)
            .catch(alerting.errorHandler('API Error', "Failed to Delete Operator."));
        };

        obj.newOperator = function() {
            return { "data": { "id": emptyGuid, "code": "", "description": "" } };
        };

        return obj;

    };

    var confirmOperatorAction = function ($modal) {
        return function (operator, action) {
            var options = {
                templateUrl: appHelper.masterDataTemplatePath("operators/confirmOperatorAction"),
                controller: function () {
                    this.operator = operator;
                    this.action = action;
                },
                controllerAs: "model"
            };

            return $modal.open(options).result;
        };
    };

    masterData.factory("confirmOperatorAction", confirmOperatorAction);
    masterData.factory("operatorService", operatorService);

}(angular.module("journeyDesigner-app")))