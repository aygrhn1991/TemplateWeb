var app = angular.module('app', []);
app.controller('index', function ($scope, $http) {
    $scope.Init = function () {
        $scope.LoadData();
    };
    $scope.LoadData = function () {
        $http.post('/Home/IndexParam_Get').success(function (d) {
            $scope.param = d;
        }).error(function () {
            console.log('http错误');
        });
    };
    $scope.Init();
});