function Dl() {
    var acctv = $("#username").val();
    var pwdv = $("#password").val();
    var codev = $("#imycode").val();
//    if (acctv == "" || acctv == "��¼�˺�") {
//        alert('��¼�˺Ų���Ϊ��');
//        $("#username").focus();
//        return false;
//    }
//    if (pwdv == '') {
//        alert('��¼���벻��Ϊ��');
//        $("#password").focus();
//        return false;
//    }
//    if (codev == '') {
//        alert('��֤�벻��Ϊ��');
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
    if (acctv == "" || acctv == "��¼�˺�") {
        alert('��¼�˺Ų���Ϊ��');
        $("#username").focus();
        return false;
    }
    if (pwdv == '') {
        alert('��¼���벻��Ϊ��');
        $("#password").focus();
        return false;
    }
    if (codev == '') {
        alert('��֤�벻��Ϊ��');
        $("#imycode").focus();
        return false;
    }
}

