// 对String的扩展，格式化当前字符串，将值插入到当前字符串中
// var template1="我是{0}，今年{1}了";
// var result1=template1.Format("loogn",22);       ==> 我是loogn，今年22了
//
// var template2="我是{name}，今年{age}了";
// var result2=template1.Format({name:"loogn",age:22}); ==> 我是loogn，今年22了
String.prototype.Format = function (args) {
    if (arguments.length > 0) {
        var result = this;
        if (arguments.length === 1 && typeof (args) == "object") {
            for (var key in args) {
                var reg = new RegExp("({" + key + "})", "g");
                result = result.replace(reg, args[key]);
            }
        }
        else {
            for (var i = 0; i < arguments.length; i++) {
                if (arguments[i] == undefined) {
                    return "";
                }
                else {
                    var reg = new RegExp("({[" + i + "]})", "g");
                    result = result.replace(reg, arguments[i]);
                }
            }
        }
        return result;
    }
    else {
        return this;
    }
}