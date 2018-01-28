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
    formData.append("file", file);
    console.log(data);
    $.ajax({
        data: data,
        type: "POST",
        url: "{:U('Test/upload')}",
        cache: false,
        contentType: false,
        processData: false,
        success: function (url) {
            $("#summernote").summernote('insertImage', url, 'image name'); // the insertImage API  
        }
    });
};