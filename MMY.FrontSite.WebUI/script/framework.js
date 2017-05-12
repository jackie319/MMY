(function (w) {
    /**
    * @constructor Framework
    * @returns {} 
    */
    var Framework = function () {
        this.debug = false;
        this.isPing = false;
        this.timerId = null;
        this.pingInterval = 10 * 60 * 1000;
        this.ui = null;
        //http://elemefe.github.io/mint-ui/#/
        //http://mint-ui.github.io/#!/zh-cn
        this.jsComponents = {
            "mt-toast": MINT.Toast,
            "mt-indicator": MINT.Indicator,
            "mt-loadmore": MINT.Loadmore,
            "mt-infinite-scroll": MINT.InfiniteScroll,
            "mt-message-box": MINT.MessageBox,
            "mt-actionsheet": MINT.Actionsheet,
            "mt-popup": MINT.Popup,
            "mt-swipe": MINT.Swipe,
            "mt-lazyload": MINT.Lazyload,
            "mt-range": MINT.Range,
            "mt-progress": MINT.Progress,
            "mt-picker": MINT.Picker,
            "mt-datetime-picker": MINT.DatetimePicker,
            "mt-index-list": MINT.IndexList,
            "mt-palette-button": MINT.PaletteButton
        };
        this.cssComponents = {
            "mt-header": MINT.Header,
            "mt-tabbar": MINT.Tabbar,
            "mt-navbar": MINT.Navbar,
            "mt-button": MINT.Button,
            "mt-cell": MINT.Cell,
            "mt-cell-swipe": MINT.CellSwipe,
            "mt-spinner": MINT.Spinner,
            "mt-tab-container": MINT.TabContainer,
            "mt-search": MINT.Search
        };
        this.formComponents = {
            "mt-switch": MINT.Switch,
            "mt-checklist": MINT.Checklist,
            "mt-radio": MINT.Radio,
            "mt-field": MINT.Field,
            "mt-badge": MINT.Badge
        };
        this.allCompomets = {};
        for (var i=0, g; g = [this.jsComponents, this.cssComponents, this.formComponents][i++];) {
            for (var p in g) {
                if (g.hasOwnProperty(p)) {
                    this.allCompomets[p] = g[p];
                }
            }
        }
    };

    /**
     * 用户登录
     * @param {} response 
     * @param {} fnCallBack 
     * @returns {} 
     */
    Framework.prototype.login = function (user) {
        //user:{
        //    "Username": "admin",
        //    "UserId": 1,
        //    "RoleName": "SystemAdmin",
        //    "SessionName": "DMPSKey",
        //    "SessionKey": "d4791bb351d6c459b3eb5a232a0d27d6cd93c826",
        //    "ReturnUrl": "&username=admin&token=597f5441e7d174b607873874ed54b974674986ad543e7458e28a038671c9f64c"
        //}
        var me = this;
        me.setUser(user);
        me.ui.loadPage("/Home/Index");
        me.ping();
    };

    /**
     * 用户退出
     * @returns {} 
     */
    Framework.prototype.logout = function () {
        var me = this;
        localStorage.clear();
        clearInterval(me.timerId);
        me.ui.loadPage("/Home/Login");
        me.ajax({ url: "/api/Auth/Logout" });
    };

    /**
     * ping pong
     * @returns {} 
     */
    Framework.prototype.ping = function () {
        var me = this;
        if (!me.isPing) {
            me.isPing = true;
            me.ajax({ url: "/api/Auth/Ping" });
            me.timerId = setInterval(function () {
                me.ajax({ url: "/api/Auth/Ping" });
            }, me.pingInterval);
        }
    };

    /**
     * 获取用户SessionKey
     * @returns {} 
     */
    Framework.prototype.getSessionKey = function () {
        var me = this;
        return me.getUser() ? me.getUser().SessionKey : null;
    };

    /**
     * 设置用户信息
     * @param {object} user 
     * @returns {void} 
     */
    Framework.prototype.setUser = function (user) {
        localStorage["DMP_User"] = JSON.stringify(user);
    };

    /**
     * 获取用户信息
     * @returns {object} 
     */
    Framework.prototype.getUser = function () {
        return JSON.parse(localStorage["DMP_User"], function (k, v) {
            if (/\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}/.test(v)) {
                //2016-09-18T03:46:11.893Z
                return new Date(v.replace("T", ""));
            }
            else if (/\/Date\(-?\d+\)\//.test(v)) {
                //Microsoft json Date \/Date(1450800000000)\/ \/Date(-62135596800000)\/
                return new Date(parseInt(v.substring(6)));
            }
            return v;
        });
    };

    /**
     * 打印日志
     * @param {} text 
     * @param {} type 
     * @returns {} 
     */
    Framework.prototype.log = function (text, type) {
        var me = this;
        if (!type) type = "log";
        if (me.debug) {
            console[type](text);
        }
    };

    /**
     * 弹出对话框
     * @param {string} text 
     * @returns {void} 
     */
    Framework.prototype.alert = function (text, fnOk) {
        if (this.ui) {
            this.ui.$messagebox.alert(text, "", {
                confirmButtonText: '确定',
                callback: fnOk
            });
        } else {
            alert(text);
        }
    };

    /**
     * 确定
     * @param {string} text 
     * @param {function} fnOk 
     * @param {function} fnCancel 
     * @returns {void} 
     */
    Framework.prototype.confirm = function (text, fnOk, fnCancel) {
        this.ui.$messagebox.confirm(text, '', {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            type: 'info'
        }).then(fnOk).catch(fnCancel);
    };

    /**
     * 输入
     * @param {string} text 
     * @param {function} fnOk 
     * @param {function} fnCancle 
     * @returns {void} 
     */
    Framework.prototype.prompt = function (text, fnOk, fnCancel) {
        this.ui.$messagebox.prompt(text, '', {
            confirmButtonText: '确定',
            cancelButtonText: '取消'
        }).then(fnOk).catch(fnCancel);
    };


    /**
     * ajax请求
     * @param {} options 
     * @returns {} 
     */
    Framework.prototype.ajax = function (options) {
        var me = this;
        var accept = {
            "html": "text/html",
            "json": "application/json",
            "text": "text/plain"
        };
        var settings = Object.assign({
            url: "",
            method: "GET",
            data: null,
            accept: accept["json"],
            progress: function (progressEvent) { },
            success: function (body, loadEvent) { },
            error: function (body, errorEvent) {
                var request = errorEvent.target;
                if (request.status === 500 && /application\/json/i.test(request.getResponseHeader("Content-Type"))) {
                    me.errorHander(JSON.parse(body));
                }
            },
            complete: function (body, loadendEvent) { }
        }, options);

        var xhr = new XMLHttpRequest();
        xhr.addEventListener("load", function (loadEvent) {
            if (this.status === 200) {
                settings.success(this.responseText, loadEvent);
            } else {
                settings.error(this.responseText, loadEvent);
            }
        });
        xhr.addEventListener("loadend", function (loadendEvent) {
            settings.complete(this.responseText, loadendEvent);
        });
        xhr.addEventListener("progress", function (progressEvent) {
            settings.progress(this.responseText, progressEvent);
        });
        xhr.addEventListener("error", function (errorEvent) {
            settings.error(this.responseText, errorEvent);
        });
        xhr.open(settings.method, settings.url);
        xhr.setRequestHeader("Accept", accept);
        xhr.send(settings.data);
    };

    /**
     * @method setUrl
     * @param {string} url 
     * @returns {void} 
     */
    Framework.prototype.setUrl = function (url) {
        if (typeof url === "string" && url !== "") {
            history.pushState(null, "", location.origin + url);
        }
    };

    /**
     * @method getPageComponent
     * @param {string} url 
     * @param {function} fn 
     * @returns {object} 
     */
    Framework.prototype.getPageComponent = function (url, fn, fnError) {
        var me = this;

        me.setUrl(url);

        try {
            me.ajax({
                url: url,
                success: function (body) {
                    try {
                        var dp = new DOMParser();
                        var dom = dp.parseFromString(body, "text/html");
                        var div = dom.querySelector("body > div");
                        var script = dom.querySelector("body > script");
                        var template = div ? div.outerHTML : '<div><h1 class="horizontal-vertical-center">建设中……</span></div>';
                        var options = me.getVueOptions(script ? script.innerHTML : "");

                        if (options == null) {
                            options = {};
                        }

                        var component = {
                            template: template
                        };
                        component.template = template;
                        if (options.data) {
                            component.data = function () { return options.data; }
                        } else {
                            component.data = function () { return {}; }
                        }
                        me.convertToVueOptions(component, options);
                        fn(component);
                    } catch (ex) {
                        fnError(ex);
                    }
                },
                error: function (body, event) {
                    var xhr = event.target;
                    fn({
                        template: '<div class="horizontal-vertical-center">' +
                                    '<h1>{{status}}&nbsp;{{statusText}}</h1>' +
                                    '<h2 class="horizontal-vertical-center">{{message}}</h2>' +
                                  '</div>',
                        data: function () {
                            return {
                                status: xhr.status,
                                statusText: xhr.statusText,
                                message: ""
                            };
                        }
                    });
                }
            });
        } catch (e) {
            fnError(e);
        }
    };

    /**
     * 
     * @param out {object} compoment 
     * @param {object} options 
     * @returns {void} 
     */
    Framework.prototype.convertToVueOptions = function (component, options) {
        for (var propertyName in options) {
            if (options.hasOwnProperty(propertyName)) {
                if (propertyName.toLowerCase() === "template") {
                    continue;
                }
                else if (propertyName.toLowerCase() === "data") {
                    continue;
                }
                    /*Bug:js需要手动赋值?*/
                else if (propertyName.toLowerCase() === "methods") {
                    component[propertyName] = options["methods"];
                }
                else if (propertyName.toLowerCase() === "props") {
                    component[propertyName] = options["props"];
                }
                else if (propertyName.toLowerCase() === "propsData") {
                    component[propertyName] = options["propsData"];
                }
                else if (propertyName.toLowerCase() === "computed") {
                    component[propertyName] = options["computed"];
                }
                else if (propertyName.toLowerCase() === "watch") {
                    component[propertyName] = options["watch"];
                }
                    //选项 / DOM
                else if (propertyName.toLowerCase() === "render") {
                    component[propertyName] = options["render"];
                }
                    //选项 / 生命周期钩子
                else if (propertyName.toLowerCase() === "beforeCreate") {
                    component[propertyName] = options["beforeCreate"];
                }
                else if (propertyName.toLowerCase() === "created") {
                    component[propertyName] = options["created"];
                }
                else if (propertyName.toLowerCase() === "beforeMount") {
                    component[propertyName] = options["beforeMount"];
                }
                else if (propertyName.toLowerCase() === "mounted") {
                    component[propertyName] = options["mounted"];
                }
                else if (propertyName.toLowerCase() === "beforeUpdate") {
                    component[propertyName] = options["beforeUpdate"];
                }
                else if (propertyName.toLowerCase() === "updated") {
                    component[propertyName] = options["updated"];
                }
                else if (propertyName.toLowerCase() === "activated") {
                    component[propertyName] = options["activated"];
                }
                else if (propertyName.toLowerCase() === "deactivated") {
                    component[propertyName] = options["deactivated"];
                }
                else if (propertyName.toLowerCase() === "beforeDestroy") {
                    component[propertyName] = options["beforeDestroy"];
                }
                else if (propertyName.toLowerCase() === "destroyed") {
                    component[propertyName] = options["destroyed"];
                }
                    //选项 / 资源
                else if (propertyName.toLowerCase() === "directives") {
                    component[propertyName] = options["directives"];
                }
                else if (propertyName.toLowerCase() === "filters") {
                    component[propertyName] = options["filters"];
                }
                else if (propertyName.toLowerCase() === "components") {
                    component[propertyName] = options["components"];
                }
                    //选项 / 资源
                else if (propertyName.toLowerCase() === "parent") {
                    component[propertyName] = options["parent"];
                }
                else if (propertyName.toLowerCase() === "mixins") {
                    component[propertyName] = options["mixins"];
                }
                else if (propertyName.toLowerCase() === "name") {
                    component[propertyName] = options["name"];
                }
                else if (propertyName.toLowerCase() === "extends") {
                    component[propertyName] = options["extends"];
                }
                else if (propertyName.toLowerCase() === "delimiters") {
                    component[propertyName] = options["delimiters"];
                }
                else if (propertyName.toLowerCase() === "functional") {
                    component[propertyName] = options["functional"];
                }
            }
        }
    };

    /**
     * @method getVueOptions
     * @param {sring} text 
     * @returns {object} 
     */
    Framework.prototype.getVueOptions = function (text) {
        var me = this;
        text = text.replace(/(\/(\**[^\*]*(\*[^\*]+)+\*)?\/[^\r\n]*)/g, "");//multi comment
        text = text.replace(/\/\/.*\r?\n/g, "");                            //one line comment
        text = text.replace(/\r?\n/g, "");
        text = text.replace(/\s+/g, " ");
        var result = /(?!new Vue)\(({.*})\);/.exec(text);

        if (!result) return { data: function () { } };

        return me.createObjectFromText(result[1]);
    };

    /**
     * 文本创建对象
     * @param {string} text 
     * @returns {object} 
     */
    Framework.prototype.createObjectFromText = function (text) {
        var me = this;
        var v = 1;
        if (v === 1) {
            return me.createObjectFromEval(text);
        }
        return null;
    };

    /**
     * 文本创建对象
     * @param {string} text 
     * @returns {object} 
     */
    Framework.prototype.createObjectFromEval = function (text) {
        var me = this;
        try {
            me.log("createObjectFromEval");
            me.log(text);
            var o = eval("(" + text + ")");
            me.log(o);
            return o;
        } catch (e) {
            me.errorHander(e);
            return null;
        }
    };

    /*
    *获取Url中的参数
    *@param {string} 参数名字
    *@return {string}
    *example:
    *  getQueryString("ID");
    */
    Framework.prototype.getQueryString = function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return decodeURIComponent(r[2]);
        return null;
    };

    /**
     * 获取全部参数
     * @returns {string} 
     */
    Framework.prototype.getQueryStringParamaters = function () {
        return window.location.search.split("?")[1];
    };

    /**
     * 异常处理
     * @returns {void} 
     */
    Framework.prototype.errorHander = function (error) {
        var me = this;
        var e = me.unityError(error);

        if (e.name === "TKW.Framework.Web.WebAuthenticationException") {
            me.ui.$alert("登录超时，请重新登录", {
                confirmButtonText: '重新登录',
                callback: function (action) {
                    me.ui.loadPage("/Home/Login");
                }
            });
        } else {
            me.alert(e.message);
        }
    };

    /**
     * 统一异常
     * @param {} error 
     * @returns {} 
     */
    Framework.prototype.unityError = function (error) {
        var e = new Error();

        //Business
        if (typeof error.ErrorType === "string" && error.ErrorType !== "") {
            e.name = error.ErrorType;
            e.message = error.ErrorTypeName + error.ErrorTypePrompt + error.ErrorTypeDescription;
        }
            //Another Business: JsonResultModel.Errors
        else if (typeof error.Errors === "string" && error.Errors !== "") {
            e.message = error.Errors;
        }
            //Server Global: System Exception
            /*
            *url:http://localhost:23771/api/Auth/Ping
            *return:"{"Message":"An error has occurred.","ExceptionMessage":"requestUrl=[http://localhost:23771/api/Auth/Ping]","ExceptionType":"TKW.Framework.Web.WebAuthenticationException","StackTrace":""}
            */
        else if (typeof error.ExceptionMessage === "string" && error.ExceptionMessage !== "") {
            e.name = error.ExceptionType;
            e.message = error.ExceptionMessage;
        }
            //Odata
        else if (typeof error["odata.error"] === "object" && typeof error["odata.error"].innererror.message === "string") {
            e.name = error["odata.error"].innererror.type;
            e.message = error["odata.error"].innererror.message;
        }
            /*
            * url:http://localhost:23771/api/Store/137/Disable
            * return:{ "Message": "The requested resource does not support http method 'POST'." }
            */
        else if (error.Message) {
            e.message = error.Message;
        }
            //js Exception
        else if (error.message) {
            e.message = error.message;
        }

        return e;
    };

    w.$f = new Framework();


    /**
     * Vue 全局异常处理
     * @param {object} err 
     * @param {object} vm 
     * @returns {void} 
     * http://cn.vuejs.org/v2/api/#errorHandler
     */
    w.Vue.config.errorHandler = function (err, vm) {
        w.$f.errorHander(err);
    };

    /**
     * Vue-resource 全局异常处理
     * https://github.com/pagekit/vue-resource/blob/develop/docs/http.md
     */
    w.Vue.http.interceptors.push(function (request, next) {
        var vm = this;
        if (typeof vm.loading === "boolean") {
            vm.loading = true;//显示加载
        }

        // continue to next interceptor
        next(function (response) {
            if (typeof vm.loading === "boolean") {
                vm.loading = false;//隐藏加载
            }

            if (!response.ok) {
                if (response.data instanceof Blob) {
                    var fr = new FileReader();
                    fr.addEventListener("load", function (e) {
                        w.$f.errorHander(JSON.parse(e.target.result));
                    });
                    fr.readAsText(response.data);
                }
                else if (/text\/html/i.test(response.headers.get("Content-Type"))) {
                    w.$f.alert(response.status + " " + response.statusText);
                }
                else if (/application\/json/i.test(response.headers.get("Content-Type"))) {
                    w.$f.errorHander(response.data);
                }
                
            }
        });
    });
})(window);