
app.config(function ($stateProvider, $urlRouterProvider, $ocLazyLoadProvider, ASSETS) {

    $urlRouterProvider.otherwise("/app/dashboard-welcome");

    $stateProvider.
        // Main Layout Structure
        state("app", {
            abstract: true,
            url: "/app",
            templateUrl: appHelper.shellTemplatePath("layout/app-body"),
            controller: function ($rootScope) {
                $rootScope.isLoginPage = false;
                $rootScope.isLightLoginPage = false;
                $rootScope.isLockscreenPage = false;
                $rootScope.isMainPage = true;
            },
            resolve: {
                resources: function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        ASSETS.extra.toastr
                    ]);
                }
            }
        }).

        // Dashboards
        state("app.dashboard-welcome", {
            url: "/dashboard-welcome",
            templateUrl: appHelper.journeyDesignerTemplatePath("dashboards/welcome"),
            resolve: {
            }
        }).

        // Products
        // master data - brands
        state("app.journeydesigner-products", {
            url: "/journeydesigner-products",
            templateUrl: appHelper.journeyDesignerTemplatePath("products/products"),
            controller: "productListCtrl",
            resolve: {
            }
        }).

        // master data - brands - Editor
        state("app.journeydesigner-products-edit", {
            url: "/journeydesigner-products-edit/:productId",
            templateUrl: appHelper.journeyDesignerTemplatePath("products/edit-product"),
            controller: "productEditCtrl",
            resolve: {
                product: function (productService, $stateParams) {
                    var productId = $stateParams.productId;
                    if (productId !== "0") {
                        return productService.getProduct(productId);
                    } else {
                        return productService.newProduct();
                    }
                }
            }
        }).

        // Itineraries
        state("app.itineraries-index", {
            url: "/itineraries-index",
            templateUrl: appHelper.journeyDesignerTemplatePath("itineraries/itineraries"),
            controller: "ItineraryListCtrl",
            resolve: {
                select2: function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        ASSETS.forms.select2
                    ]);
                }
            }
        }).
        state("app.itineraries-edit", {
            url: "/itineraries-edit/:itineraryId",
            templateUrl: appHelper.journeyDesignerTemplatePath("itineraries/edit-itinerary"),
            controller: "ItineraryEditCtrl",
            resolve: {
                itinerary: function (itineraryService, $stateParams) {
                    var itineraryId = $stateParams.itineraryId;
                    if (itineraryId !== "0") {
                        return itineraryService.getItinerary(itineraryId);
                    } else {
                        return itineraryService.newItinerary();
                    }
                },
                select2: function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        ASSETS.forms.select2
                    ]);
                }
            }
        }).
        state("app.itineraries-view", {
            url: "/itineraries-view/:itineraryId",
            templateUrl: appHelper.journeyDesignerTemplatePath("itineraries/view"),
            controller: "itineraryViewCtrl",
            resolve: {
                itinerary: function (itineraryService, $stateParams) {
                    var itineraryId = $stateParams.itineraryId;
                    //return { "id": itineraryId };
                    if (itineraryId !== "0") {
                        return itineraryService.getItinerary(itineraryId);
                    } else {
                        throw 'itinerary failure';
                    }
                }
            }
        }).

        // master data - brands
        state("app.masterdata-brands", {
            url: "/masterdata-brands",
            templateUrl: appHelper.masterDataTemplatePath("brands/brands"),
            controller: "brandListCtrl",
            resolve: {
            }
        }).

        // master data - brands - Editor
        state("app.masterdata-brands-edit", {
            url: "/masterdata-brands-edit/:brandId",
            templateUrl: appHelper.masterDataTemplatePath("brands/edit-brand"),
            controller: "brandEditCtrl",
            resolve: {
                brand: function (brandService, $stateParams) {
                    var brandId = $stateParams.brandId;
                    if (brandId !== "0") {
                        return brandService.getBrand(brandId);
                    } else {
                        return brandService.newBrand();
                    }
                }
            }
        }).

        // master data - operators
        state("app.masterdata-operators", {
            url: "/masterdata-operators",
            templateUrl: appHelper.masterDataTemplatePath("operators/operators"),
            controller: "operatorListCtrl",
            resolve: {
            }
        }).

        // master data - operators - Editor
        state("app.masterdata-operators-edit", {
            url: "/masterdata-operators-edit/:operatorId",
            templateUrl: appHelper.masterDataTemplatePath("operators/edit-operator"),
            controller: "operatorEditCtrl",
            resolve: {
                operator: function (operatorService, $stateParams) {
                    var operatorId = $stateParams.operatorId;
                    if (operatorId !== "0") {
                        return operatorService.getOperator(operatorId);
                    } else {
                        return operatorService.newOperator();
                    }
                }
            }
        }).

        // Logins and Lockscreen
        state("login", {
            url: "/login",
            templateUrl: appHelper.shellTemplatePath("login"),
            controller: "LoginCtrl",
            resolve: {
                resources: function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        ASSETS.forms.jQueryValidate,
                        ASSETS.extra.toastr
                    ]);
                }
            }
        }).
        state("login-light", {
            url: "/login-light",
            templateUrl: appHelper.shellTemplatePath("login-light"),
            controller: "LoginLightCtrl",
            resolve: {
                resources: function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        ASSETS.forms.jQueryValidate
                    ]);
                }
            }
        }).
        state("lockscreen", {
            url: "/lockscreen",
            templateUrl: appHelper.shellTemplatePath("lockscreen"),
            controller: "LockscreenCtrl",
            resolve: {
                resources: function ($ocLazyLoad) {
                    return $ocLazyLoad.load([
                        ASSETS.forms.jQueryValidate,
                        ASSETS.extra.toastr
                    ]);
                }
            }
        });
});

app.constant("ASSETS", {

    'forms': {
        'select2': [
            appHelper.assetPath('js/select2/select2.css'),
            appHelper.assetPath('js/select2/select2-bootstrap.css'),
            appHelper.assetPath('js/select2/select2.min.js')
        ],
        'jQueryValidate': appHelper.assetPath("js/jquery-validate/jquery.validate.min.js")
    },

    'extra': {
        'toastr': appHelper.assetPath("js/toastr/toastr.min.js")
    }
});