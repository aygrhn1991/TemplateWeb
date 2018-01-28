window.GetUrlParam = function (name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null)
        return unescape(r[2]);
    return null;
}
window.DrawTable = function (id) {
    $(id).DataTable({
        language: { url: '/Plugin/datatables/js/chinese.json' },
        destroy: true
    }).on('draw.dt', function () {
        console.log('ok');
    });
};
window.EditorImageUpload = function (files) {
    var formData = new FormData();
    for (var i = 0; i < files.length; i++) {
        formData.append("file[]", files[i]);
    }
    $.ajax({
        data: formData,
        type: "POST",
        url: "/Plugin/summernote/handler/UploadHandler.ashx",
        cache: false,
        contentType: false,
        processData: false,
        success: function (url) {
            console.log(url);
            //$("#summernote").summernote('insertImage', url, 'image name'); // the insertImage API  
        }
    });
};