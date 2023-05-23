var cardinputID = "#ctl00_ContentPlaceHolder1_txtCards";
var cardtypeDDL = "#ctl00_ContentPlaceHolder1_ddlcardType";
var ldico = '';
var errico = '';
var okico = '';
jQuery(document).ready(function() {
    $(cardinputID).blur(function() {
        var Groupstxt = $(cardinputID).val().split('\n');
        var Groupscount = Groupstxt.length;
        var show = Groupscount - 1;
        if (Groupstxt[Groupstxt.length - 1]) {
            show = Groupscount;
        }
        $("#Groupscount").html(show + "张" + $(cardtypeDDL).find("option:selected").text() + "卡");
    });

    $(cardinputID).keyup(function() {
        var Groupstxt = $(cardinputID).val().split('\n');
        var Groupscount = Groupstxt.length;
        var show = Groupscount - 1;
        if (Groupstxt[Groupstxt.length - 1]) {
            show = Groupscount;
        }
        $("#Groupscount").html(show + "张" + $(cardtypeDDL).find("option:selected").text() + "卡");
    });

    $("#btnXiaoKa").click(function() {
        var groupchannelId = $(cardtypeDDL).val();
        var groupcontent = $(cardinputID).val();
        if (groupcontent == '') {
            $(cardinputID).focus();
            $("#Groupsinfo").html("请输入卡信息").show(); ClearGroupsinfo();
            return false;
        };
        if (groupchannelId == '') {
            $("#Groupsinfo").html("通道信息获取失败").show(); ClearGroupsinfo();
            return false;
        };
        $("#Groupsinfo").css({ color: "#666666" });
        $(this).attr("disabled", true);
        groupcontentarr = Cleartrim(groupcontent);
        var group_h = groupcontentarr.split('\n');
        var Groupbackmsg = "";
        $("#Groupsinfo").html("").hide();
        ClearGroupsinfo();
        $(cardinputID).val("");
        for (var i = 0; i < group_h.length; i++) {
            var groupcard = group_h[i].split(',');
            var ino = i + 1;
            if (ino < 10) { ino = "0" + ino; }
            if (groupcard.length != 3) {
                $("#Groupsinfo_" + ino).html( "第 " + ino + " 张 { 卡信息格式不正确,不予接收处理 }</span>").show();
            }
            else {
                var groupcardid = groupcard[0];
                var groupcardpass = groupcard[1];
                var grouppaymoney = groupcard[2];
                if (ino < 21) { if (group_h[i]) { Groupscard(ino, groupchannelId, groupcardid, groupcardpass, grouppaymoney); } }
            }
        }
        $(this).removeAttr("disabled");
        queryOrder();
    });

});

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
    $("#Groupsload").html("").hide();
    $("#Groupscount").html("0")
}

function Cleartrim(a) {
    a = a.replace(/\s{2,}/g, ',');
    a = a.replace(/，/g, ',');
    a = a.replace(/ /g, ',');
    return a
}

function Groupscard(b, c, d, e, f) {
    $("#Groupsinfo_" + b).html("正在提交..").show();
    postorderdata = "ChannelId=" + c + "&CardId=" + d + "&CardPass=" + e + "&FaceValue=" + f + "";
    $.ajax({
        type: "get",
        contentType: "text/html",
        url: "/Merchant/Ajax/CardSell.ashx?t=" + Math.random(),
        data: postorderdata,
        error: function() {
            $("#Groupsinfo").css({
                color: "red"
            });
            $("#Groupsinfo").html(errico + "提交出现错误</span>");
            ClearGroupsinfo()
        },
        success: function(a) {
            if (a != "true") {
                Groupbackmsg = errico + "第 " + b + " 张 { 卡号：" + d + " | 提交失败：" + a + " }"
            } else {
                Groupbackmsg = okico + "第 " + b + " 张 { 卡号：" + d + " | 提交成功,正在处理 }"
            }
            $("#Groupsload").html("").hide();
            $("#Groupsinfo_" + b).html("").hide();
            $("#Groupsinfo_" + b).html(Groupbackmsg).show()
        }
    })
}

function queryOrder() {
    $("#queryorder").attr("disabled", "disabled");
    $("#toporder").html("<tr><td colspan='10' class='nomsg'>" + ldico + "Loading..</span></td></tr>");
    $.ajax({
        type: "get",
        contentType: "text/html",
        url: "/Merchant/Ajax/ordersearch.ashx?t=" + Math.random(),
        data: "",
        error: function() {
            $("#toporder").html("<tr><td colspan='10' class='nomsg'>提交出现错误</td></tr>")
        },
        success: function(a) {
            if (a != "") {
                $("#queryorder").removeAttr("disabled");
                $("#toporder").html(a)
            }
        }
    })
}

function checkflag(a) {
    setTimeout(function() {
        stopflag(a)
    },
	300);
    $("#sub" + a).attr("disabled", "disabled");
    $("#paymoney" + a).html(ldico + "Loading</span>");
    $("#orderzt" + a).html(ldico + "Loading</span>");
    $("#errorMsg" + a).html(ldico + "Loading</span>")
}

function stopflag(c) {
    postData = "oid=" + c + "&rnd=" + Math.random();
    $.ajax({
        type: "get",
        dataType: "json",
        timeout: 10000,
        url: '/Merchant/Ajax/OrderJson.ashx',
        data: postData,
        success: function(a) {
            $("#sub" + c).removeAttr("disabled");
            $("#orderzt" + c).html(a.Success);
            $("#paymoney" + c).html(a.paymoney);
            $("#errorMsg" + c).html(a.errorMsg)
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