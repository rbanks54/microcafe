(function (module) {

    var oauth = function () {

        var url = "http://localhost:8179/oauth2/token";
        var clientId = "099153c2625149bc8ecb3e85e03f0022";
        
        this.setUrl = function (newUrl) {
            url = newUrl;
        };

        this.setClientId = function (newClientId) {
            clientId = newClientId;
        };

        this.$get = function ($http, formEncode, currentUser) {

            var processToken = function(username) {
                return function(response) {
                    currentUser.profile.username = username;
                    currentUser.profile.token = response.data.access_token;
                    currentUser.save();
                    return username;
                }
            };

            var login = function(username, password) {

                var config = {
                    headers: {
                        "Content-Type": "application/x-www-form-urlencoded"
                    }
                }

                var data = formEncode({
                    username: username,
                    password: password,
                    grant_type: "password",
                    client_id: clientId
                });

                return $http.post(url, data, config)
                    .then(processToken(username));

            };

            var logout = function() {
                currentUser.profile.username = "";
                currentUser.profile.token = "";
                currentUser.remove();
            };

            return {
                login: login,
                logout: logout
            };
        }
    };

    module.config(function ($provide) {
        $provide.provider("oauth", oauth);
    });

}(angular.module("common")));