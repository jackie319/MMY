/*
*检查用户是否跳出框架
*/
(function (w) {
    var sitePath = "/wwwroot/";
    var pageLoaderPage = "index.html";
    var mainPage = "main.html";
    var loginPage = "login.html";


    //没有加载器
    if (!w.$f.ui) {
        location.href = sitePath + pageLoaderPage + "?returnUrl=" + location.pathname;
        return;
    }

    /**
     * 是否需要菜单
     * @param {} url 
     * @returns {} 
     */
    function isNeedMenu(url) {
        if (url.indexOf(pageLoaderPage) > -1) {
            return false;
        }
        else if (url.indexOf(mainPage) > -1) {
            return false;
        }
        else if (url.indexOf(loginPage) > -1) {
            return false;
        }
        return true;
    }

    //有加载器加载页面
    var returnUrl = w.$f.getQueryString("returnUrl");
    if (w.$f.ui && returnUrl) {
        if (isNeedMenu(returnUrl)) {
            //需要菜单
            w.$f.ui.loadPage(sitePath + mainPage + "?returnUrl=" + returnUrl, function () {
            });
        } else {
            //不需要菜单
            w.$f.ui.loadPage(returnUrl);
        }
    } else {
        //默认加载登录页面
        w.$f.ui.loadPage(sitePath + loginPage);
    }
})(window)