(function (productData) {

    var productService = function ($http, alerting) {

        var serviceBase = '/api/journeydesigner/products';
        var emptyGuid = "00000000-0000-0000-0000-000000000000";
        var obj = {};

        obj.getProducts = function() {
            return $http.get(serviceBase)
                .then(function (results) {
                    return results.data;
                })
                .catch(alerting.errorHandler('API Error!', "Failed to retrieve the list of Products."));
        };

        obj.getProduct = function(productId) {
            return $http.get(serviceBase + '/' + productId);
        };

        obj.getProductTypes = function () {
            return $http.get(serviceBase + '/types');
        };

        obj.insertProduct = function (product) {
            return $http.post(serviceBase, product)
                .then(function (results) {
                    return results;
                })
                .catch(alerting.errorHandler('API Error!', "Failed to insert a new Product."));
        };

        obj.updateProduct = function (productId, product) {
            return $http.put(serviceBase + "/" + productId, product)
                .then(function (status) {
                    return status.data;
                })
                .catch(alerting.errorHandler('API Error!', "Failed to update an existing Product."));
        };

        obj.deleteProduct = function (productId) {
            return $http.delete(serviceBase + '/' + productId)
                .then(function (status) {
                    return status.data;
                })
                .catch(alerting.errorHandler('API Error!', "Failed to delete an existing Product."));
        };

        obj.newProduct = function() {
            return { "data": { "id": emptyGuid, "code": "", "title": "", "description": "" } };
        };


        return obj;

    };

    var confirmProductDelete = function ($modal) {
        return function (product) {
            var options = {
                templateUrl: appHelper.journeyDesignerTemplatePath("products/confirmProductDelete"), 
                controller: function () {
                    this.product = product;
                },
                controllerAs: "model"
            };

            return $modal.open(options).result;
        };
    };

    productData.factory("confirmProductDelete", confirmProductDelete);
    productData.factory("productService", productService);

}(angular.module("journeyDesigner-app")))