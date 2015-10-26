"use strict";

/// <reference path="../../../Scripts/jasmine/jasmine.js" />
/// <reference path="../../../Scripts/angular.js" />
/// <reference path="../../../Scripts/angular-route.js" />
/// <reference path="../../../Scripts/angular-cookies.js" />
/// <reference path="../../../Scripts/angular-resource.js" />
/// <reference path="../../../Scripts/angular-mocks.js" />
/// <reference path="../../../scripts/angular-ui/ui-bootstrap.js" />
/// <reference path="../../../scripts/angular-ui-router.js" />
/// <reference path="../../../scripts/oc-lazyload/ocLazyLoad.js" />
/// <reference path="../../../scripts/xenon/angular-fullscreen.js" />
/// <reference path="../../../Content/js/drop-and-drag/angular-drag-and-drop-lists.min.js" />

/// <reference path="../../../content/js/jquery-1.11.1.min.js" />

/// <reference path="../../../scripts/xenon/xenon-helper.js" />
/// <reference path="../../../scripts/xenon/xenon-custom.js" />

/// <reference path="../../../apps/common/module.js" />
/// <reference path="../../../apps/common/js/services/alerting.js" />

/// <reference path="../../../apps/shell/app-module.js" />
/// <reference path="../../../apps/shell/app-controllers.js" />
/// <reference path="../../../apps/shell/app-directives.js" />
/// <reference path="../../../apps/shell/app-factory.js" />
/// <reference path="../../../apps/shell/app-services.js" />
/// <reference path="../../../apps/shell/app-config.js" />

/// <reference path="../../../apps/JourneyDesigner/directives/focusElement.js.js" />
/// <reference path="../../../apps/JourneyDesigner/services/itineraries.js" />
/// <reference path="../../../apps/JourneyDesigner/controllers/itineraries.js" />

