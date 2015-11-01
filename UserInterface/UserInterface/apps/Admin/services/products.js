(function (admin) {

    var productService = function ($http, alerting) {

        var apiBase = '/api/admin/products';
        var singleProductApiBase = '/api/admin/product';
        var emptyGuid = "00000000-0000-0000-0000-000000000000";
        var obj = {};

        obj.getProducts = function() {
            return $http.get(apiBase)
                .then(function (results) {
                    return results.data;
                })
                .catch(alerting.errorHandler('API Error!', "Failed to retrieve the list of Products."));
        };

        obj.getProduct = function(productId) {
            return $http.get(singleProductApiBase + '/' + productId);
        };
        
        obj.insertProduct = function (product, successCallback, errorCallback) {
            return $http.post(apiBase, product)
                .then(successCallback, errorCallback)
                .catch(alerting.errorHandler('API Error', "Failed to insert a new Product."));
        };

        obj.updateProduct = function (productId, product, successCallback, errorCallback) {
            return $http.put(singleProductApiBase + "/" + productId, product)
                .then(successCallback, errorCallback)
                .catch(alerting.errorHandler('API Error', "Failed to Update Product."));
        };

        obj.deleteProduct = function (productId) {
            return $http.delete(singleProductApiBase + '/' + productId)
                .then(function (status) {
                    return status.data;
                })
                .catch(alerting.errorHandler('API Error!', "Failed to delete the Product."));
        };

        obj.newProduct = function() {
            return { "data": { "id": emptyGuid, "name": "", "description": "" } };
        };

        return obj;
    };

    var confirmProductDelete = function ($modal) {
        return function (brand) {
            var options = {
                templateUrl: appHelper.adminTemplatePath("products/confirmBrandDelete"), 
                controller: function () {
                    this.brand = brand;
                },
                controllerAs: "model"
            };

            return $modal.open(options).result;
        };
    };

    admin.factory("confirmProductDelete", confirmProductDelete);
    admin.factory("productService", productService);

}(angular.module("microcafe-app")))