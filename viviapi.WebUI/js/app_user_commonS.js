jQuery(document).ready(function() {
    //提交销卡
    $('#form_postorder button[type="reset"]').click(function() { $("#form_postorder button.green").removeAttr("disabled"); $("#callinfo").html(""); });
    $("#form_postorder button.green").click(function() {
        var channeltype = $("#ctl00_ContentPlaceHolder1_xk_channelId").val();
        var channelname = $("#ctl00_ContentPlaceHolder1_xk_channelname").val();
        var xkcardid = $("#xk_cardId").val();
        var xkcardpass = $("#xk_cardPass").val();
        var xkfacevalue = $("#ctl00_ContentPlaceHolder1_xk_faceValue").val();
        var cmgs = "卡号输入错误！";
        var mmgs = "密码输入错误！";
        if (xkcardid == '') { $("#xk_cardId").focus(); $("#callinfo").html("<img style=\"vertical-align\:middle\" src=\"/Style/images/wrong.png\" />" + "请输入卡号</span>"); return false };
        if (xkcardpass == '') { $("#xk_cardPass").focus(); $("#callinfo").html("<img style=\"vertical-align\:middle\" src=\"/Style/images/wrong.png\" />" + "请输入卡密码</span>"); return false };
        if (xkfacevalue == '') { $("#xk_faceValue").focus(); $("#callinfo").html("<img style=\"vertical-align\:middle\" src=\"/Style/images/wrong.png\" />" + "请输入正确的卡面额</span>"); return false };
        switch (channeltype) {
            case '107':
                if (xkcardid.length != 9) {
                    $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为9位", lock: true, fixed: true, cancelVal: '确定', cancel: function() { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                }
                else {
                    if (xkcardpass.length != 12) {
                        $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为12位", lock: true, fixed: true, cancelVal: '确定', cancel: function() { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                    }
                }
                break; //腾讯卡
            case '104':
                if (xkcardid.length != 15) { $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为15位", lock: true, fixed: true, cancelVal: '确定', cancel: function() { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false; }
                else
                { if (xkcardpass.length != 8 && xkcardpass.length != 9) { $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为8或9位", lock: true, fixed: true, cancelVal: '确定', cancel: function() { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false; } }
                break; //盛大卡
            case '106':
                if (xkcardid.length != 16) { $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为16位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false; }
                else
                { if (xkcardpass.length != 16) { $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为16位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false; } }
                break; //骏网一卡通
            case '111':
                if (xkcardid.length != 10) {
                    $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为10位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                } else {
                    if (xkcardpass.length != 15) { $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为15位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false; }
                } break; //完美一卡通
            case '112':
                if (xkcardid.length != 20) {
                    $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为20位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                } else {
                    if (xkcardpass.length != 12) {
                        $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为12位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                    }
                } break; //搜狐一卡通
            case '105':
                if (xkcardid.length != 16) {
                    $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为16位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                } else {
                    if (xkcardpass.length != 8) {
                        $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为8位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                    }
                } break; //征途游戏卡
            case '109':
                if (xkcardid.length != 13) {
                    $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为13位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                } else {
                    if (xkcardpass.length != 10) {
                        $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为10位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                    }
                } break; //久游一卡通
            case '110':
                if (xkcardid.length != 13) {
                    $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为13位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                } else {
                    if (xkcardpass.length != 9) {
                        $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为9位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                    }
                } break; //网易一卡通
            case '115':
                if (xkcardid.length != 20) {
                    $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为20位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                } else {
                    if (xkcardpass.length != 8) {
                        $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为8位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                    }
                } break; //光宇一卡通
            case '114':
                if (xkcardid.length != 19) {
                    $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为19位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                } else {
                    if (xkcardpass.length != 18) {
                        $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为18位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                    }
                } break; //电信充值卡
            case '103':
                if (xkcardid.length != 17) {
                    $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为17位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                } else {
                    if (xkcardpass.length != 18) {
                        $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为18位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                    }
                } break; //神州行充值卡
            case '108':
                if (xkcardid.length != 15) {
                    $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为15位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                } else {
                    if (xkcardpass.length != 19) {
                        $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为19位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                    }
                } break; //联通充值卡
            case '117':
                if (xkcardid.length != 15) {
                    $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为15位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                } else {
                    if (xkcardpass.length != 15) {
                        $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为15位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                    }
                } break; //纵游一卡通
            case '119':
                if (xkcardid.length != 10 && xkcardid.length != 12) {
                    $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为10或12位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                } else {
                    if (xkcardpass.length != 10 && xkcardpass.length != 15) {
                        $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为10或15位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                    }
                } break; //天宏一卡通
            case '118':
                if (xkcardid.length != 15) {
                    $.dialog({ title: lktitle, content: channelname + cmgs + "<br />卡号为15位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardId").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                } else {
                    if (xkcardpass.length != 8) {
                        $.dialog({ title: lktitle, content: channelname + mmgs + "<br />密码为8位", lock: true, fixed: true, okVal: '确定', ok: function() { $("#xk_cardPass").focus(); }, icon: 'warning', width: '250px', height: '90px' }); return false;
                    }
                } break; //天下一卡通

        } //switch end	
        if (parseInt(xkfacevalue) < 1 || parseInt(xkfacevalue) > 1000) { $("#xk_faceValue").focus(); $("#callinfo").html("<img style=\"vertical-align\:middle\" src=\"/Style/images/wrong.png\" />" + "请输入正确的卡面额</span>"); return false };
        var postorderdata = "ChannelId=" + channeltype + "&CardId=" + xkcardid + "&CardPass=" + xkcardpass + "&FaceValue=" + xkfacevalue + "";
        $(".required").val("");
        $("#callinfo").css({ color: "#666666" }); $("#callinfo").html("<img style=\"vertical-align\:middle\" src=\"/Style/images/onLoad.gif\" />" + "正在提交，请稍等</span>");
        $.ajax({
            type: "get",
            contentType: "text/html",
            url: "/Merchant/Ajax/smartcardsell.ashx?t=" + Math.random(),
            data: postorderdata,
            error: function() {
                $("#callinfo").css({ color: "red" });
                $("#callinfo").html("<img style=\"vertical-align\:middle\" src=\"/Style/images/wrong.png\" />" + "提交出现错误</span>");
            },
            success: function(result) {
                if (result != "true") {
                    $("#callinfo").html("<img style=\"vertical-align\:middle\" src=\"/Style/images/wrong.png\" />" + "<font color=\"red\">提交失败：" + result + "</font></span>");
                } //失败
                else
                { $("#callinfo").html("<img style=\"vertical-align\:middle\" src=\"/Style/images/right.png\" />" + "<font color=\"green\">提交成功!</span></font>"); } //成功
            }
        });
        queryOrder();
    });

    $("#arr_content").blur(function() {
        var Groupstxt = $("#arr_content").val().split('\n');
        var Groupscount = Groupstxt.length;
        //alert(Groupstxt[Groupstxt.length-1]);
        if (Groupstxt[Groupstxt.length - 1]) {
            $("#Groupscount").html(Groupscount);
        }
        else {
            $("#Groupscount").html(Groupscount - 1);
        }
    });
    $("#arr_content").keyup(function() {
        var Groupstxt = $("#arr_content").val().split('\n');
        var Groupscount = Groupstxt.length;
        //alert(Groupstxt[Groupstxt.length-1]);
        if (Groupstxt[Groupstxt.length - 1]) {
            $("#Groupscount").html(Groupscount);
        }
        else {
            $("#Groupscount").html(Groupscount - 1);
        }
    });
    //批量提交
    $("#form_Groupscard button.green").click(function() {
        var xkfacevalue = $("#ctl00_ContentPlaceHolder1_xk_faceValue").val();

        var groupchannelId = $("#ctl00_ContentPlaceHolder1_g_channelId").val();
        var groupcontent = $("#arr_content").val();
        groupcontent = text_trim(groupcontent);

        if (groupcontent == '') {
            $("#arr_content").focus();
            $("#Groupsinfo").html("<img style=\"vertical-align\:middle\" src=\"/Style/images/wrong.png\" />" + "<font color=\"red\">请输入卡信息</font></span>").show(); ClearGroupsinfo();
            return false;
        };
        if (groupchannelId == '') {
            $("#Groupsinfo").html("<img style=\"vertical-align\:middle\" src=\"/Style/images/wrong.png\" />" + "<font color=\"red\">通道信息获取失败</font></span>").show(); ClearGroupsinfo();
            return false;
        };
        $("#Groupsinfo").css({ color: "#666666" });
        $(this).attr("disabled", true);

        groupcontentarr = Cleartrim(groupcontent);
        var group_h = groupcontentarr.split('\n');
        var Groupbackmsg = "";
        $("#Groupsinfo").html("").hide();
        ClearGroupsinfo();
        $("#arr_content").val("");
        for (var i = 0; i < group_h.length; i++) {
            var groupcard = group_h[i].split(',');
            var ino = i + 1;
            if (ino < 10) { ino = "0" + ino; }
            if (groupcard.length < 2) {
                $("#Groupsinfo_" + ino).html("<img style=\"vertical-align\:middle\" src=\"/Style/images/wrong.png\" />" + "<font color=\"red\">第 " + ino + " 张 { 卡信息格式不正确,不予接收处理 }</font></span>").show();
            }
            else {
                var groupcardid = groupcard[0];
                var groupcardpass = groupcard[1];
                var grouppaymoney = xkfacevalue;
                if (ino < 61) { if (group_h[i]) { Groupscard(ino, groupchannelId, groupcardid, groupcardpass, grouppaymoney); } }
            }
        }
        $("#form_Groupscard button.green").removeAttr("disabled");
        queryOrder();
    });
    $('#form_Groupscard button[type="reset"]').click(function() {
        $("#form_Groupscard button.green").removeAttr("disabled");
        $("#arr_content").val("");
        $("#Groupsinfo").html("").hide(); ClearGroupsinfo();
    });
})

function checkflag(a) {
    setTimeout(function() {
        stopflag(a)
    },
	300);
    $("#sub" + a).attr("disabled", "disabled");
    $("#paymoney" + a).html("<img src=\"../Style/images/loading1.gif\" /></span>");
    $("#orderzt" + a).html("<img src=\"../Style/images/loading1.gif\" /></span>");
    $("#errorMsg" + a).html("<img src=\"../Style/images/loading1.gif\" /></span>")
}
function stopflag(c) {
    postData = "oid=" + c + "&rnd=" + Math.random();
    $.ajax({
        type: "get",
        dataType: "json",
        timeout: 10000,
        url: '/merchant/ajax/OrderJson.ashx',
        data: postData,
        success: function(a) {
            $("#sub" + c).removeAttr("disabled");
            $("#orderzt" + c).html(a.Success);
            $("#paymoney" + c).html(a.paymoney);
            $("#errorMsg" + c).html(a.errorMsg.substring(0, 13))
        },
        error: function(a, b) {
            $("#sub" + c).removeAttr("disabled");
            $.dialog({
                title: lktitle,
                content: '结果获取失败,请稍等重试' + b,
                lock: true,
                fixed: true,
                ok: function() {
                    window.location.reload()
                },
                icon: 'warning',
                width: '250px',
                height: '90px'
            })
        }
    })
}
function queryOrder() {
    $("#queryorder").attr("disabled", "disabled");
    $("#toporder").html("<tr><td colspan='10' class='nomsg'>" + "<img src=\"../style/images/loading2.gif\" /></span></td></tr>");
    $.ajax({
        type: "get",
        contentType: "text/html",
        url: "/merchant/ajax/ordersearch.ashx?t=" + Math.random(),
        data: "",
        error: function() {
            $("#toporder").html("<tr><td colspan='10' class='nomsg'>提交出现错误</td></tr>")
        },
        success: function(a) {
            if (a != "") {
                $("#queryorder").removeAttr("disabled");
                a.replace("已完成", "<font color='green'>已完成</font>").replace("失败", "<font color='red'>失败</font>");
                $("#toporder").html(a);
            }
        }
    })
}
function Groupscard(b, c, d, e, f) {
    $("#Groupsinfo_" + b).html("<img style=\"vertical-align\:middle\" src=\"/Style/images/onLoad.gif\" />" + "正在提交..</span>").show();
    postorderdata = "ChannelId=" + c + "&CardId=" + d + "&CardPass=" + e + "&FaceValue=" + f + "";
    $.ajax({
        type: "get",
        contentType: "text/html",
        url: "/Merchant/Ajax/smartcardsell.ashx?t=" + Math.random(),
        data: postorderdata,
        error: function() {
            $("#Groupsinfo").css({
                color: "red"
            });
            $("#Groupsinfo").html("<img style=\"vertical-align\:middle\" src=\"/Style/images/wrong.png\" />" + "提交出现错误</span>");
            ClearGroupsinfo()
        },
        success: function(a) {
            if (a != "true") {
                Groupbackmsg = "<img style=\"vertical-align\:middle\" src=\"/Style/images/wrong.png\" />" + "<font color=\"red\">第 " + b + " 张 { 卡号：" + d + " | 提交失败：" + a + " }</font></span>"
            } else {
                Groupbackmsg = "<img style=\"vertical-align\:middle\" src=\"/Style/images/right.png\" />" + "<font color=\"green\">第 " + b + " 张 { 卡号：" + d + " | 提交成功,稍后请到订单页面 查结果 }</font></span>"
            }
            $("#Groupsload").html("").hide();
            $("#Groupsinfo_" + b).html("").hide();
            $("#Groupsinfo_" + b).html(Groupbackmsg).show()
        }
    })
}
function ClearGroupsinfo() {
    $("#Groupsinfo_01").html("").hide();
    $("#Groupsinfo_02").html("").hide();
    $("#Groupsinfo_03").html("").hide();
    $("#Groupsinfo_04").html("").hide();
    $("#Groupsinfo_05").html("").hide();
    $("#Groupsinfo_06").html("").hide();
    $("#Groupsinfo_07").html("").hide();
    $("#Groupsinfo_08").html("").hide();
    $("#Groupsinfo_09").html("").hide();
    $("#Groupsinfo_10").html("").hide();
    $("#Groupsinfo_11").html("").hide();
    $("#Groupsinfo_12").html("").hide();
    $("#Groupsinfo_13").html("").hide();
    $("#Groupsinfo_14").html("").hide();
    $("#Groupsinfo_15").html("").hide();
    $("#Groupsinfo_16").html("").hide();
    $("#Groupsinfo_17").html("").hide();
    $("#Groupsinfo_18").html("").hide();
    $("#Groupsinfo_19").html("").hide();
    $("#Groupsinfo_20").html("").hide();
    $("#Groupsinfo_21").html("").hide();
    $("#Groupsinfo_22").html("").hide();
    $("#Groupsinfo_23").html("").hide();
    $("#Groupsinfo_24").html("").hide();
    $("#Groupsinfo_25").html("").hide();
    $("#Groupsinfo_26").html("").hide();
    $("#Groupsinfo_27").html("").hide();
    $("#Groupsinfo_28").html("").hide();
    $("#Groupsinfo_29").html("").hide();
    $("#Groupsinfo_30").html("").hide();
    $("#Groupsinfo_31").html("").hide();
    $("#Groupsinfo_32").html("").hide();
    $("#Groupsinfo_33").html("").hide();
    $("#Groupsinfo_34").html("").hide();
    $("#Groupsinfo_35").html("").hide();
    $("#Groupsinfo_36").html("").hide();
    $("#Groupsinfo_37").html("").hide();
    $("#Groupsinfo_38").html("").hide();
    $("#Groupsinfo_39").html("").hide();
    $("#Groupsinfo_40").html("").hide();
    $("#Groupsinfo_41").html("").hide();
    $("#Groupsinfo_42").html("").hide();
    $("#Groupsinfo_43").html("").hide();
    $("#Groupsinfo_44").html("").hide();
    $("#Groupsinfo_45").html("").hide();
    $("#Groupsinfo_46").html("").hide();
    $("#Groupsinfo_47").html("").hide();
    $("#Groupsinfo_48").html("").hide();
    $("#Groupsinfo_49").html("").hide();
    $("#Groupsinfo_50").html("").hide();
    $("#Groupsinfo_51").html("").hide();
    $("#Groupsinfo_52").html("").hide();
    $("#Groupsinfo_53").html("").hide();
    $("#Groupsinfo_54").html("").hide();
    $("#Groupsinfo_55").html("").hide();
    $("#Groupsinfo_56").html("").hide();
    $("#Groupsinfo_57").html("").hide();
    $("#Groupsinfo_58").html("").hide();
    $("#Groupsinfo_59").html("").hide();
    $("#Groupsinfo_60").html("").hide();
    $("#Groupsload").html("").hide();
    $("#Groupscount").html("0")
}
function Cleartrim(a) {
    a = a.replace(/\s{2,}/g, ',');
    a = a.replace(/，/g, ',');
    a = a.replace(/ /g, ',');

    return a;
}


function str_trim(str) {
    return str.replace(/(^\s*)|(\s*$)/g, "");
}

/*去掉每行后面多余的空格*/
function text_trim(_text) {
    var _result = "";
    var _arr = _text.split('\n');

    for (var i = 0; i < _arr.length; i++) {
        var _card = str_trim(_arr[i]);
        if (i < _arr.length - 1) {
            _result = _result + _card + '\n';
        }
        else {
            _result = _result + _card;
        }
    }
    return _result;
}