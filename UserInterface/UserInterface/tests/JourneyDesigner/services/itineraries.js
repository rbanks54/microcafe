
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
                     
/// <reference path="../../../apps/JourneyDesigner/services/days.js" />
/// <reference path="../../../apps/JourneyDesigner/services/itineraries.js" />

describe("Service: itineraryService", function () {

    // load the service's module
    beforeEach(module("journeyDesigner-app")); 

    // instantiate service
    var itineraryService;
    var $httpBackend;

    beforeEach(inject(function (_$httpBackend_, _itineraryService_) {
        itineraryService = _itineraryService_;
        $httpBackend = _$httpBackend_;
    }));


    it("should GET set of \"itineraries\" from the server", function () {
        $httpBackend.expectGET("/api/itineraries").respond(200, { 'id': "e10c621a-d70f-4f94-8ce4-1fa5146fb981", 'name': "Products 01" }, { 'id': "2e595848-6894-4150-ba9c-d4b0283197ce", 'name': "Products 02" });

        var itinerariesSet = itineraryService.getItineraries();

        $httpBackend.flush();

        expect(itinerariesSet).not.toBe(null);
    });


    it("should GET an individual \"itinerary\" from the server", function () {
        
        $httpBackend.expectGET("/api/itineraries/e10c621a-d70f-4f94-8ce4-1fa5146fb981").respond(200, { 'id': "e10c621a-d70f-4f94-8ce4-1fa5146fb981", 'name': "Itinerary 01" });

        var itinerary = itineraryService.getItinerary("e10c621a-d70f-4f94-8ce4-1fa5146fb981");

        $httpBackend.flush();

        expect(itinerary).not.toBe(null);
    });
    
    it("should return a day object an adding a day to the itinerary", function () {
        var emptyGuid = "00000000-0000-0000-0000-000000000000";
        var tempItinerary = {
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
                "77a616d0-69b9-47cc-a2e4-4bb8305eeaf8"
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
                }
            ]
        };
        var newDay = {
            "id": emptyGuid,
            "description": "Paris",
            "version": 0,
            "viewMode": 'Edit'
        };

        $httpBackend.expectPOST("/api/itineraries/" + tempItinerary.id + "/addday").respond(200, { 'id': tempItinerary.id, 'name': "Itinerary 01" });

        var addDayResponse = itineraryService.insertItineraryDay(tempItinerary, newDay);

        $httpBackend.flush();

        expect(addDayResponse).not.toBeNull();
        expect(addDayResponse.newDayId).not.toBe(emptyGuid);
    });

    afterEach(function () {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    });
});