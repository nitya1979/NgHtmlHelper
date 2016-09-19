(function () {
    var app = angular.module("NgHelperDemo");

    var customerController = function ($scope, customerService) {
        $scope.customer = new Customer();
        $scope.customer.Gender = "F";

        $scope.genderList = [{ code: "M", name: "Male" }, { code: "F", name: "Female" }];

        $scope.preferenecList = [ { prefId: "101", prefName: "Preference One" },
                                  { prefId: "102", prefName: "Preference Two" },
                                  { prefId: "103", prefName: "Preference Three" }];

        $scope.countryList = [{ countryCode: "US", countryName: "United States" },
                              { countryCode: "UK", countryName: "United Kingdom" },
                            { countryCode: "INR", countryName: "India" },
                            { countryCode: "CA", countryName: "Canada" }];

        $scope.dateOfBirthOptions = {
            //dateDisabled: disabled,
            formatYear: 'yy',
            startingDay: 1
        };

        $scope.dateOfBirthPopup = {
            opened: false
        };

        $scope.dateOfBirthOpen = function () {
            $scope.dateOfBirthPopup.opened = true;
        };
        
        $scope.isDisabled = true;
    };

    app.controller("customerController", customerController);
}());