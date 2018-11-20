$(function () {
    $.ajax({
        url: '/home/jssdkconfig?url=' + window.location.href,
        type: 'POST',
        success: function (data) {
            wx.config({
                debug: false,
                appId: data.appId,
                timestamp: data.timestamp,
                nonceStr: data.nonceStr,
                signature: data.signature,
                jsApiList: ['updateAppMessageShareData', 'updateTimelineShareData','onMenuShareTimeline','onMenuShareAppMessage']
            });
        }
    });
    wx.ready(function () {
        // wx.updateAppMessageShareData({
        //     title: '龙江问医',
        //     desc: '龙江问医',
        //     link: 'http://' + window.location.host + '/dy/oauth/requestcode',
        //     imgUrl: 'http://' + window.location.host + '/dy/static/img/logo.jpg',
        // }, function (res) {
        // });
        // wx.updateTimelineShareData({
        //     title: '龙江问医',
        //     link: 'http://' + window.location.host + '/dy/oauth/requestcode',
        //     imgUrl: 'http://' + window.location.host + '/dy/static/img/logo.jpg',
        // }, function (res) {
        // });
        wx.onMenuShareTimeline({
            title: '同歌善知教育',
            link: 'http://' + window.location.host + '/home/index',
            imgUrl: 'http://' + window.location.host + '/Upload/setting/logo-2018-05-05-10-44-48-b95d9c85bdec457f94c94fe2b8abd403.png',
            success: function () {
            }
        });
        wx.onMenuShareAppMessage({
            title: '同歌善知教育',
            desc: '同歌善知教育平台',
            link: 'http://' + window.location.host + '/home/index',
            imgUrl: 'http://' + window.location.host + '/Upload/setting/logo-2018-05-05-10-44-48-b95d9c85bdec457f94c94fe2b8abd403.png',
            type: 'link',
            dataUrl: '',
            success: function () {
            }
        });
    });
    wx.error(function (res) {
        console.log('配置读取失败：' + res);
    });


});

