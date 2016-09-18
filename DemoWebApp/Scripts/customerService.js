(function () {
    var customerService = function ($http) {

        var getCustomer = function (id) {

            return $http.get("/api/Customer/" + id).then(function (response) {
                return response.data;
            });
        };

        return {
            getCustomer : getCustomer
        };
    };

    var app = angular.module("NgHelperDemo");
    app.factory("customerService", customerService);
}());