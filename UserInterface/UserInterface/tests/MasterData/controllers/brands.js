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

/// <reference path="../../../apps/MasterData/controllers/brands.js" />

describe("Controller: brandListCtrl", function () {

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
    var brandsControllerMock;
    var brandsServiceMock;
    var alertingMock;
    var requestCounterMock;
    var brandDataMock;


    // Initialize the mock data and services
    beforeEach(function () {
        brandDataMock = [
  {
      "id": "415029be-4560-4828-aafa-c753f63ed7d4",
      "title": "ROUGHIES",
      "code": "Sue Underwood",
      "description": "In laboris anim sint laboris reprehenderit ipsum ea. Exercitation enim officia voluptate anim aliquip enim et excepteur id commodo est exercitation. Id nulla velit laborum duis est duis enim ut reprehenderit cillum ullamco.\r\n"
  },
  {
      "id": "5cea8cce-8176-430b-b524-82d4311b15d7",
      "title": "NETBOOK",
      "code": "Cora Wynn",
      "description": "Consectetur cupidatat ut proident velit cupidatat quis sit. Quis aliqua qui sunt aliquip. Irure aliqua esse proident proident veniam laboris officia.\r\n"
  },
  {
      "id": "76735d03-59af-443c-9832-11550bb59b76",
      "title": "COMTOURS",
      "code": "Mildred Ruiz",
      "description": "Pariatur pariatur aliqua velit eiusmod officia id. Cillum est aliqua magna do eiusmod. Commodo non Lorem magna irure aliqua nulla. Consectetur ipsum ad exercitation tempor. Irure culpa qui commodo ipsum Lorem laborum culpa nisi. Qui in mollit sit tempor id magna sit occaecat.\r\n"
  },
  {
      "id": "a57182a7-739b-48b9-b8cb-6fafd4f17597",
      "title": "ACRODANCE",
      "code": "Adkins Brewer",
      "description": "Laboris ex officia adipisicing do sunt. Est consequat duis esse enim ex velit. Id dolor ut exercitation labore et aliquip velit esse in laboris excepteur anim qui.\r\n"
  },
  {
      "id": "aeee4af1-23ea-42ba-9988-19475ab909e9",
      "title": "INTERGEEK",
      "code": "Shana Rhodes",
      "description": "Nostrud sunt ullamco enim tempor elit officia laboris ut excepteur do eiusmod magna incididunt esse. Fugiat voluptate eiusmod id pariatur incididunt dolor ipsum ullamco exercitation. Amet ea dolor velit nostrud laborum qui veniam culpa et. Excepteur eu et et Lorem velit excepteur. Ullamco occaecat enim reprehenderit exercitation exercitation nisi in aliqua non laboris anim excepteur amet. Nostrud amet reprehenderit mollit anim incididunt sunt dolore.\r\n"
  }
        ];

        brandsServiceMock = {
            getBrands: function () {
                deferred = q.defer();
                return deferred.promise;
            },
            getBrand: function () {
                deferred = q.defer();
                return deferred.promise;
            },
            insertBrand: function (settings) {
                return;
            },
            updateBrand: function (settings) {
                return;
            },
            deleteBrand: function (target) {
                return;
            },
            newBrand: function (target) {
                return;
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
    beforeEach(inject(function ($controller, $rootScope, _$log_, _$templateCache_, $q) {

        // this is for deferring...
        q = $q;

        // Create a new scope that's a child of the $rootScope
        scope = $rootScope.$new();

        log = _$log_;
        templateCache = _$templateCache_;

        // this is to set up the controller
        brandsControllerMock = $controller("brandListCtrl", {
            $scope: scope,
            brandService: brandsServiceMock,
            alerting: alertingMock
        });
    }));

    //------------------------------------------------------------------------------
    // Actual Tests
    //------------------------------------------------------------------------------
    it("scope should not be empty", function () {
        expect(scope).not.toBeNull();
    });

    it("should request the \"get All Brands\" during init", function () {
        spyOn(brandsServiceMock, "getBrands").and.callThrough();

        scope.initialise();

        // ---------------------------------------------------
        // How to I mock a response from the $http.Get ?
        // ---------------------------------------------------
        var response = {
            "data": brandDataMock,
            "headers": function () { return { 'TotalCount': "20" }; }
        };

        deferred.resolve(response);

        scope.$root.$digest();

        expect(brandsServiceMock.getBrands).toHaveBeenCalled();
    });
});