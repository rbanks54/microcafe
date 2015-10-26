(function (masterData) {

    var brandService = function ($http, alerting) {

        var serviceBase = '/api/masterdata/brands';
        var emptyGuid = "00000000-0000-0000-0000-000000000000";
        var obj = {};

        obj.getBrands = function() {
            return $http.get(serviceBase)
                .then(function (results) {
                    return results.data;
                })
                .catch(alerting.errorHandler('API Error!', "Failed to retrieve the list of Brands."));
        };

        obj.getBrand = function(brandId) {
            return $http.get(serviceBase + '/' + brandId);
        };
        
        obj.insertBrand = function (brand, successCallback, errorCallback) {
            return $http.post(serviceBase, brand)
                .then(successCallback, errorCallback)
                .catch(alerting.errorHandler('API Error', "Failed to insert a new Brand."));
        };

        obj.updateBrand = function (brandId, brand, successCallback, errorCallback) {
            return $http.put(serviceBase + "/" + brandId, brand)
                .then(successCallback, errorCallback)
                .catch(alerting.errorHandler('API Error', "Failed to Update Brand."));
        };

        obj.deleteBrand = function (brandId) {
            return $http.delete(serviceBase + '/' + brandId)
                .then(function (status) {
                    return status.data;
                })
                .catch(alerting.errorHandler('API Error!', "Failed to delete an existing Brand."));
        };

        obj.newBrand = function() {
            return { "data": { "id": emptyGuid, "code": "", "name": "" } };
        };


        return obj;

    };

    var confirmBrandDelete = function ($modal) {
        return function (brand) {
            var options = {
                templateUrl: appHelper.masterDataTemplatePath("brands/confirmBrandDelete"), 
                controller: function () {
                    this.brand = brand;
                },
                controllerAs: "model"
            };

            return $modal.open(options).result;
        };
    };

    masterData.factory("confirmBrandDelete", confirmBrandDelete);
    masterData.factory("brandService", brandService);

}(angular.module("journeyDesigner-app")))