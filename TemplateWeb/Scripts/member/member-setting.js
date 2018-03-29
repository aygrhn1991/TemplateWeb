var app = angular.module('app', ['ngTable']);
app.controller('info', function ($scope, $http, NgTableParams) {
    $scope.Init = function () {
        $scope.LoadData();
    };
    $scope.LoadData = function () {
        window.LayerOpen();
        $http.post('/Member/Info_Get').success(function (d) {
            $scope.infoModel = d;
            window.LayerClose();
        }).error(function () {
            console.log('http错误');
            window.LayerClose();
        });
    };
    $scope.Save = function (e) {
        window.LayerOpen();
        $http.post('/Member/Info_Add_Edit', e).success(function (d) {
            if (d == true) {
                alert('保存成功');
                $scope.LoadData();
            } else {
                alert('保存失败');
                window.LayerClose();
            }
        }).error(function () {
            console.log('http错误');
            window.LayerClose();
        });
    };
    $scope.Init();
});