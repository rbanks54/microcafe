(function (module) {

    var productListController = function ($scope, productService, alerting) {

        var model = this;

        // Searching and filtering
        $scope.sortType = 'name'; // set the default sort type
        $scope.sortReverse = false;  // set the default sort order

        $scope.products = null;

        $scope.loadProducts = function () {
            // Start Loading....
            productService.getProducts()
                .then(function (data) {
                    $scope.products = data;
                    // End Loading...
                })
                .catch(alerting.errorHandler('API Error!', "Failed to retrieve the list of Products."));
        };

        $scope.initialise = function () {
            $scope.loadProducts();
        };

        $scope.initialise();
    };

    var productEditController = function ($scope, $rootScope, $location, $stateParams, productService, confirmProductDelete, product, alerting) {
        var emptyGuid = "00000000-0000-0000-0000-000000000000";
        
        $rootScope.title = 'Unknown';
        $scope.buttonText = 'Unknown';
        $scope.formMode = 'Unknown';

        var productId = emptyGuid;
        var original = null;

        $scope.setOriginal = function (source) {
            productId = source.data.id;
            $rootScope.title = (source.data.id !== emptyGuid) ? 'Edit Product' : 'Add Product';
            $scope.formMode = (source.data.id !== emptyGuid) ? 'Edit' : 'Add';
            $scope.buttonText = 'Save';


            original = source.data;
            original._id = source.data.id;
            $scope.product = angular.copy(original);
            $scope.product._id = source.data.id;
        };

        $scope.isClean = function () {
            return angular.equals(original, $scope.product);
        };

        $scope.confirmDeleteProduct = function(product) {
            confirmProductDelete(product).then($scope.deleteProduct);
        }

        $scope.saveProduct = function (product) {
            $scope.validationErrors = null;
            if (productId === emptyGuid) {
                productService.insertProduct(product,
                    function (response) {
                        $location.path('/app/admin-products');
                        alerting.addSuccess('Add Product', "Successfully added a new Product.");
                    },
                    function (response) {
                        $scope.validationErrors = alerting.getValidationErrors(response);

                        var uiMessage = "Failed to add new Product.";
                        if (response.data.exceptionMessage !== undefined) {

                            var json = response.data.exceptionMessage;

                            var expectionMessageJSON = JSON.parse(json);

                            uiMessage = uiMessage + "<p>" + expectionMessageJSON.Data.Message + "</p>";
                        }

                        alerting.addResponseError(response, 'Add Product', uiMessage);
                    });
            } else {
                productService.updateProduct(productId, product,
                    function (response) {
                        $scope.reloadProduct(response.data.id);
                        alerting.addSuccess('Update Product', "Successfully updated an existing product.");
                    },
                    function (response) {
                        $scope.validationErrors = alerting.getValidationErrors(response);
                        alerting.addResponseError(response, 'Update Prodict', "Failed to add new product.");
                    });
            }
        };

        $scope.reloadProduct = function (productId) {
            productService.getProduct(productId)
                        .then(function (getData) {
                            $scope.setOriginal(getData);
                        });
        };


        $scope.initialise = function () {
            $scope.setOriginal(product);
        };

        $scope.initialise();
    };

    module.controller("productsListCtrl", productListController);
    module.controller("productEditCtrl", productEditController);

}(angular.module("microcafe-app")));