
    var times = 10;
    function fun() {
        if (times > 1) {
            document.getElementById("button").value = "我已了解，继续付款。[" + times + "秒后自动跳转]";
        } else {
            document.forms[0].submit();
            //document.getElementById("button").value = "我已了解，点击继续";
        }
        times -= 1;
        if (times <= 0) {
            window.clearInterval(doTEST);
            document.getElementById("button").disabled = false;
        }
    }
    var doTEST;
    window.clearInterval(doTEST);
    doTEST = window.setInterval("fun()", 1000);
            