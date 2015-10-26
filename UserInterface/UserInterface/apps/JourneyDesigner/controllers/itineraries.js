(function (module) {

    var ItineraryListController = function ($scope, itineraryService, alerting, requestCounter, itineraryEditReferenceData) {

        var model = this;

        // Variables for the form
        $scope.itineraries = null;
        $scope.seasons = [];
        $scope.products = [];

        // for Sorting
        $scope.sortType = 'name'; // set the default sort type
        $scope.sortReverse = false;  // set the default sort order
        $scope.searchSeason = '';     // set the default search/filter term
        $scope.searchProduct = '';     // set the default search/filter term
        
        // 
        $scope.isLoaded = false;

        $scope.loadItineraries = function () {
            // Start Loading....
            itineraryService.getItineraries()
                .then(function (data) {
                    $scope.itineraries = data;
                    // End Loading...
                    $scope.isLoaded = true && requestCounter.getRequestCount() === 0;
                })
                .catch(alerting.errorHandler('API Error!', "Failed to retrieve the list of Itinerarys."));
        };

        $scope.loadAssociatedLists = function () {
            itineraryEditReferenceData.getData()
                .then(function (getData) {
                    $scope.products = getData.productDtos;
                    $scope.seasons = getData.seasonDtos;
                    // End Loading...
                    $scope.isLoaded = true && requestCounter.getRequestCount() === 0;
                });
        };

        $scope.initialise = function () {
            $scope.loadItineraries();
            $scope.loadAssociatedLists();
        };

        $scope.initialise();
    };

    var ItineraryEditController = function ($scope, $rootScope, $location, $stateParams, itineraryService, confirmItineraryAction, itinerary, alerting, itineraryEditReferenceData) {
        var emptyGuid = "00000000-0000-0000-0000-000000000000";

        $rootScope.title = 'Unknown';
        $scope.buttonText = 'Unknown';
        $scope.formMode = 'Unknown';

        var itineraryId = emptyGuid;
        var original = null;

        // For Lookups
        $scope.products = [];
        $scope.brands = [];
        $scope.operators = [];
        $scope.seasons = [];

        $scope.setOriginal = function (source) {
            itineraryId = source.data.id;
            $rootScope.title = (source.data.id !== emptyGuid) ? 'Edit Itinerary' : 'Add Itinerary';
            $scope.buttonText = 'Save';
            $scope.formMode = (source.data.id !== emptyGuid) ? 'Update' : 'Add';

            original = source.data;
            original._id = source.data.id;
            $scope.itinerary = angular.copy(original);
            $scope.itinerary._id = source.data.id;
        };

        $scope.isClean = function () {
            return angular.equals(original, $scope.itinerary);
        };

        $scope.deleteItinerary = function (itinerary) {
            itineraryService.deleteItinerary(itineraryId, itinerary)
                        .then(function (getData) {
                            $location.path('/app/itineraries-index');
                            alerting.addSuccess('Delete Itinerary!', "Successfully deleted an existing Itinerary.");
                        });
        };

        $scope.confirmDeleteItinerary = function (itinerary) {
            confirmItineraryAction(itinerary, 'delete').then($scope.deleteItinerary);
        }

        $scope.checkDefaultName = function () {
            $scope.itinerary.name = $scope.itinerary.product.name;
        }

        $scope.saveItinerary = function (itinerary) {

            // Adjust the values from the drop downs before passing through
            itinerary.seasonId = itinerary.season.id;
            itinerary.productId = itinerary.product.id;
            itinerary.brandId = itinerary.brand ? itinerary.brand.id : "";
            itinerary.operatorId = itinerary.operator ? itinerary.operator.id : "";
            
            $scope.validationErrors = null;
            if (itineraryId === emptyGuid) {
                itineraryService.insertItinerary(itinerary,
                    function (response) {
                        $location.path('/app/itineraries-index');
                        alerting.addSuccess('Add Itinerary', "Successfully added a new Itinerary.");
                    },
                    function (response) {
                        $scope.validationErrors = alerting.getValidationErrors(response);

                        var uiMessage = "Failed to add new Itinerary.";
                        if (response.data.exceptionMessage !== undefined) {

                            var json = response.data.exceptionMessage;

                            var expectionMessageJSON = JSON.parse(json);

                            uiMessage = uiMessage + "<p>" + expectionMessageJSON.Data.Message + "</p>";
                        }

                        alerting.addResponseError(response, 'Add Itinerary', uiMessage);
                    });
            } else {
                itineraryService.updateItinerary(itineraryId, itinerary,
                    function (response) {
                        $scope.reloadItinerary(response.data.id);
                        alerting.addSuccess('Update Itinerary', "Successfully updated an existing Itinerary.");
                    },
                    function (response) {
                        $scope.validationErrors = alerting.getValidationErrors(response);
                        alerting.addResponseError(response, 'Update Itinerary', "Failed to add new Itinerary.");
                    });
            }
        };

        $scope.reloadItinerary = function (itineraryId) {
            itineraryService.getItinerary(itineraryId)
                        .then(function (getData) {
                            $scope.setOriginal(getData);
                        });
        };

        $scope.loadAssociatedLists = function () {
            itineraryEditReferenceData.getData()
                .then(function (getData) {
                    $scope.products = getData.productDtos;
                    $scope.brands = getData.brandDtos;
                    $scope.operators = getData.operatorDtos;
                    $scope.seasons = getData.seasonDtos;
                });
        };

        $scope.initialise = function () {
            $scope.setOriginal(itinerary);
            $scope.loadAssociatedLists();
        };

        $scope.initialise();
    };

    var ItineraryViewController = function ($scope, $rootScope, $location, $stateParams, itineraryService, dayService, itinerary, alerting, confirmItineraryDayAction) {
        var emptyGuid = "00000000-0000-0000-0000-000000000000";

        $scope.emptyGuid = emptyGuid;
        $scope.toggleAllDaysNextAction = 'Selected';
        $rootScope.title = 'Unknown';

        var itinerayId = emptyGuid;
        var original = null;
        
        $scope.models = {
            selected: null
        };

        $scope.toggleAllDays = function (source) {
            source.itineraryDays.forEach(function (value, index, ar) {
                value["viewMode"] = $scope.toggleAllDaysNextAction;
            });

            $scope.toggleAllDaysNextAction = ($scope.toggleAllDaysNextAction === 'Selected' ? 'Read' : 'Selected');
        }

        $scope.setOriginal = function (source) {
            itinerayId = source.data.id;
            $rootScope.title = (source.data.id !== emptyGuid) ? 'View Itinerary' : '!!!!ERROR!!!!';

            // Need to add an ui properties to the javascript object
            if (source.data.itineraryDays !== null) {
                source.data.itineraryDays.forEach(function(value, index, ar) {
                    value["viewMode"] = 'Read';
                });
            }

            original = source.data;
            original._id = source.data.id;
            $scope.itinerary = angular.copy(original);
            $scope.itinerary.selectedDay = null;

            $scope.itinerary._id = source.data.id;
        };

        $scope.reloadItinerary = function (itineryId) {
            itineraryService.getItinerary(itineryId)
                .then(function (source) {
                    var currentEdited = [];
                    var currentSelected = [];
                    $scope.itinerary.itineraryDays.forEach(function(day, index, ar) {
                        if (day.viewMode === 'Selected')
                        {
                            currentSelected.push(day.id);
                        }
                        if (day.viewMode === 'Edit') {
                            currentEdited.push(day.id);
                        }
                    });

                    angular.extend($scope.itinerary, source.data);

                    // maintain current user day view mode
                    $scope.itinerary.itineraryDays.forEach(function (day, index, ar) {
                        if (currentEdited.indexOf(day.id) > -1) {
                            day.viewMode = 'Edit';
                        }
                        else if (currentSelected.indexOf(day.id) > -1) {
                            day.viewMode = 'Selected';
                        }
                        else {
                            day.viewMode = 'Read';
                        }
                    });
                });
        };

        $scope.isClean = function () {
            return angular.equals(original, $scope.itinerary);
        };

        $scope.addDayItinerary = function () {
            if (!$scope.itinerary.itineraryDayIds) {
                $scope.itinerary.itineraryDayIds = [];
            }
            if (!$scope.itinerary.itineraryDays) {
                $scope.itinerary.itineraryDays = [];
            }

            var cmd = { "description": "" };
            itineraryService.insertItineraryDay($scope.itinerary, cmd)
                .then(function (response) {
                    var newDay = {
                        "id": response.data.dayId,
                        "description": response.data.description,
                        "version": 0,
                        "viewMode": 'Edit'
                    };

                    $scope.itinerary.itineraryDayIds.push(response.data.dayId);
                    $scope.itinerary.itineraryDays.push(newDay);

                    $scope.reloadItinerary($scope.itinerary.id);

                    alerting.addSuccess('Add Day', "Successfully added a new day.");
                })
                .catch(alerting.errorHandler('API Error!', "Failed to add a new day."));
        }

        $scope.persistDayDescription = function (sender) {
            var dayEdited = sender.day;
            var elementId = "day_" + dayEdited.id;
            var element = document.getElementById(elementId);
            var newDayDescription = element.value;

            dayService.getDay(dayEdited.id,
                function (response) {
                    var day = response.data;
                    day.description = newDayDescription;

                    dayService.updateDay(day.id, day,
                        function (response) {
                            dayEdited.description = newDayDescription;
                            alerting.addSuccess('Update Day', "Successfully updated day.");
                        },
                        function (response) {
                            alerting.addResponseError(response, 'API Error', "Failed to update day description.");
                        });
                },
                function (response) {
                    alerting.addResponseError(response, 'API Error', "Failed to update day description.");
                })
                .catch(alerting.errorHandler('API Error', "Failed to update day description."));
        };

        $scope.persistDayOrder = function () {
            var needSaving = false;
            var numberOfDays = $scope.itinerary.itineraryDays.length;
            for (index = 0; index < numberOfDays; index++) {
                if ($scope.itinerary.itineraryDays[index].id !== $scope.itinerary.itineraryDayIds[index]) {
                    $scope.itinerary.itineraryDayIds[index] = $scope.itinerary.itineraryDays[index].id;
                    needSaving = true;
                }
            }

            if (needSaving === true) {
                itineraryService.reorderItineraryDays($scope.itinerary)
                    .then(function (response) {
                        alerting.addSuccess('Reorder Itinerary Days!', "Successfully re-order the Itinerary days.");
                        $scope.reloadItinerary($scope.itinerary.id); // to try to ensure version synchronisation
                    })
                    .catch(alerting.errorHandler('API Error!', "Failed to re-order the Itinerary days."));
            }
        };

        $scope.deleteDay = function (day) {

            itineraryService.removeDayFromItinerary($scope.itinerary, day,
                    function (response) {
                        //$location.path('/app/masterdata-brands');
                        $scope.reloadItinerary(itinerayId);
                        alerting.addSuccess('Add Itinerary', "Successfully removed the day from the Itinerary.");
                    },
                    function (response) {
                        $scope.validationErrors = alerting.getValidationErrors(response);

                        var uiMessage = "Failed to remove the day from the brand.";
                        if (response.data.exceptionMessage !== undefined) {

                            var json = response.data.exceptionMessage;

                            var expectionMessageJSON = JSON.parse(json);

                            uiMessage = uiMessage + "<p>" + expectionMessageJSON.Data.Message + "</p>";
                        }

                        alerting.addResponseError(response, 'Removing Day', uiMessage);
                    });
        };

        $scope.confirmDeleteDay = function (day) {
            confirmItineraryDayAction(day, 'delete').then($scope.deleteDay);
        }

        $scope.initialise = function () {
            $scope.setOriginal(itinerary);
        };

        $scope.initialise();
    };

    module.controller("ItineraryListCtrl", ItineraryListController);
    module.controller("ItineraryEditCtrl", ItineraryEditController);
    module.controller("itineraryViewCtrl", ItineraryViewController);

}(angular.module("journeyDesigner-app")));