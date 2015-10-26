(function(module) {

    var formEncode = function() {
        return function(data) {
            var pairs = [];
            for (var name in data) {
                pairs.push(encodeURIComponent(name) + '=' + encodeURIComponent(data[name]));
            }
            return pairs.join('&').replace(/%20/g, '+');
        };
    };

    module.factory("formEncode", formEncode);

}(angular.module("common")));