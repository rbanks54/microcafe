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
                     
/// <reference path="../../../apps/JourneyDesigner/services/products.js" />


describe("Service: productService", function () {

    // load the service's module
    beforeEach(module("journeyDesigner-app")); 

    // instantiate service
    var productService;
    var $httpBackend;

    beforeEach(inject(function (_$httpBackend_, _productService_) {
        productService = _productService_;
        $httpBackend = _$httpBackend_;
    }));


    it("should GET set of \"products\" from the server", function () {
        $httpBackend.expectGET("/api/journeydesigner/products").respond(200, { 'id': "e10c621a-d70f-4f94-8ce4-1fa5146fb981", 'name': "Products 01" }, { 'id': "2e595848-6894-4150-ba9c-d4b0283197ce", 'name': "Products 02" });

        var productSet = productService.getProducts();

        $httpBackend.flush();

        expect(productSet).not.toBe(null);
    });


    afterEach(function () {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    });
});