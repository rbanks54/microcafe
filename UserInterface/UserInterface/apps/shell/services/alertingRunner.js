(function (module) {

    module.run(function ($rootScope, alerting) {
        $rootScope.$on("alertAddedEvent", function () {
            //alerting.addDanger("Could not load " + toState.name);
            //alert('something added!');

            // Options for the 
            var opts = {
                "closeButton": true,
                "debug": false,
                "positionClass": "toast-top-right",
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "5000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            };

            var dangerOpts = {
                "closeButton": true,
                "debug": false,
                "positionClass": "toast-top-right",
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "-1",
                "timeOut": "-1",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            };

            alerting.currentAlerts.forEach(function (alert) {

                // Display the message
                switch (alert.type) {
                    case "info":
                        {
                            toastr.info(alert.message, alert.title, opts);
                            break;
                        }
                    case "warning":
                        {
                            toastr.warning(alert.message, alert.title, opts);
                            break;
                        }
                    case "success":
                        {
                            toastr.success(alert.message, alert.title, opts);
                            break;
                        }
                    default:
                        {
                            toastr.error(alert.message, alert.title, dangerOpts);
                        }
                }

                // remove the message
                alerting.removeAlert(alert);
            });

        });
    });

}(angular.module("microcafe-app")));