var app = angular.module('app', []);
app.controller('index', function ($scope, $http) {
    $scope.Init = function () {
        $scope.LoadData();
    };
    $scope.LoadData = function () {
        $http.post('/Home/IndexParam_Get').success(function (d) {
            $scope.param = d;
            $('.tp-partner').liMarquee();
            $('#favicon-icon').attr('href', $scope.param.param.favicon);
            document.title = $scope.param.param.sitename == null || $scope.param.param.sitename == '' ? '未设置标题' : $scope.param.param.sitename;
        }).error(function () {
            console.log('http错误');
        });
    };
    $scope.Init();
});