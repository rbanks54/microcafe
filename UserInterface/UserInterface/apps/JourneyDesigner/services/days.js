(function (masterData) {

    var dayService = function ($http, alerting) {

        var serviceBase = '/api/days';
        var obj = {};

        obj.getDay = function (dayId, successCallback, errorCallback) {
            return $http.get(serviceBase + '/' + dayId)
                .then(successCallback, errorCallback)
                .catch(alerting.errorHandler('API Error', "Failed to find Day."));
        };
        

        obj.updateDay = function (dayId, day, successCallback, errorCallback) {
            return $http.put(serviceBase + "/" + dayId, day)
                .then(successCallback, errorCallback)
                .catch(alerting.errorHandler('API Error', "Failed to Update Day."));
        };

        return obj;

    };


    masterData.factory("dayService", dayService);

}(angular.module("journeyDesigner-app")))