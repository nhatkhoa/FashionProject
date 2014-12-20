(function () {
    "use strict";

    var app = angular.module("Store", ['ngStorage', 'angular.filter']);
    
    // --- Home Controller
    app.controller("HomeCtrl", ['$rootScope', '$http','$localStorage', function ($rootScope, $http, $localStorage) {
        // --- Khai báo đối tượng controller
        var vm = this;
        vm.clear = function () {
            $localStorage.carts = {};
            $localStorage.carts.items = [];
            $localStorage.carts.num = [];
            $localStorage.carts.count = 0;
            $localStorage.carts.total = 0;
            vm.carts = $localStorage.carts;

        }
        // --- Nếu giỏ hàng localStorage không tồn tại thì tạo mới
        if ($localStorage.carts === undefined) {
            vm.clear();
        }
        
        // --- Khai báo dữ liệu trống cho view popup
        vm.view = {};
        vm.view.Name = "Chưa có thông tin";
        vm.view.Cost = "Chưa có thông tin";
        vm.view.Info = "Chưa có thông tin";
        vm.view.Size = [];
        
       
        // --- Khai báo danh sách shopping cart và lấy dữ liệu ra (nếu có)
        vm.carts = $localStorage.carts;
        
        // --- Hàm xóa phần tử trong carts
        vm.remove = function (item) {
            // --- Cập nhật lại tổng tiền và số lượng
            vm.carts.count--;
            vm.carts.total = vm.carts.total - item.Cost;
            // --- Xóa sản phẩm khỏi giỏ
            vm.carts.items.splice(vm.carts.items.indexOf(item), 1);
            // --- Lưu vào bộ nhớ
            $localStorage.carts = vm.carts;
        }

        // ---- Lắng nghe yêu cầu mở popup từ ShowCtrl
        $rootScope.$on("popup", function (item_id) {
            // --- Lấy id sản phẩm select
            var id = $localStorage.selectItem;
            
            // --- Sử dụng ajax cập nhật lại nội dung popup
            $http.get("../../Home/GetProduct/" + id)
                .success(function (response) {
                    vm.view = response[0];
                    vm.view.thumb = vm.view.Images[0];
                    //--- Tùy chọn lại danh sách size
                    vm.view.Sizes = response[0].Sizes.split(' ')
                    
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

      
        

    }]);

    app.controller("ShowCtrl", ['$rootScope', '$http', '$localStorage', function ($rootScope, $http, $localStorage) {
        var show = this

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
        show.popup = function (item_id) {
            ;
            // --- Lưu item id vào localStorage
            $localStorage.selectItem = item_id;
            // --- Thông báo mở popup
            $rootScope.$broadcast('popup', item_id);
        };

        // --- Hàm thêm vào giỏ hàng
        show.cart = function (item) {
            
            // --- Lấy giỏ hàng từ bộ nhớ
            var carts = $localStorage.carts;
            // --- Lấy index của item (nếu có tồn tại)
            var i = carts.items.indexOf(item);
            // --- Nếu i === -1 : không tồn tại sản phẩm trong giỏ
            if (i === -1) {
                // --- Thêm sản phẩm vào giỏ và lấy index dùng để cập nhật số lượng
                var index = carts.items.push(item);
                // --- Số lượng ứng với item ban đầu là 1
                carts.num.push(1);
                // --- Tăng số lượng sản phẩm
                carts.count = carts.count + 1;
                // --- Tăng tổng tiền
                carts.total = carts.total + int.parse(item.Cost);
            } else {
                // --- Cập nhật số lượng và tổng tiền
                carts.num[i]++;
                // --- Tăng số lượng sản phẩm
                carts.count = carts.count + 1;
                // --- Tăng tổng tiền
                carts.total = carts + item.Cost;
            }

          
            // --- Cập nhật lại vào bộ nhớ
            $localStorage.carts = carts;

            // --- Thông báo broadcast cập nhật giỏ hàng 
            $rootScope.$broadcast('cart', item);

        };
        

        

    }]);
    app.controller("ProductCtrl", ['$scope', '$rootScope', '$http', '$localStorage', function ($scope, $rootScope, $http, $localStorage) {
        var pro = this
        pro.selectPage = 1;
        pro.$key = "";
        // --- Khai báo danh sách products
        //pro.products = [];

        // --- Hàm load dữ liệu ban đầu
        pro.loadData = function () {
            $http.get("../../SanPham/Get")
                .success(function (response) {                  
                    // --- Khi thành công : Khởi tạo danh sách sản phẩm
                    pro.products = response;
                })
                .error(function () {
                    alert("Không thể kết nối đến hệ thống!");
                })
        }

        pro.loadData();

        
        // --- Hàm load dữ liệu ban đầu
        pro.loadThem = function () {
            $http.get("../../SanPham/Get")
                .success(function(response) {
                    // --- Nếu không nhận được dữ liệu thi return (sử dụng cache)
                    if (response === null)
                        return;
                    // --- Nếu thaành công thêm vào danh sách mới cập nhật
                    angular.forEach(response, function(item) {
                        pro.products.push(item);
                    });
                    

                });

        }
        // --- Hàm pro popup chi tiết sản phẩm
        pro.popup = function (item_id) {
            ;
            // --- Lưu item id vào localStorage
            $localStorage.selectItem = item_id;
            // --- Thông báo mở popup
            $rootScope.$broadcast('popup', item_id);
        };

        // --- Hàm thêm vào giỏ hàng
        pro.cart = function (item) {

            // --- Lấy giỏ hàng từ bộ nhớ
            var carts = $localStorage.carts;
            // --- Lấy index của item (nếu có tồn tại)
            var i = carts.items.indexOf(item);
            // --- Nếu i === -1 : không tồn tại sản phẩm trong giỏ
            if (i === -1) {
                // --- Thêm sản phẩm vào giỏ và lấy index dùng để cập nhật số lượng
                var index = carts.items.push(item);
                // --- Số lượng ứng với item ban đầu là 1
                carts.num.push(1);
                // --- Tăng số lượng sản phẩm
                carts.count = carts.count + 1;
                // --- Tăng tổng tiền
                carts.total = carts.total + int.parse(item.Cost);
            } else {
                // --- Cập nhật số lượng và tổng tiền
                carts.num[i]++;
                // --- Tăng số lượng sản phẩm
                carts.count = carts.count + 1;
                // --- Tăng tổng tiền
                carts.total = carts + item.Cost;
            }


            // --- Cập nhật lại vào bộ nhớ
            $localStorage.carts = carts;

            // --- Thông báo broadcast cập nhật giỏ hàng 
            $rootScope.$broadcast('cart', item);

        };


    }]);




})();