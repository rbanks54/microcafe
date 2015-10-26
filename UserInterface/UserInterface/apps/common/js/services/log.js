(function (module) {

    module.config(function ($provide) {
        $provide.decorator("$log", function ($delegate, $injector) {

            var log = function (message) {
                var alerting = $injector.get("alerting");
                alerting.addInformation('Info', message);
                return $delegate.log(message);
            };

            var warn = function (message) {
                var alerting = $injector.get("alerting");
                alerting.addWarning('Warning', message);
                return $delegate.info(message);
            };

            var info = function (message) {
                var alerting = $injector.get("alerting");
                alerting.addInformation('Info', message);
                return $delegate.info(message);
            };

            var error = function (message) {
                var alerting = $injector.get("alerting");
                alerting.addDanger('Error', message);
                return $delegate.info(message);
            };

            return {
                log: log,
                warn: warn,
                info: info,
                error: error
            };
        });
    });

}(angular.module("common")));