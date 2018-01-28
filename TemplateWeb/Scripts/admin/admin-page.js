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
app.controller('pageList', function ($scope, $http) {
    $scope.Init = function () {
        $scope.LoadData();
    };
    $scope.LoadData = function () {
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
app.controller('pageAdd', function ($scope, $http) {    
    $scope.Init = function () {
        $scope.id = parseInt(window.GetUrlParam('id'));
        if ($scope.id == 0) {
            $scope.pageModel = {
                id: $scope.id,
                title: '',
                content: ''
            };
        } else {
            $http.post('/Admin/Page_Get', {
                id: $scope.id
            }).success(function (d) {
                $scope.pageModel = {
                    id: d.id,
                    title: d.title,
                    content: d.content
                };
                $('#summernote').summernote('code', d.content);
            }).error(function (e) {
                console.log('http错误');
            });
        }
    };
    $scope.Save = function () {
        $scope.pageModel.content = $('#summernote').summernote('code');
        $http.post('/Admin/Page_Add_Edit', {
            id: $scope.pageModel.id,
            title: $scope.pageModel.title,
            content: $scope.pageModel.content
        }).success(function (d) {
            if (d == true) {
                alert('保存成功');
                self.location.href = '/Admin/PageList';
            } else {
                alert('保存失败');
            }
        }).error(function (e) {
            console.log('http错误');
        });
    };
    $('#summernote').summernote({
        lang: 'zh-CN',
        minHeight: '60%',
        callbacks: {
            onInit: function () {
                $scope.Init();
            },
            onImageUpload: function (files) {
                console.log(0);
                window.EditorImageUpload(files);
                // upload image to server and create imgNode...
                $('#summernote').summernote('insertImage', 'https://gss2.bdstatic.com/-fo3dSag_xI4khGkpoWK1HF6hhy/baike/c0%3Dbaike92%2C5%2C5%2C92%2C30/sign=acb64cc2542c11dfcadcb771024e09b5/a6efce1b9d16fdfa613e4dcfb78f8c5494ee7b45.jpg');
            }
        }
    });
});