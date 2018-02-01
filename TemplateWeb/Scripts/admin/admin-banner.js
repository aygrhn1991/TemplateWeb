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
    $("#demo").zyUpload({
        width: "650px",                 // 宽度
        height: "400px",                 // 宽度
        itemWidth: "120px",                 // 文件项的宽度
        itemHeight: "100px",                 // 文件项的高度
        url: "/Plugin/webuploader/handler/UploadHandler.ashx",  // 上传文件的路径
        multiple: true,                    // 是否可以多个文件上传
        dragDrop: true,                    // 是否可以拖动上传文件
        del: true,                    // 是否可以删除文件
        finishDel: false,  				  // 是否在上传文件完成后删除预览
        /* 外部获得的回调接口 */
        onSelect: function (files, allFiles) {                    // 选择文件的回调方法
            console.info("当前选择了以下文件：");
            console.info(files);
            console.info("之前没上传的文件：");
            console.info(allFiles);
        },
        onDelete: function (file, surplusFiles) {                     // 删除一个文件的回调方法
            console.info("当前删除了此文件：");
            console.info(file);
            console.info("当前剩余的文件：");
            console.info(surplusFiles);
        },
        onSuccess: function (file) {                    // 文件上传成功的回调方法
            console.info("此文件上传成功：");
            console.info(file);
        },
        onFailure: function (file) {                    // 文件上传失败的回调方法
            console.info("此文件上传失败：");
            console.info(file);
        },
        onComplete: function (responseInfo) {           // 上传完成的回调方法
            console.info("文件上传完成");
            console.info(responseInfo);
        }
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