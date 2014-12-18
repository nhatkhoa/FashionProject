(function () {
    "use strict";

    var app = angular.module("Store", ['ngStorage', 'angular.filter']);
    
    // --- Home Controller
    app.controller("HomeCtrl", ['$rootScope', '$http','$localStorage', function ($rootScope, $http, $localStorage) {
        // --- Khai báo đối tượng controller
        var vm = this;
        
        // --- Nếu giỏ hàng localStorage không tồn tại thì tạo mới
        if ($localStorage.carts === undefined) {
            $localStorage.carts = {};
            $localStorage.carts.items = [];
            $localStorage.carts.count = 0;
            $localStorage.carts.total = 0;
        }
        // --- Khai báo dữ liệu trống cho view popup
        vm.view = {};
        vm.view.Name = "Chưa có thông tin";
        vm.view.Cost = "Chưa có thông tin";
        vm.view.Info = "Chưa có thông tin";
        vm.view.Size = [];
        
        // --- Khai báo danh sách shopping cart và lấy dữ liệu ra (nếu có)
        vm.carts = $localStorage.carts;

        // ---- Lắng nghe yêu cầu mở popup từ ShowCtrl
        $rootScope.$on("popup", function (item_id) {
            // --- Lấy id sản phẩm select
            var id = $localStorage.selectItem;
            
            // --- Sử dụng ajax cập nhật lại nội dung popup
            $http.get("Home/GetProduct/" + id)
                .success(function (response) {
                    vm.view = response[0];
                    
                    //--- Tùy chọn lại danh sách size
                    vm.view.Sizes = response[0].Sizes.split(' ')
                    console.log(vm.view.Sizes);
                })
                .error(function() {
                    alert("Kết nối thất bại !");
                });

        });
        
        // ---- Lắng nghe yêu cầu mở popup từ ShowCtrl
        $rootScope.$on("cart", function(item) {
            // --- Cập nhật lại giỏ hàng
            vm.carts = $localStorage.carts;
        });

        // --- Hàm thêm vào giỏ hàng
        vm.addCart = function(item) {
            alert("Thêm giỏ hàng !");
        };


    }]);
    
    app.controller("ShowCtrl", ['$rootScope', '$http', '$localStorage', function ($rootScope, $http, $localStorage) {
        var show = this
        // --- Khai báo danh sách products
        show.products = [];
        
        // --- Hàm load dữ liệu ban đầu
        show.loadData = function () {
             // --- Sử dụng cache tạm thời nếu không load được dữ liệu
            show.products = $localStorage.data; 
            $http.get("Home/GetProducts")
                .success(function (response) {
                    // --- Nếu không nhận được dữ liệu thi return (sử dụng cache)
                    if (response === null)
                        return
                    // --- Nếu thaành công cập nhật lại danh sách sản phẩm
                    show.products = response;
                    // --- Cache dữ liệu
                    $localStorage.data = response;
                    console.log(response);
                })
                .error(function () {
                    alert("Không thể kết nối đến hệ thống!");
                })
        }

        show.loadData();
        
        // --- Hàm show popup chi tiết sản phẩm
        show.popup = function (item_id) {;
            // --- Lưu item id vào localStorage
            $localStorage.selectItem = item_id;
            // --- Thông báo mở popup
            $rootScope.$broadcast('popup', item_id);
        };
        
        // --- Hàm thêm vào giỏ hàng
        show.cart = function (item) {
            
            // --- Thêm sản phẩm vào giỏ hàng LocalStorage
            $localStorage.carts.push(item);
            // --- Thông báo broadcast cập nhật giỏ hàng 
            $rootScope.$broadcast('cart', item);

        };
        
    }])
})();