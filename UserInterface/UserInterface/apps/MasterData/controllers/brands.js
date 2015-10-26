(function (module) {

    var brandListController = function ($scope, brandService, alerting) {

        var model = this;

        // Searching and filtering
        $scope.sortType = 'name'; // set the default sort type
        $scope.sortReverse = false;  // set the default sort order

        $scope.brands = null;

        $scope.loadBrands = function () {
            // Start Loading....
            brandService.getBrands()
                .then(function (data) {
                    $scope.brands = data;
                    // End Loading...
                })
                .catch(alerting.errorHandler('API Error!', "Failed to retrieve the list of Brands."));
        };

        $scope.initialise = function () {
            $scope.loadBrands();
        };

        $scope.initialise();
    };

    var brandEditController = function ($scope, $rootScope, $location, $stateParams, brandService, confirmBrandDelete, brand, alerting) {
        var emptyGuid = "00000000-0000-0000-0000-000000000000";
        
        $rootScope.title = 'Unknown';
        $scope.buttonText = 'Unknown';
        $scope.formMode = 'Unknown';

        var brandId = emptyGuid;
        var original = null;

        $scope.setOriginal = function (source) {
            brandId = source.data.id;
            $rootScope.title = (source.data.id !== emptyGuid) ? 'Edit Brand' : 'Add Brand';
            $scope.formMode = (source.data.id !== emptyGuid) ? 'Edit' : 'Add';
            $scope.buttonText = 'Save';


            original = source.data;
            original._id = source.data.id;
            $scope.brand = angular.copy(original);
            $scope.brand._id = source.data.id;
        };

        $scope.isClean = function () {
            return angular.equals(original, $scope.brand);
        };

        $scope.deleteBrand = function (brand) {

            throw 'Not Impletement! - deleteBrand';

            //$location.path('/apps/index.html#/app/masterdata-brands');
            //brandService.deleteBrand(brand.brandNumber);
        };

        $scope.confirmDeleteBrand = function(brand) {
            confirmBrandDelete(brand).then($scope.deleteBrand);
        }

        $scope.saveBrand = function (brand) {
            $scope.validationErrors = null;
            if (brandId === emptyGuid) {
                brandService.insertBrand(brand,
                    function (response) {
                        $location.path('/app/masterdata-brands');
                        alerting.addSuccess('Add Brand', "Successfully added a new Brand.");
                    },
                    function (response) {
                        $scope.validationErrors = alerting.getValidationErrors(response);

                        var uiMessage = "Failed to add new Brand.";
                        if (response.data.exceptionMessage !== undefined) {

                            var json = response.data.exceptionMessage;

                            var expectionMessageJSON = JSON.parse(json);

                            uiMessage = uiMessage + "<p>" + expectionMessageJSON.Data.Message + "</p>";
                        }

                        alerting.addResponseError(response, 'Add Brand', uiMessage);
                    });
            } else {
                brandService.updateBrand(brandId, brand,
                    function (response) {
                        $scope.reloadBrand(response.data.id);
                        alerting.addSuccess('Update Brand', "Successfully updated an existing Brand.");
                    },
                    function (response) {
                        $scope.validationErrors = alerting.getValidationErrors(response);
                        alerting.addResponseError(response, 'Update Brand', "Failed to add new Brand.");
                    });
            }
        };

        $scope.reloadBrand = function(brandId) {
            brandService.getBrand(brandId)
                        .then(function (getData) {
                            $scope.setOriginal(getData);
                        });
        };


        $scope.initialise = function () {
            $scope.setOriginal(brand);
        };

        $scope.initialise();
    };

    module.controller("brandListCtrl", brandListController);
    module.controller("brandEditCtrl", brandEditController);

}(angular.module("journeyDesigner-app")));