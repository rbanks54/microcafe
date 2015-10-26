(function (module) {

    var operatorListController = function ($scope, operatorService, alerting) {

        var model = this;

        // Searching and filtering
        $scope.sortType = 'code'; // set the default sort type
        $scope.sortReverse = false;  // set the default sort order

        $scope.operators = null;

        $scope.loadOperators = function () {
            operatorService.getOperators(
                function (response) {
                    $scope.operators = response.data;
                },
                function (response) {
                    alerting.addResponseError(response, 'Load Operators', "Failed to retrieve the list of Operators.");
                });
        };

        $scope.initialise = function () {
            $scope.loadOperators();
        };

        $scope.initialise();
    };

    var operatorEditController = function ($scope, $rootScope, $location, $stateParams, operatorService, confirmOperatorAction, operator, alerting) {
        var emptyGuid = "00000000-0000-0000-0000-000000000000";
        
        $rootScope.title = 'Unknown';

        var operatorId = emptyGuid;
        var original = null;

        // clear server side validation error list when model changes
        $scope.$watch('operator',function(newValue, oldValue) {
            if (newValue != oldValue) {
                $scope.validationErrors = null;
            }
        }, true);

        // ensure operator code is upper case
        $scope.onCodeChanged = function () {
            if ($scope.operator && $scope.operator.code) {
                $scope.operator.code = $scope.operator.code.toUpperCase();
            }
        }

        $scope.setOriginal = function (source) {
            operatorId = source.data.id;
            $rootScope.title = (source.data.id !== emptyGuid) ? 'Edit Operator' : 'Add Operator';

            original = source.data;
            original._id = source.data.id;
            $scope.operator = angular.copy(original);
            $scope.operator._id = source.data.id;
            $scope.validationErrors = null;
        };

        $scope.isClean = function () {
            return angular.equals(original, $scope.operator);
        };

        $scope.deleteOperator = function (operator) {
            operatorService.deleteOperator(operatorId, operator,
                function (response) {
                    $location.path('/app/masterdata-operators');
                    alerting.addSuccess('Delete Operator', "Successfully deleted Operator.");
                },
                function (response) {
                    alerting.addResponseError(response, 'Delete Operator', "Failed to delete Operator.");
                });
            
        };

        $scope.confirmDeleteOperator = function (operator) {
            confirmOperatorAction(operator, 'delete').then($scope.deleteOperator);
        }

        $scope.archiveOperator = function (operator) {
            operatorService.archiveOperator(operatorId, operator,
                function (response) {
                    $scope.reloadOperator(response.data.id);
                    alerting.addSuccess('Archive Operator', "Successfully archived Operator.");
                },
                function (response) {
                    alerting.addResponseError(response, 'Archive Operator', "Failed to archive Operator.");
                });
        };

        $scope.confirmArchiveOperator = function (operator) {
            confirmOperatorAction(operator, 'archive').then($scope.archiveOperator);
        }

        $scope.activateOperator = function (operator) {
            operatorService.activateOperator(operatorId, operator,
                function (response) {
                    $scope.reloadOperator(response.data.id);
                    alerting.addSuccess('Activate Operator', "Successfully activated Operator.");
                },
                function (response) {
                    alerting.addResponseError(response, 'Activate Operator', "Failed to activate Operator.");
                });
        };

        $scope.confirmActivateOperator = function (operator) {
            confirmOperatorAction(operator, 'activate').then($scope.activateOperator);
        }

        $scope.saveOperator = function (operator) {
            $scope.validationErrors = null;
            if (operatorId === emptyGuid) {
                operatorService.insertOperator(operator,
                    function (response) {
                        $location.path('/app/masterdata-operators'); 
                        alerting.addSuccess('Add Operator', "Successfully added a new Operator.");
                    },
                    function (response) {
                        $scope.validationErrors = alerting.getValidationErrors(response);
                        alerting.addResponseError(response, 'Add Operator', "Failed to add new Operator.");
                    });
            } else {
                operatorService.updateOperator(operatorId, operator,
                    function (response) {
                        $scope.reloadOperator(response.data.id);
                        alerting.addSuccess('Update Operator', "Successfully updated an existing Operator.");
                    },
                    function (response) {
                        $scope.validationErrors = alerting.getValidationErrors(response);
                        alerting.addResponseError(response, 'Update Operator', "Failed to add new Operator.");
                    });
            }
        };

        $scope.reloadOperator = function(operatorId) {
            operatorService.getOperator(operatorId)
                        .then(function (getData) {
                            $scope.setOriginal(getData);
                        });
        };

        $scope.initialise = function () {
            $scope.setOriginal(operator);
        };

        $scope.initialise();
    };

    module.controller("operatorListCtrl", operatorListController);
    module.controller("operatorEditCtrl", operatorEditController);

}(angular.module("journeyDesigner-app")));