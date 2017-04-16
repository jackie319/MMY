/*
* @author:mailzy@vip.qq.com
* @version:1.0.3
*/
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
        this.basicCompomets = {
            "el-row": ELEMENT.Row,
            "el-col": ELEMENT.Col,
            "el-button": ELEMENT.Button
        };
        this.formCompomets = {
            "el-radio": ELEMENT.Radio,
            "el-radio-group": ELEMENT.RadioGroup,
            "el-checkbox": ELEMENT.Checkbox,
            "el-checkbox-group": ELEMENT.CheckboxGroup,
            "el-input": ELEMENT.Input,
            "el-input-number": ELEMENT.InputNumber,
            "el-select": ELEMENT.Select,
            "el-switch": ELEMENT.Switch,
            "el-slider": ELEMENT.Slider,
            "el-time-picker": ELEMENT.TimePicker,
            "el-time-select": ELEMENT.TimeSelect,
            "el-date-picker": ELEMENT.DatePicker,
            "el-upload": ELEMENT.Upload,
            "el-rate": ELEMENT.Rate,
            "el-form": ELEMENT.Form,
            "el-form-item": ELEMENT.FormItem,
            "el-option": ELEMENT.Option
        };
        this.dataCompomets = {
            "el-table": ELEMENT.Table,
            "el-table-column": ELEMENT.TableColumn,
            "el-tag": ELEMENT.Tag,
            "el-progress": ELEMENT.Progress,
            "el-tree": ELEMENT.Tree,
            "el-badge": ELEMENT.Badge,
            "el-pagination": ELEMENT.Pagination
        };
        this.noticeCompomets = {
            "el-alert": ELEMENT.Alert
        };
        this.navigationCompomets = {
            "el-menu": ELEMENT.Menu,
            "el-menu-item": ELEMENT.MenuItem,
            "el-submenu": ELEMENT.Submenu,
            "el-tabs": ELEMENT.Tabs,
            "el-tab-pane": ELEMENT.TabPane,
            "el-breadcrumb": ELEMENT.Breadcrumb,
            "el-breadcrumb-item": ELEMENT.BreadcrumbItem,
            "el-dropdown": ELEMENT.Dropdown,
            "el-dropdown-menu": ELEMENT.DropdownMenu,
            "el-steps": ELEMENT.Steps,
            "el-step": ELEMENT.Step
        };
        this.othersCompomets = {
            "el-dialog": ELEMENT.Dialog,
            "el-tooltip": ELEMENT.Tooltip,
            "el-popover": ELEMENT.Popover,
            "el-card": ELEMENT.Card,
            "el-carousel": ELEMENT.Carousel,
            "el-collapse": ELEMENT.Collapse
        };
        this.allCompomets = {};
        for (var i, g; g = [this.basicCompomets, this.formCompomets, this.dataCompomets, this.noticeCompomets, this.navigationCompomets, this.othersCompomets][i++];) {
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
        //me.ping();
    };

    /**
     * 用户退出
     * @returns {} 
     */
    Framework.prototype.logout = function () {
        var me = this;
        localStorage.clear();
        clearInterval(me.timerId);
        me.ui.loadPage("/wwwroot/login.html");
        me.ajax({ method: "POST", url: "/Account/Logout" });
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
        localStorage["User"] = JSON.stringify(user);
    };

    /**
     * 获取用户信息
     * @returns {object} 
     */
    Framework.prototype.getUser = function () {
        return JSON.parse(localStorage["User"], function (k, v) {
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

    Framework.prototype.fixDate = function (o) {
        for (var p in o) {
            if (/\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}/.test(o[p])) {
                //2016-09-18T03:46:11.893Z
                o[p] = new Date(o[p].replace("T", ""));
            }
            else if (/\/Date\(-?\d+\)\//.test(o[p])) {
                //Microsoft json Date \/Date(1450800000000)\/ \/Date(-62135596800000)\/
                o[p] = new Date(parseInt(o[p].substring(6)));
            }
            
        }
        return o;
    };

    Framework.prototype.getMenu = function () {
        var me = this;
        var user = me.getUser();
        return user.UserMenuModels;
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
            this.ui.$alert(text, "", {
                confirmButtonText: '确定',
                callback: fnOk
            });
        } else {
            alert(text);
        }

        if (webConfig.debug) {
            console.log(text);
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
        this.ui.$confirm(text, '', {
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
        this.ui.$prompt(text, '', {
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

        options.accept = options.accept ? accept[options.accept] : accept["json"];

        var settings = Object.assign({
            url: "",
            method: "GET",
            data: null,
            accept: accept["json"],
            progress: function (progressEvent) { },
            success: function (body, loadEvent) { },
            error: function (body, errorEvent) {
                var request = errorEvent.target;
                if (/application\/json/i.test(request.getResponseHeader("Content-Type"))) {
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
        xhr.setRequestHeader("Accept", settings.accept);
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
                accept: "html",
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
        //text = text.replace(/\/\/.*\r?\n/g, "");    //one line comment
        //text = text.replace(/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*/g, "");   //multi comment http://blog.ostermiller.org/find-comment
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

        if (e.name === 1) {
            me.ui.$alert("登录超时，请重新登录", {
                confirmButtonText: '重新登录',
                callback: function (action) {
                    me.ui.loadPage("/wwwroot/login.html");
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
        if (typeof error.Success === "boolean" && !error.Success) {
            e.name = error.ExceptionType;
            e.message = error.ErrorMsg;
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
        next(function(response) {
            if (typeof vm.loading === "boolean") {
                vm.loading = false; //隐藏加载
            }

            if (response.ok) {
                if (/application\/json/i.test(response.headers.get("Content-Type")) && !response.data.Success) {
                    w.$f.errorHander(response.data);
                    // stop and return response
                    next(request.respondWith(body,
                    {
                        status: 404,
                        statusText: 'Not Find'
                    }));
                }
            }
        });
    });
})(window);