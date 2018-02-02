var app = angular.module('app', ['ngTable']);
app.controller('bannerList', function ($scope, $http, NgTableParams) {
    $scope.Init = function () {
        $scope.LoadData();
    };
    $scope.LoadData = function () {
        window.LayerOpen();
        $http.post('/Admin/PageList_Get').success(function (d) {
            $scope.data = d;
            $scope.dt = new NgTableParams({
                count: 10,
            }, {
                    counts: [10, 20, 50],
                    dataset: d,
                });
            window.LayerClose();
        }).error(function () {
            console.log('http错误');
            window.LayerClose();
        });
    };
    $scope.Delete = function (e) {
        if (confirm('是否删除：' + e.title)) {
            window.LayerOpen();
            $http.post('/Admin/Page_Delete', {
                id: e.id
            }).success(function (d) {
                if (d == true) {
                    alert('删除成功');
                    $scope.LoadData();
                } else {
                    alert('删除失败');
                    window.LayerClose();
                }
            }).error(function () {
                console.log('http错误');
                window.LayerClose();
            });
        }
    };
    $scope.Init();
});
app.controller('bannerAdd', function ($scope, $http) {
    $('#easyContainer').easyUpload({
        allowFileTypes: '*.jpg;*.png;*.gif;*.txt;*.pdf;*.exe;',//允许上传文件类型，格式';*.doc;*.pdf'
        note: '提示：最多上传5个文件，超出默认前五个，支持格式为：jpg、png、gif、txt、pdf',
        url: '/Plugin/easyupload/handler/UploadHandler.ashx',//上传文件地址
        
        successFunc: function (res) {
            console.log('成功回调', res);
        },//上传成功回调函数
        errorFunc: function (res) {
            console.log('失败回调', res);
        },//上传失败回调函数
        
    });
    //$scope.Init = function () {
    //    $scope.id = parseInt(window.GetUrlParam('id'));
    //    if ($scope.id == 0) {
    //        $scope.pageModel = {
    //            id: 0,
    //            title: '',
    //            content: '',
    //        };
    //    } else {
    //        window.LayerOpen();
    //        $http.post('/Admin/Page_Get', {
    //            id: $scope.id
    //        }).success(function (d) {
    //            $scope.pageModel = d;
    //            $('#summernote').summernote('code', d.content);
    //            window.LayerClose();
    //        }).error(function () {
    //            console.log('http错误');
    //            window.LayerClose();
    //        });
    //    }
    //};
    //$scope.Save = function () {
    //    window.LayerOpen();
    //    $scope.pageModel.content = $('#summernote').summernote('code');
    //    $http.post('/Admin/Page_Add_Edit', $scope.pageModel).success(function (d) {
    //        if (d == true) {
    //            alert('保存成功');
    //            self.location.href = '/Admin/PageList';
    //        } else {
    //            alert('保存失败');
    //            window.LayerClose();
    //        }
    //    }).error(function () {
    //        console.log('http错误');
    //        window.LayerClose();
    //    });
    //};
    //$('#summernote').summernote({
    //    lang: 'zh-CN',
    //    minHeight: '60%',
    //    callbacks: {
    //        onInit: function () {
    //            $scope.Init();
    //        },
    //        onImageUpload: function (files) {
    //            window.EditorImageUpload(files);
    //        }
    //    }
    //});
});