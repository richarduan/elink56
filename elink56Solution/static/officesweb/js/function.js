



function getCookie(name) {
    alert(name);
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
    if (arr = document.cookie.match(reg))
        return unescape(arr[2]);
    else
        return null;
}



function getToken() {
    var __tokeyvalue = $("input[name='__RequestVerificationToken']").val();
    return __tokeyvalue;
}

jQuery.postJSON = function (url, data, callback,header) {
    data.token = getToken();
    jQuery.ajax({
        url: url,
        data: jQuery.param(data),
        dataType: "json",
        type: "POST",
        success: callback
    });
}

function rnd(n) {
    if (n == "" || n == null) n = 100
    return Math.random().toString().replace("0.", "");
}