(function (module) {

    var addToken = function (currentUser, $q) {

        var request = function (config) {
            if (currentUser.profile.loggedIn) {
                config.headers.Authorization = "Bearer " + currentUser.profile.token;
            }
            return $q.when(config);
        };

        return {
            request: request
        }
    };

    module.factory("addToken", addToken);
    module.config(function ($httpProvider) {
        $httpProvider.interceptors.push("addToken");
    });

}(angular.module("common")));