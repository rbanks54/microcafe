
app.config(function ($stateProvider, $urlRouterProvider, $ocLazyLoadProvider, ASSETS) {

    $urlRouterProvider.otherwise("/app/cashier-order");

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
        state("app.cashier-order", {
            url: "/cashier-order",
            templateUrl: appHelper.cashierTemplatePath("order"),
            resolve: {
            }
        }).

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