describe("Controller: ItineraryViewCtrl", function () {

    // load the controller's module
    beforeEach(module("journeyDesigner-app"));

    // Local Variables
    var scope;
    var log;

    // This is for the $http interactions
    var q;
    var deferred;

    //
    var itinerariesControllerMock;
    var itineraryServiceMock;
    var alertingMock;
    var itinerariesDataMock;

    // Initialize the mock data and services
    beforeEach(function () {
        itinerariesDataMock = [
            {
                "id": "480884cf-1580-4472-a2cd-62124b3093e0",
                "version": 25,
                "name": "Itinerary 50",
                "seasonId": "905c1e0d-3050-4211-9b89-bedcdf1755de",
                "productId": "5ab1cf24-5d43-444b-b015-ad185f958a92",
                "ownerId": "00000000-0000-0000-0000-000000000000",
                "brandId": "a33bc1ef-95f3-4db8-9ea9-312cfb784f33",
                "operatorId": "288cea67-93d4-460f-9b03-1b583e8a1ebf",
                "regionId": "00000000-0000-0000-0000-000000000000",
                "status": 0,
                "isDeleted": false,
                "itineraryDayIds": [
                    "77a616d0-69b9-47cc-a2e4-4bb8305eeaf8",
                    "255b7ab5-4ae6-47ec-907d-512610f95c70",
                    "c729a5f5-8c03-4a67-a111-9e11040ea215"
                ],
                "brand": {
                    "id": "a33bc1ef-95f3-4db8-9ea9-312cfb784f33",
                    "code": "ST",
                    "name": "Scenic"
                },
                "operator": {
                    "id": "288cea67-93d4-460f-9b03-1b583e8a1ebf",
                    "code": "STAU",
                    "description": "Scenic Australia",
                    "isArchived": false
                },
                "season": {
                    "id": "905c1e0d-3050-4211-9b89-bedcdf1755de",
                    "name": "2018",
                    "version": 0
                },
                "product": {
                    "id": "5ab1cf24-5d43-444b-b015-ad185f958a92",
                    "code": "DGRA",
                    "name": "Grand Australia",
                    "description": null,
                    "version": 0,
                    "productType": 2,
                    "displayName": "Grand Australia (DGRA)",
                    "productTypeDisplayName": "Land Tour"
                },
                "displayName": "Itinerary 50 - DGRA - 2018",
                "itineraryStatusDisplayName": "Open",
                "itineraryDays": [
                    {
                        "id": "77a616d0-69b9-47cc-a2e4-4bb8305eeaf8",
                        "description": "Paris",
                        "version": 0
                    },
                    {
                        "id": "255b7ab5-4ae6-47ec-907d-512610f95c70",
                        "description": "Athens",
                        "version": 0
                    },
                    {
                        "id": "c729a5f5-8c03-4a67-a111-9e11040ea215",
                        "description": "Athens > Nafplion",
                        "version": 0
                    }
                ]
            }
        ];

        itineraryServiceMock = {
            getItineraries: function () {
                deferred = q.defer();
                return deferred.promise;
            },
            getItinerary: function () {
                deferred = q.defer();
                return deferred.promise;
            },
            insertItineraryDay: function () {
                deferred = q.defer();
                return deferred.promise;
            }
        };

        alertingMock = {
            addSuccess: function (settings) {
                return;
            },
            addInformation: function (settings) {
                return;
            },
            addWarning: function (settings) {
                return;
            },
            addDanger: function (settings) {
                return;
            },
            addAlert: function (settings) {
                return;
            },
            removeAlert: function (settings) {
                return;
            },
            errorHandler: function (settings) {
                return;
            },
            currentAlerts: function (settings) {
                return;
            }
        };
    });

    // Initialize the controller and a mock scope
    beforeEach(inject(function ($controller, $rootScope, _$log_, $q) {

        // this is for deferring...
        q = $q;

        // Create a new scope that's a child of the $rootScope
        scope = $rootScope.$new();

        log = _$log_;

        // this is to set up the controller
        //var itinerariesControllerMock = function ($scope, $rootScope, $location, $stateParams, itineraryService, itinerary, alerting)
        itinerariesControllerMock = $controller("itineraryViewCtrl", {
            $scope: scope,
            $log: log,
            itineraryService: itineraryServiceMock,
            itinerary: { "data": itinerariesDataMock[0] },
            //alerting
        });
    }));
    
    it("scope should not be undefined or null", function () {
        expect(scope).toBeDefined();
        expect(scope).not.toBeNull();
    });

    it("scope should call \"setOriginal\" on initialisation", function () {
        spyOn(scope, "setOriginal").and.callThrough();

        scope.initialise();

        expect(scope.itinerary).not.toBeNull();
        expect(scope.original).not.toBeNull();

        expect(scope.setOriginal).toHaveBeenCalled();
    });

    it("should call service \"getItinerary\" on reloadItinerary", function () {
        var emptyGuid = "00000000-0000-0000-0000-000000000000";
        spyOn(itineraryServiceMock, "getItinerary").and.callThrough();

        scope.reloadItinerary(emptyGuid);

        var response = {
            "data": itinerariesDataMock[0]
        };

        deferred.resolve(response);
        scope.$root.$digest();

        expect(itineraryServiceMock.getItinerary).toHaveBeenCalled();
    });

   
    it("should throw an exception when calling saveDay with existing day", function () {
        var nonEmptyGuid = "00000000-0000-0000-0000-000000000001";
        
        var existingDay = {
            "id": nonEmptyGuid,
            "description": "",
            "version": 0,
            "viewMode": 'Edit'
        };
        
        expect(function() { scope.saveDay(existingDay) }).toThrow();
    });

    // -----------------------------------------------------------------------------------------------------------
    // 
    // NOTE: this is delayed as Richard or Stephen cannot find a way to mock the document.getElementById
    //
    // -----------------------------------------------------------------------------------------------------------
    //it("scope should call \"saveDay\" on persistDayDescription", function () {
    //    var emptyGuid = "00000000-0000-0000-0000-000000000000";
    //    spyOn(scope, "saveDay").and.callThrough();
    //
    //    scope.addTempDay();
    //
    //    scope.$root.$digest();
    //
    //    var sender = {
    //        "day": { 'id': emptyGuid } 
    //    };
    //    scope.persistDayDescription(sender);
    //
    //    expect(scope.saveDay).toHaveBeenCalled();
    //});

});