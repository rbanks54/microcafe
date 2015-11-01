
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

        //// Products
        //// master data - brands
        //state("app.journeydesigner-products", {
        //    url: "/journeydesigner-products",
        //    templateUrl: appHelper.journeyDesignerTemplatePath("products/products"),
        //    controller: "productListCtrl",
        //    resolve: {
        //    }
        //}).

        //// master data - brands - Editor
        //state("app.journeydesigner-products-edit", {
        //    url: "/journeydesigner-products-edit/:productId",
        //    templateUrl: appHelper.journeyDesignerTemplatePath("products/edit-product"),
        //    controller: "productEditCtrl",
        //    resolve: {
        //        product: function (productService, $stateParams) {
        //            var productId = $stateParams.productId;
        //            if (productId !== "0") {
        //                return productService.getProduct(productId);
        //            } else {
        //                return productService.newProduct();
        //            }
        //        }
        //    }
        //}).

        // admin - products
        state("app.admin-products", {
            url: "/admin-products",
            templateUrl: appHelper.adminTemplatePath("products/products"),
            controller: "productsListCtrl",
            resolve: {
            }
        }).

        // admin - products - Editor
        state("app.admin-products-edit", {
            url: "/admin-products-edit/:productId",
            templateUrl: appHelper.adminTemplatePath("products/edit-product"),
            controller: "productEditCtrl",
            resolve: {
                product: function (productService, $stateParams) {
                    var id = $stateParams.productId;
                    if (id !== "0") {
                        return productService.getProduct(id);
                    } else {
                        return productService.newProduct();
                    }
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