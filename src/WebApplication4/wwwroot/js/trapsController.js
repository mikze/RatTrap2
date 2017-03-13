(function () {
    "use strict";

    angular
        .module("app-traps")
        .controller("trapsController", trapsController);


    function trapsController($http) {    
        /* jshint validthis:true */
        var vm = this;

        vm.guwno = "jajco";

        vm.traps = [];

        vm.newTrap = {};

        vm.errorMessage = "";

        $http.get("/api/traps")
        .then(function (response) {
            angular.copy(response.data, vm.traps)
        },
        function (error) {
            vm.errorMessage = "Filed to load data" + error;
        });

        vm.addTrap = function () {

            $http.post("/api/traps", vm.newTrap)
        .then(function (response) {
            
            vm.traps.push(response.data);
            vm.newTrap = {};
        },
        function (error) {
            vm.errorMessage = "Filed to save new trap" + error;
        });
            
        };
    }
})();
