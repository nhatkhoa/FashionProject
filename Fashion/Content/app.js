(function () {
    "use strict";

    var app = angular.module("Store", []);
    
    // --- Home Controller
    app.controller("HomeCtrl", ['$scope', '$rootScope', '$http', function ($scope, $rootScope, $http) {
        $scope.message = "Nhật Khoa";
        // --- Khai báo đối tượng product
        $scope.view = {};
        $scope.view.name = "Nhật khoa";
        
        $scope.changeView = function(item_id) {
            $http.get("Home/GetProduct/" + item_id)
                .success(function(response) {
                    $scope.view = response;
                })
                .error(function() {
                    alert("Không thể kết nối đến hệ thống!")
                })
        }
        $scope.changeView();


    }])
})();