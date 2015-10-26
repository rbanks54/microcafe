(function(module) {

    var USERKEY = "utoken";

    var currentUser = function(localStorage){

        var saveUser = function () {
            localStorage.add(USERKEY, profile);
        };

        var removeUser = function () {
            localStorage.remove(USERKEY);
        };
        
        var initialize = function () {
            var user = {
                username: "",
                token: "",
                get loggedIn() {
                    return this.token ? true : false;
                }
            };

            var localUser = localStorage.get(USERKEY);
            if (localUser) {
                user.username = localUser.username;
                user.token = localUser.token;
            }
            return user;
        };

        var profile = initialize();
        
        return {
            save: saveUser,
            remove: removeUser,
            profile: profile
        };
    };

    module.factory("currentUser", currentUser);

}(angular.module("common")));