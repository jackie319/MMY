(function () {
    var cssList = [
        "script/mint-ui/lib/style.min.css",
        "css/schema.default.css",
        "css/theme/default.css",
        "css/app.css"
    ];

    for (var i = 0, item; item = cssList[i++];) {
        document.writeln('<link href="' +  item + '" rel="stylesheet"/>');
    }
})();

