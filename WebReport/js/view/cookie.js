function setCookie(name, value) {
    var minutes = 30;
    var exp = new Date();
    exp.setTime(exp.getTime() + minutes * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
    location.href = "../html/page1.htm";
}

function getCookie(name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");

    if (arr = document.cookie.match(reg)) {
        return unescape(arr[2]);
    }
    else
        return null;
}

function delCookie(name) {
    var exp = new Date();
    exp.setTime(exp.getTime() - 1000);
    var cval = getCookie(name);
    if (cval != null) {
       
        document.cookie = name + "=" + cval + ";expires=" + exp.toUTCString()+";path=/;";
    }
        
} 