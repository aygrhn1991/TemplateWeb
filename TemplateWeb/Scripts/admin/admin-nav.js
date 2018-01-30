var app = angular.module('app', []);
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
        $scope.sortType = {
            up: 'up',
            down: 'down'
        };
        $scope.LoadData();
    };
    $scope.LoadData = function () {
        window.LayerOpen();
        $http.post('/Admin/NavList_Get').success(function (d) {
            $scope.data = d;
            if (d.length == 0) {
                $('#dt').DataTable({
                    language: { url: '/Plugin/datatables/js/chinese.json' },
                    destroy: true,
                    bSort: false
                }).on('draw.dt', function () {
                    window.LayerClose()
                });
            } else {
                $scope.$on('ngRepeatFinished', function (event) {
                    $('#dt').DataTable({
                        language: { url: '/Plugin/datatables/js/chinese.json' },
                        destroy: true,
                        bSort: false
                    }).on('draw.dt', function () {
                        window.LayerClose()
                    });
                });
            }
        }).error(function () {
            console.log('http错误');
        });
    };
    $scope.Save = function (e) {
        window.LayerOpen();
        $http.post('/Admin/Nav_Add_Edit', e).success(function (d) {
            if (d == true) {
                alert('保存成功');
                self.location.reload();
            } else {
                alert('保存失败');
                window.LayerClose();
            }
        }).error(function () {
            console.log('http错误');
        });
    };
    $scope.Delete = function (e) {
        if (confirm('是否删除：' + e.title)) {
            window.LayerOpen();
            $http.post('/Admin/Nav_Delete', {
                id: e.id
            }).success(function (d) {
                if (d == true) {
                    alert('删除成功');
                    self.location.reload();
                } else {
                    alert('删除失败');
                    window.LayerClose();
                }
            }).error(function () {
                console.log('http错误');
            });
        }
    };
    $scope.SetEnable = function (e) {
        window.LayerOpen();
        e.enable = !e.enable;
        $http.post('/Admin/Nav_Add_Edit', e).success(function (d) {
            if (d == true) {
                alert('保存成功');
                self.location.reload();
            } else {
                alert('保存失败');
                window.LayerClose();
            }
        }).error(function () {
            console.log('http错误');
        });
    };
    $scope.SetSort = function (e, sortType) {
        window.LayerOpen();
        $http.post('/Admin/Nav_Sort', {
            id: e.id,
            sortType: sortType
        }).success(function (d) {
            if (d == true) {
                alert('保存成功');
                self.location.reload();
            } else {
                alert('保存失败');
                window.LayerClose();
            }
        }).error(function () {
            console.log('http错误');
        });
    };
    $scope.Init();
});
app.controller('navAdd', function ($scope, $http) {
    $scope.Init = function () {
        $scope.navModel = {
            id: 0,
            title: '',
            enable: false,
            mode: 0,
            url: null,
            page_id: null,
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
                window.LayerClose();
            }
        }).error(function (e) {
            console.log('http错误');
        });
    };
    $scope.Init();
});
app.controller('navContent', function ($scope, $http) {
    $scope.Init = function () {
        $scope.modeType = [
            { key: 0, value: 'url外链' },
            { key: 1, value: '单页' },
            { key: 2, value: '子导航' },
        ];
        $scope.id = parseInt(window.GetUrlParam('id'));
        window.LayerOpen();
        $http.post('/Admin/Nav_Get', {
            id: $scope.id
        }).success(function (d) {
            $scope.navModel = d;
            $scope.PageLoad();
            $scope.SubnavLoad();
        }).error(function () {
            console.log('http错误');
        });
    };
    $scope.SetMode = function (e) {
        $scope.navModel.mode = e.key;
    }
    $scope.PageLoad = function () {
        window.LayerOpen();
        $http.post('/Admin/PageList_Get').success(function (d) {
            $scope.pageList = d;
            $scope.navModelPageTitle = '暂未选择单页';
            d.forEach(function (e) {
                if (e.id == $scope.navModel.page_id) {
                    $scope.navModelPageTitle = e.title;
                }
            });
            window.LayerClose();
        }).error(function () {
            console.log('http错误');
        });
    };
    $scope.SetPage = function (e) {
        $scope.navModel.page_id = e.id;
        $scope.navModelPageTitle = e.title;
    };
    $scope.SubnavLoad = function () {
        window.LayerOpen();
        $http.post('/Admin/SubNavList_Get').success(function (d) {
            $scope.subnavList = d;
            window.LayerClose();
        }).error(function () {
            console.log('http错误');
        });
    };
    $scope.Save = function (e) {
        $http.post('/Admin/Nav_Add_Edit', $scope.navModel).success(function (d) {
            if (d == true) {
                alert('保存成功');
                self.location.href = '/Admin/NavList';
            } else {
                alert('保存失败');
                window.LayerClose();
            }
        }).error(function (e) {
            console.log('http错误');
        });
    };
    $scope.Init();
});
app.controller('subnavAdd', function ($scope, $http) {
    $scope.Init = function () {
        $scope.subnavModel = {
            id: 0,
            nav_id: parseInt(window.GetUrlParam('id')),
            title: '',
            enable: false,
            mode: 0,
            sort: null,
            page_id: null,
            url: null,
        };
    };
    $scope.Save = function () {
        window.LayerOpen();
        $http.post('/Admin/Subnav_Add_Edit', $scope.subnavModel).success(function (d) {
            if (d == true) {
                alert('保存成功');
                $('#subnav-add').modal('hide');
                $scope.SubnavLoad();
            } else {
                alert('保存失败');
                window.LayerClose();
            }
        }).error(function (e) {
            console.log('http错误');
        });
    };
    $scope.Init();
});