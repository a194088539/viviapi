function Dl() {
    var acctv = $("#username").val();
    var pwdv = $("#password").val();
    var codev = $("#imycode").val();
//    if (acctv == "" || acctv == "登录账号") {
//        alert('登录账号不能为空');
//        $("#username").focus();
//        return false;
//    }
//    if (pwdv == '') {
//        alert('登录密码不能为空');
//        $("#password").focus();
//        return false;
//    }
//    if (codev == '') {
//        alert('验证码不能为空');
//        $("#imycode").focus();
//        return false;
    //    }
    $.ajax({
        url: "/Merchant/Ajax/Login.ashx?t=" + Math.random(),
        data: { username: acctv,
            password: pwdv,
            imycode: codev
        },
        type: "POST",
        dataType: "text",
        success: function(json) {
            if (json == 'ok') {
                parent.window.location.href = "/Merchant/welcome.aspx";
            } else {
                alert(json);
            }
        }
    });
    return false;
}

function Dl2() {
    var acctv = $("#username").val();
    var pwdv = $("#password").val();
    var codev = $("#imycode").val();
    if (acctv == "" || acctv == "登录账号") {
        alert('登录账号不能为空');
        $("#username").focus();
        return false;
    }
    if (pwdv == '') {
        alert('登录密码不能为空');
        $("#password").focus();
        return false;
    }
    if (codev == '') {
        alert('验证码不能为空');
        $("#imycode").focus();
        return false;
    }
}

