﻿var app = angular.module('app', []);
app.directive('onRenderFinished', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attr, controller) {
            if (scope.$last === true) {
                $timeout(function () {
                    scope.$emit('ngRepeatFinished');
                });
            }
        }
    };
});
app.controller('navList', function ($scope, $http) {
    $scope.Init = function () {
        $scope.LoadData();
    };
    $scope.LoadData = function () {
        window.LayerOpen();
        $http.post('/Admin/PageList_Get').success(function (d) {
            $scope.data = d;
            if (d.length == 0) {
                window.DrawTable('#dt');
            } else {
                $scope.$on('ngRepeatFinished', function (event) {
                    window.DrawTable('#dt');
                });
            }
        }).error(function (e) {
            console.log('http错误');
        });
    };
    $scope.delete = function (d) {
        if (confirm('是否删除：' + d.title)) {
            window.LayerOpen();
            $http.post('/Admin/Page_Delete', {
                id: d.id
            }).success(function (d) {
                if (d == true) {
                    alert('删除成功');
                    self.location.reload();
                } else {
                    alert('删除失败');
                }
            }).error(function (e) {
                console.log('http错误');
            });
        }
    };
    $scope.Init();
});
app.controller('navAdd', function ($scope, $http) {
    $scope.Init = function () {
        $scope.navModel = {
            id: 0,
            title: '',
            enable: false,
            has_sub_nav: false,
            url: null,
            pageid: null,
            sort: null,
        };
    };
    $scope.Save = function () {
        $http.post('/Admin/Nav_Add_Edit', $scope.navModel).success(function (d) {
            if (d == true) {
                alert('保存成功');
                self.location.reload();
            } else {
                alert('保存失败');
            }
        }).error(function (e) {
            console.log('http错误');
        });
    };
    $scope.Init();
});