(function (module) {

    var orderEntryController = function ($scope, $rootScope, $location, $stateParams, orderService, order) {
        $rootScope.title = 'Place an order';
        $scope.buttonText = 'Place';
        $scope.paymentButtonText = 'Pay';

        $scope.products = [];

        $scope.updateTotal = function () {
            if ($scope.order.product !== nothing){
                $scope.order.total = $scope.order.quantity * $scope.product.price;
            }
        }

        $scope.placeOrder = function(order)
        {

            $scope.order.placed = true;

        }

        $scope.payForOrder = function(order)
        {
            //do something
        }

        $scope.setOriginal = function () {
            $scope.order.quantity = 0;
            $scope.order.product = "";
            $scope.order.total = 0;
            $scope.order.placed = false;
        };

        $scope.loadAssociatedLists = function () {
            productLookupService.getData()
                .then(function (results) {
                    $scope.products = results.productDtos;
                });
        };

        $scope.initialise = function () {
            $scope.setOrderToEmpty();
            $scope.loadAssociatedLists();
        };

        $scope.initialise();
    };

    module.controller("orderEntryCtrl", orderEntryController);

}(angular.module("microcafe-app")));