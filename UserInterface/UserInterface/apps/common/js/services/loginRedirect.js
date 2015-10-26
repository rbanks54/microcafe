(function (module) {

    var loginRedirect = function () {

        var loginUrl = "/login";
        var lastPath = "";

        this.setLoginUrl = function (value) {
            loginUrl = value;
        };

        this.$get = function ($q, $location) {

            return {

                responseError: function (response) {
                    if (response.status == 401) {
                        lastPath = $location.path();
                        $location.path(loginUrl);
                    }
                    return $q.reject(response);
                },

                redirectPreLogin: function () {
                    if (lastPath) {
                        $location.path(lastPath);
                        lastPath = "";

                    } else {
                        $location.path("/");
                    }
                }
            };
        };
    };

    module.provider("loginRedirect", loginRedirect);
    module.config(function ($httpProvider) {
        $httpProvider.interceptors.push("loginRedirect");
    });

}(angular.module("common")))