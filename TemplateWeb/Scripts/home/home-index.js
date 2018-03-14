﻿var app = angular.module('app', []);
app.controller('layout', function ($scope, $http) {
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
app.controller('index', function ($scope, $http) {
    $scope.Init = function () {
        $scope.LoadData();
    };
    $scope.LoadData = function () {
        $http.post('/Home/IndexContent_Get').success(function (d) {
            $scope.content = d;
        }).error(function () {
            console.log('http错误');
        });
    };
    $scope.Init();
});
app.controller('page', function ($scope, $http, $sce) {
    $scope.Init = function () {
        $scope.id = parseInt(window.GetUrlParam('id'));
        $scope.LoadData();
    };
    $scope.LoadData = function () {
        $http.post('/Home/Page_Get', {
            id: $scope.id
        }).success(function (d) {
            $scope.page = d;
            $scope.page.content = $sce.trustAsHtml($scope.page.content);
        }).error(function () {
            console.log('http错误');
        });
    };
    $scope.Init();
});
app.controller('newsList', function ($scope, $http) {
    $scope.Init = function () {
        $scope.id = parseInt(window.GetUrlParam('id'));
        $scope.LoadData();
    };
    $scope.LoadData = function () {
        $http.post('/Home/NewsList_Get', {
            id: $scope.id
        }).success(function (d) {
            $scope.newsList = d;
        }).error(function () {
            console.log('http错误');
        });
    };
    $scope.Init();
});