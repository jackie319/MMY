(function () {
    var jsList = [
        "script/polyfill/Object.assign.js",
        "script/polyfill/DOMParser.parseFromString.js",
        "script/cookies.js",
        "script/guid.js",
        "script/dateformat.js",
        "script/jszip.js",
        "script/linq.js",
        "script/md5.js",
        "script/vue.min.js",
        "script/vue-plugins/vue-resource/vue-resource.js",
        "script/mint-ui/lib/index.js",
        "script/framework.js",
        "script/containerBridgeConfig.js",
        "script/containerBridge.min.js"
    ];

    for (var i = 0, item; item = jsList[i++];) {
        document.writeln('<script src="' + item + '"></script>');
    }
})();

