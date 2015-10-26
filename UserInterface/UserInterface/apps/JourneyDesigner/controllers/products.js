(function (module) {

    var productListController = function ($scope, $rootScope, productService, alerting, requestCounter) {

        $rootScope.title = 'Products';

        // Searching and filtering
        $scope.sortType = 'code'; // set the default sort type
        $scope.sortReverse = false;  // set the default sort order

        $scope.products = null;
        $scope.isLoaded = false;

        $scope.loadProducts = function () {
            // Start Loading....
            productService.getProducts()
                .then(function (data) {
                    $scope.products = data;
                    // End Loading...
                    $scope.isLoaded = true && requestCounter.getRequestCount() === 0;
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

        $scope.loadProductTypes = function () {
            // Start Loading....
            productService.getProductTypes()
                .then(function (result) {
                    $scope.productTypes = result.data;
                    // End Loading...
                })
                .catch(alerting.errorHandler('API Error!', "Failed to retrieve the list of Product Types"));
        };

        $scope.setOriginal = function (source) {
            productId = source.data.id;
            $rootScope.title = (source.data.id !== emptyGuid) ? 'Edit Product' : 'Add Product';
            $scope.buttonText = 'Save';
            $scope.formMode = (source.data.id !== emptyGuid) ? 'Edit' : 'Add';

            original = source.data;
            original._id = source.data.id;
            $scope.product = angular.copy(original);
            $scope.product._id = source.data.id;
        };

        $scope.isClean = function () {
            return angular.equals(original, $scope.product);
        };

        $scope.deleteProduct = function (product) {

            throw 'Not Impletement! - deleteProduct';

            //$location.path('/apps/index.html#/app/journeydesigner-products');
            //productService.deleteProduct(product.productNumber);
        };

        $scope.confirmDeleteProduct = function(product) {
            confirmProductDelete(product).then($scope.deleteProduct);
        }

        $scope.saveProduct = function (product) {
            
            if (productId === emptyGuid) {
                productService.insertProduct(product)
                    .then(function (response) {
                        $scope.reloadProduct(response.data.id);
                        alerting.addSuccess('Add Product!', "Successfully added a new Product.");
                    });
            } else {
                productService.updateProduct(productId, product)
                    .then(function (data) {
                        $scope.reloadProduct(data.id);
                        alerting.addSuccess('Update Product!', "Successfully updated an existing Product.");
                    });
            }

            $location.path("/app/journeydesigner-products");

        };

        $scope.reloadProduct = function(productId) {
            productService.getProduct(productId)
                        .then(function (getData) {
                            $scope.setOriginal(getData);
                        });
        };


        $scope.initialise = function () {
            $scope.loadProductTypes();
            $scope.setOriginal(product);
        };

        $scope.initialise();
    };

    module.controller("productListCtrl", productListController);
    module.controller("productEditCtrl", productEditController);

}(angular.module("journeyDesigner-app")));