﻿(function () {
    var jsList = [
        "/apiconfig.js",
        "/scripts/polyfill/Object.assign.js",
        "/scripts/polyfill/DOMParser.parseFromString.js",
        "/scripts/cookies.js",
        "/scripts/guid.js",
        "/scripts/dateformat.js",
        "/scripts/jszip.js",
        "/scripts/linq.js",
        "/scripts/md5.js",
        "/scripts/vue.min.js",
        "/scripts/vue-plugins/vue-resource/vue-resource.js",
        "/scripts/element-ui/lib/index.js",
        "/scripts/vue-plugins/purchaserlist/JavaScript.js",
        "/scripts/vue-plugins/purchaserecord/JavaScript.js",
        "/scripts/vue-compnent/ckeditor/ckeditor/ckeditor.js",
        "/scripts/vue-compnent/ckeditor/ckeditor/config.js",
        "/scripts/vue-compnent/ckeditor/index.js",
        "/scripts/framework.js"
    ];

    for (var i = 0, item; item = jsList[i++];) {
        document.writeln('<script src="/wwwroot' + webConfig.apiServerAddress + item + '"></script>');
    }
})();

