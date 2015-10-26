(function(module) {

    module.config(function ($provide) {

        $provide.decorator("$exceptionHandler", function ($delegate, $injector) {
            return function (exception, cause) {
                $delegate(exception, cause);

                var alerting = $injector.get("alerting");
                alerting.addDanger('Unhandled Exception', exception.message);
            };
        });

    });

}(angular.module("common")));