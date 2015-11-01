(function (module) {

    module.run(function ($rootScope, alerting) {
        $rootScope.$on("$stateChangeError", function (event, toState, toParams,
                                                      fromState, fromParams, error) {
            alerting.addDanger('State Change Error!', "Could not load " + toState.name);
        });
    });

}(angular.module("microcafe-app")));