(function (masterData) {

    var itineraryService = function ($http, alerting) {

        var serviceBase = '/api/itineraries';
        var emptyGuid = "00000000-0000-0000-0000-000000000000";
        var obj = {};

        obj.getItineraries = function () {
            return $http.get(serviceBase)
                .then(function (results) {
                    return results.data;
                })
                .catch(alerting.errorHandler('API Error!', "Failed to retrieve the list of Itinerarys."));
        };

        obj.getItinerary = function (itinereryId) {
            return $http.get(serviceBase + '/' + itinereryId);
        };
        
        obj.insertItinerary = function (itinerary, successCallback, errorCallback) {
            return $http.post(serviceBase, itinerary)
                .then(successCallback, errorCallback)
                .catch(alerting.errorHandler('API Error', "Failed to insert a new Itinerary."));
        };

        obj.updateItinerary = function (itineraryId, itinerary, successCallback, errorCallback) {
            return $http.put(serviceBase + "/" + itineraryId, itinerary)
                .then(successCallback, errorCallback)
                .catch(alerting.errorHandler('API Error', "Failed to Update Itinerary."));
        };

        obj.deleteItinerary = function (itinereryId, itinerary) {
            return $http.put(serviceBase + '/' + itinereryId + "/delete", itinerary)
                .then(function (status) {
                    return status.data;
                })
                .catch(alerting.errorHandler('API Error!', "Failed to delete an existing Itinerary."));
        };

        obj.insertItineraryDay = function (itinerary, day) {
            var addMessage = {
                "version": itinerary.version,
                "description": day.description
            };
            return $http.post(serviceBase + '/' + itinerary.id + "/addday", addMessage);
        };

        obj.reorderItineraryDays = function (itinerary) {
            var reorderMessage = {
                "version": itinerary.version,
                "newOrder": itinerary.itineraryDayIds
            };
            return $http.put(serviceBase + '/' + itinerary.id + "/reorderdays", reorderMessage);
        };

        obj.removeDayFromItinerary = function (itinerary, day, successCallback, errorCallback) {
            var removeDayMessage = {
                "RemovedDayId": day.id,
                "version": itinerary.version
            };

            return $http.put(serviceBase + "/" + itinerary.id + "/removeday", removeDayMessage)
                .then(successCallback, errorCallback)
                .catch(alerting.errorHandler('API Error', "Failed to remove day from Itinerary."));
        };

        obj.newItinerary = function () {
            return {
                "data": {
                    "id": "00000000-0000-0000-0000-000000000000",
                    "name": "",
                    "seasonId": "00000000-0000-0000-0000-000000000000",
                    "productId": "00000000-0000-0000-0000-000000000000",
                    "tourcodeId": "00000000-0000-0000-0000-000000000000",
                    "ownerId": "00000000-0000-0000-0000-000000000000",
                    "brandId": "00000000-0000-0000-0000-000000000000",
                    "operatorId": "00000000-0000-0000-0000-000000000000",
                    "version": 0,
                    "displayName": "",
                    "isDeleted": false,
                    "brand": {
                        "id": "00000000-0000-0000-0000-000000000000",
                        "code": "",
                        "name": "",
                        "displayName": ""
                    },
                    "operator": {
                        "id": "00000000-0000-0000-0000-000000000000",
                        "code": "",
                        "description": "",
                        "isArchived": false,
                        "displayName": ""
                    },
                    "season": {
                        "id": "Scenic Australia (STAU)",
                        "name": "",
                        "version": 0,
                        "displayName": ""
                    },
                    "product": {
                        "id": "Scenic Australia (STAU)",
                        "code": "",
                        "name": "",
                        "description": null,
                        "productType": 1,
                        "version": 0,
                        "displayName": "",
                        "productTypeDisplayName": ""
                    }
                }
            };
        };

        return obj;

    };

    var confirmItineraryDayAction = function ($modal) {
        return function (day, action) {
            var options = {
                templateUrl: appHelper.journeyDesignerTemplatePath("itineraries/confirmItineraryDayAction"),
                controller: function () {
                    this.day = day;
                    this.action = action;
                },
                controllerAs: "model"
            };

            return $modal.open(options).result;
        };
    };

    var confirmItineraryAction = function ($modal) {
        return function (itinerary, action) {
            var options = {
                templateUrl: appHelper.journeyDesignerTemplatePath("itineraries/confirmItineraryAction"),
                controller: function () {
                    this.itinerary = itinerary;
                    this.action = action;
                },
                controllerAs: "model"
            };

            return $modal.open(options).result;
        };
    };

    var itineraryEditReferenceData = function ($http, alerting) {
        var obj = {};

        obj.getData = function () {
            var serviceUrl = '/api/referenceData/itineraryedit';
            return $http.get(serviceUrl)
                .then(function (results) {
                    return results.data;
                })
                .catch(alerting.errorHandler('API Error!', "Failed to retrieve the list of Itinerarys."));
        };

        return obj;
    };
    
    masterData.factory("confirmItineraryDayAction", confirmItineraryDayAction);
    masterData.factory("confirmItineraryAction", confirmItineraryAction);
    masterData.factory("itineraryService", itineraryService);
    masterData.factory("itineraryEditReferenceData", itineraryEditReferenceData);

}(angular.module("journeyDesigner-app")))