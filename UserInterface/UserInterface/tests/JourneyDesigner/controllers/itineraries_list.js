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

/// <reference path="../../../apps/JourneyDesigner/services/itineraries.js" />
/// <reference path="../../../apps/JourneyDesigner/controllers/itineraries.js" />

describe("Controller: ItineraryListCtrl", function () {

    // load the controller's module
    beforeEach(module("journeyDesigner-app"));

    // Local Variables
    var scope;
    var log;
    var templateCache;

    // This is for the $http interactions
    var q;
    var deferred;

    //
    var itinerariesControllerMock;
    var itineraryServiceMock;
    var itineraryEditReferenceDataMock;
    var alertingMock;
    var requestCounterMock;
    var itinerariesDataMock;


    // Initialize the mock data and services
    beforeEach(function () {
        //itinerariesDataMock = [{ 'id': "e10c621a-d70f-4f94-8ce4-1fa5146fb981", 'name': "itineraries 01" }, { 'id': "2e595848-6894-4150-ba9c-d4b0283197ce", 'name': "itineraries 02" }];
        itinerariesDataMock = [];

        itineraryServiceMock = {
            getItineraries: function () {
                deferred = q.defer();
                return deferred.promise;
            }
        };

        itineraryEditReferenceDataMock = {
            getData: function () {
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

        requestCounterMock = {
            request: function () {
                deferred = q.defer();
                return deferred.promise;
            },
            requestError: function () {
                deferred = q.defer();
                return deferred.promise;
            },
            response: function () {
                deferred = q.defer();
                return deferred.promise;
            },
            responseError: function () {
                deferred = q.defer();
                return deferred.promise;
            },
            getRequestCount: function () {
                deferred = q.defer();
                return deferred.promise;
            }
        }
    });

    // Initialize the controller and a mock scope
    beforeEach(inject(function ($controller, $rootScope, _$log_, $q) {

        // this is for deferring...
        q = $q;

        // Create a new scope that's a child of the $rootScope
        scope = $rootScope.$new();

        log = _$log_;

        // this is to set up the controller
        itinerariesControllerMock = $controller("ItineraryListCtrl", {
            $scope: scope,
            $log: log,
            itineraryService: itineraryServiceMock,
            alerting: alertingMock,
            requestCounter: requestCounterMock,
            itineraryEditReferenceData: itineraryEditReferenceDataMock
        });
    }));

    it("scope should not be empty", function () {
        expect(scope).not.toBeNull();
    });

    it("should call \"getItineraries\" during init", function () {
        spyOn(itineraryServiceMock, "getItineraries").and.callThrough();
        spyOn(itineraryEditReferenceDataMock, "getData").and.callThrough();

        scope.initialise();

        var response = {
            "data": itinerariesDataMock,
            "headers": function () { return { 'TotalCount': "20" }; }
        };

        deferred.resolve(response);
        scope.$root.$digest();

        expect(itineraryServiceMock.getItineraries).toHaveBeenCalled();
        expect(itineraryEditReferenceDataMock.getData).toHaveBeenCalled();
    });
});
