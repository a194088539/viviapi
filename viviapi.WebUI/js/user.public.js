var lktitle = "提示：";
var errico = "<img style=\"vertical-align\:middle\" src=\"/Style/images/wrong.png\" />" + " ";
var okico = "<img style=\"vertical-align\:middle\" src=\"/Style/images/right.png\" />" + " ";
var ldico = "<img style=\"vertical-align\:middle\" src=\"/Style/images/onLoad.gif\" />" + " ";
function loginout() {
    $.dialog({
        title: lktitle,
        content: '您确定退出登录商户中心吗？',
        lock: true,
        fixed: true,
        ok: function() {
            location.href = "/Member/loginout.aspx";
            return false
        },
        cancelVal: '取消',
        cancel: true,
        icon: 'question',
        width: '250px',
        height: '90px'
    })
}
function salfReset() {
    $.dialog({
        title: lktitle,
        content: '您确定重置安全码吗？',
        lock: true,
        fixed: true,
        ok: function() {
            location.href = "/Member/modes/index.aspx?resetapikey=1";
            return false
        },
        cancelVal: '取消',
        cancel: true,
        icon: 'question',
        width: '250px',
        height: '90px'
    })
}
function setcookie(name, value) {
    var Days = 1;
    var exp = new Date();
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString()
}
function getcookie(name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
    if (arr = document.cookie.match(reg)) return unescape(arr[2]);
    else return null
}
function delcookie(name) {
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = getcookie(name);
    if (cval != null) document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString()
}
/*去掉_viewstate*/
function serializex(src) {
    var i = src.indexOf("__VIEWSTATE");
    var j = src.indexOf("&", i);
    if (i > -1) {
        src = src.replace(src.substring(i, j + 1), "");
    }
    return src;
}
jQuery(document).ready(function() {
    var browser_ver = $.browser.version;
    var accurate_value = browser_ver.substr(0, 1); (function(config) {
        config['drag'] = true;
        config['fixed'] = true;
        config['esc'] = true;
        config['resize'] = false
    })($.dialog.defaults);
    var feedbackcon = $("#boxfeedback").html();
    var advisorycon = $("#boxadvisory").html();
    $(".btn-feedback").dialog({
        title: '意见反馈',
        fixed: true,
        content: feedbackcon,
        width: 250,
        height: 120
    });
    $(".btn-advisory").dialog({
        title: '在线咨询',
        fixed: true,
        content: advisorycon,
        width: 250,
        height: 120,
        cancelVal: '取消',
        cancel: true
    });
    $("#ctl00_ContentPlaceHolder1_sdate").date_input();
    $("#ctl00_ContentPlaceHolder1_edate").date_input();
    $("#phoneputbox").fadeOut();
    $("#yphoneputbox").fadeOut();
    $("#phonecodebox").fadeOut();
    $("#personbox").fadeOut();
    var ww = $(window).width();
    var dw = $("#foot_layer").width();
    $("#foot_layer").css({
        "left": (ww - dw) / 2 + $(window).scrollLeft() + "px"
    });
    $(".m-userinfo span.name").hover(function() {
        if ($(this).queue().length <= 0) {
            $('.m-userinfo span.name').removeClass('name').addClass('on');
            $(this).siblings().stop(true, true).slideDown();
            $('span.icodown').removeClass('icodown').addClass("icoup")
        }
    }).parent().hover(function() {
        if ($.browser.msie && accurate_value == '6') {
            $(".jqTransformSelectWrapper:eq(1)").fadeOut();
            $("#tiphack").fadeOut()
        }
    },
    function() {
        $('.m-userinfo .mi-info').slideUp(200);
        if ($.browser.msie && accurate_value == '6') {
            $(".jqTransformSelectWrapper:eq(1)").fadeIn();
            $("#tiphack").fadeIn()
        };
        $('span.icoup').removeClass('icoup').addClass("icodown");
        $('.m-userinfo span.on').removeClass('on').addClass('name')
    });
    if ($('form').length > 0) {
        $('form,#searchbox-hide').jqTransform({
            imgPath: '/common/plugin/img/'
        })
    };
    $(".chart-box h4").click(tab);
    function tab() {
        $(this).addClass("ac").siblings().removeClass("ac");
        var tab = $(this).attr("id");
        $("#" + tab + "-box").show().siblings().hide();
    };
    $("#form_payaccount button.green").click(function() {
        if ($("#cardAddr").val() == '') {
            $("#cardAddr").focus();
            $("#callinfo").html(errico + "请输入开户支行");
            return false
        };
        if ($("#thiscard").val() == '') {
            $("#thiscard").focus();
            $("#callinfo").html(errico + "请输入当前银行卡号");
            return false
        };
        if ($("#cardId").val() == '') {
            $("#cardId").focus();
            $("#callinfo").html(errico + "请输入您的银行卡号");
            return false
        };
        //        if ($("#cardId").val().length != 16 && $("#cardId").val().length != 19) {
        //            $("#cardId").focus();
        //            $("#callinfo").html(errico + "请输入正确的银行卡号");
        //            return false
        //        };
        if ($("#recardId").val() == '') {
            $("#recardId").focus();
            $("#callinfo").html(errico + "请确认银行卡号");
            return false
        };
        if ($("#recardId").val() !== $("#cardId").val()) {
            $("#recardId").focus();
            $("#callinfo").html(errico + "两次输入的银行卡号不一致");
            return false
        };
        $("#callinfo").css({
            color: "#666666"
        });
        $(this).attr("disabled", true);
        $("#callinfo").html(ldico + "正在提交请求");

        var bankNameValue = $("#bankName").val();
        var actionValue = $("#action").val();
        var cardAddrValue = $("#cardAddr").val();
        var cardIdValue = $("#cardId").val();
        var recardIdValue = $("#recardId").val();
        var thiscardValue = $("#thiscard").val();

        //$("#aspnetForm").serialize()
        $.get("/Member/ajax/payacct.ashx?t=" + Math.random(), { bankName: bankNameValue, action: actionValue, cardAddr: cardAddrValue, cardId: cardIdValue, recardId: recardIdValue, thiscard: thiscardValue },
        function(data, textStatus) {
            if (data == "true") {
                $("#form_payaccount button.green").removeAttr("disabled");
                $("#callinfo").css({
                    color: "green"
                });
                $(".required").val("");
                $("#callinfo").html(okico + "修改成功！")
            } else {
                $("#form_payaccount button.green").removeAttr("disabled");
                $("#callinfo").css({
                    color: "red"
                });
                $("#callinfo").html(errico + "失败:" + data + "")
            }
        })
    });
    $('#form_payaccount button[type="reset"]').click(function() {
        $("#form_payaccount button.green").removeAttr("disabled");
        $("#callinfo").html("")
    });
    $("#form_password button.green").click(function() {
        if ($("#email").val() == '') {
            $("#email").focus();
            $("#callinfo").html(errico + "请输入当前邮箱");
            return false
        };
        if ($("#oldpass").val() == '') {
            $("#oldpass").focus();
            $("#callinfo").html(errico + "请输入旧密码");
            return false
        };
        if ($("#newpass").val() == '') {
            $("#newpass").focus();
            $("#callinfo").html(errico + "请输入新密码");
            return false
        };
        if ($("#repass").val() == '') {
            $("#repass").focus();
            $("#callinfo").html(errico + "请确认新密码");
            return false
        };
        if ($("#repass").val() !== $("#newpass").val()) {
            $("#repass").focus();
            $("#callinfo").html(errico + "新密码两次输入不一致");
            return false
        };
        $("#callinfo").css({
            color: "#666666"
        });
        $(this).attr("disabled", true);
        $("#callinfo").html(ldico + "正在提交请求");
        var r = Math.random();
        $.get("/Member/ajax/ModiPwd.ashx?r=" + r.toLocaleString(), serializex($("#aspnetForm").serialize()),
        function(data, textStatus) {
            if (data == "true") {
                $("#form_password button.green").removeAttr("disabled");
                $("#callinfo").css({
                    color: "green"
                });
                $(".required").val("");
                $("#callinfo").html(okico + "操作成功!")
            } else {
                $("#form_password button.green").removeAttr("disabled");
                $("#callinfo").css({
                    color: "red"
                });
                $("#callinfo").html(errico + "失败:" + data + "")
            }
        })
    });
    $('#form_password button[type="reset"]').click(function() {
        $("#form_password button.green").removeAttr("disabled");
        $("#callinfo").html("")
    });
    $("#form_cover button.green").click(function() {
        if ($('#oldans').length > 0) {
            if ($("#oldans").val() == '') {
                $("#oldans").focus();
                $("#callinfo").html(errico + "请输入旧的密保答案");
                return false
            };
            if ($("#oldans").val().length > 15) {
                $("#oldans").focus();
                $("#callinfo").html(errico + "问题或答案不能超过15个字");
                return false
            }
        };
        if ($("#newques").val() == '') {
            $("#newques").focus();
            $("#callinfo").html(errico + "请输入新的密保问题");
            return false
        };
        if ($("#newques").val().length > 15) {
            $("#newques").focus();
            $("#callinfo").html(errico + "问题或答案不能超过15个字");
            return false
        };
        if ($("#newans").val() == '') {
            $("#newans").focus();
            $("#callinfo").html(errico + "请输入新的密保答案");
            return false
        };
        if ($("#newans").val().length > 15) {
            $("#newans").focus();
            $("#callinfo").html(errico + "问题或答案不能超过15个字");
            return false
        };
        $("#callinfo").css({
            color: "#666666"
        });
        $(this).attr("disabled", true);
        $("#callinfo").html(ldico + "正在提交请求");
        $.get("/Member/ajax/Cover.ashx", serializex($("#aspnetForm").serialize()),
        function(data, textStatus) {
            if (data == "true") {
                $("#form_cover button.green").removeAttr("disabled");
                $("#callinfo").css({
                    color: "green"
                });
                $(".required").val("");
                $("#callinfo").html(okico + "操作成功!")
            } else {
                $("#form_cover button.green").removeAttr("disabled");
                $("#callinfo").css({
                    color: "red"
                });
                $("#callinfo").html(errico + "失败:" + data + "")
            }
        })
    });
    $('#form_cover button[type="reset"]').click(function() {
        $("#form_cover button.green").removeAttr("disabled");
        $("#callinfo").html("")
    });
    $("#form_userinfo button.green").click(function() {
        $("#callinfo").css({
            color: "#666666"
        });
        $(this).attr("disabled", true);
        $("#callinfo").html(ldico + "正在提交请求");
        var r = Math.random();
        $.get("/Member/ajax/userinfo.ashx?r=" + r.toString(), serializex($("#aspnetForm").serialize()),
        function(data, textStatus) {
            if (data == "true") {
                $("#form_userinfo button.green").removeAttr("disabled");
                $("#callinfo").css({
                    color: "green"
                });
                $(".required").val("");
                $("#callinfo").html(okico + "操作成功!")
            } else {
                $("#form_userinfo button.green").removeAttr("disabled");
                $("#callinfo").css({
                    color: "red"
                });
                $("#callinfo").html(errico + "失败:" + data + "")
            }
        })
    });
    $('#form_userinfo button[type="reset"]').click(function() {
        $("#form_userinfo button.green").removeAttr("disabled");
        $("#callinfo").html("")
    });
    $("#phoneinput a").click(function() {
        $("#phoneinput").fadeOut();
        $("#phoneputbox").fadeIn();
        $("#yphoneputbox").fadeIn();
        $("#phonecodebox").fadeIn();
        $('#phonetxt').text("新手机号码");
        $('#formflag').html("")
        $('#action').val("modiphone"); //add by vivisoft
    });
    $("#phoneinput_close a").click(function() {
        $("#phoneinput").fadeIn();
        $("#phoneputbox").fadeOut();
        $("#yphoneputbox").fadeOut();
        $("#phonecodebox").fadeOut();
        $('#phonetxt').text("手机号码");
        $('#formflag').html("")
        $('#action').val("renew"); //add by vivisoft
    });
    $("#form_cashpass button.green").click(function() {
        if ($("#loginpass").val() == '') {
            $("#loginpass").focus();
            $("#callinfo").html(errico + "请输入您的登录密码，以便验证身份");
            return false
        };
        if ($("#email").val() == '') {
            $("#email").focus();
            $("#callinfo").html(errico + "请输入您的账户邮箱");
            return false
        };
        if ($("#newpass").val() == '') {
            $("#newpass").focus();
            $("#callinfo").html(errico + "请输入您要设置的提现密码");
            return false
        };
        if ($("#newpass").val().length < 6) {
            $("#newpass").focus();
            $("#callinfo").html(errico + "请输入不小于6位数的字母、符号、数字组合");
            return false
        };
        if ($("#repass").val() == '') {
            $("#repass").focus();
            $("#callinfo").html(errico + "请确认您要设置的提现密码");
            return false
        };
        if ($("#repass").val().length < 6) {
            $("#repass").focus();
            $("#callinfo").html(errico + "请输入不小于6位数的字母、符号、数字组合");
            return false
        };
        if ($("#repass").val() !== $("#newpass").val()) {
            $("#repass").focus();
            $("#callinfo").html(errico + "两次提现密码输入不一致");
            return false
        };
        $("#callinfo").css({
            color: "#666666"
        });
        $(this).attr("disabled", true);
        $("#callinfo").html(ldico + "正在提交请求");
        $.get("/Member/ajax/cashpass.ashx", serializex($("#aspnetForm").serialize()),
        function(data, textStatus) {
            if (data == "true") {
                $("#form_cashpass button.green").removeAttr("disabled");
                $("#callinfo").css({
                    color: "green"
                });
                $(".required").val("");
                $("#callinfo").html(okico + "操作成功!")
            } else {
                $("#form_cashpass button.green").removeAttr("disabled");
                $("#callinfo").css({
                    color: "red"
                });
                $("#callinfo").html(errico + "失败:" + data + "")
            }
        })
    });
    $('#form_cashpass button[type="reset"]').click(function() {
        $("#form_cashpass button.green").removeAttr("disabled");
        $("#callinfo").html("")
    });
    $("#form_email button.green").click(function() {
        if ($("#loginpass").val() == '') {
            $("#loginpass").focus();
            $("#callinfo").html(errico + "请输入您的登录密码，以便验证身份");
            return false
        };
        if ($("#email").val() == '') {
            $("#email").focus();
            $("#callinfo").html(errico + "请输入您的账户邮箱");
            return false
        };
        if ($("#newemail").val() == '') {
            $("#newemail").focus();
            $("#callinfo").html(errico + "请输入您要设置的新邮箱账号");
            return false
        };
        $("#callinfo").css({
            color: "#666666"
        });
        $(this).attr("disabled", true);
        $("#callinfo").html(ldico + "正在提交请求");
        var r = Math.random();
        $.get("/Member/ajax/Email.ashx?r=" + r.toString(), serializex($("#aspnetForm").serialize()),
        function(data, textStatus) {
            if (data == "true") {
                $("#form_email button.green").removeAttr("disabled");
                $("#callinfo").css({
                    color: "green"
                });
                $(".required").val("");
                $("#callinfo").html(okico + "操作成功!")
            } else {
                $("#form_email button.green").removeAttr("disabled");
                $("#callinfo").css({
                    color: "red"
                });
                $("#callinfo").html(errico + "失败:" + data + "")
            }
        })
    });
    $('#form_email button[type="reset"]').click(function() {
        $("#form_email button.green").removeAttr("disabled");
        $("#callinfo").html("")
    });
    $("#form_person button.green").click(function() {
        var personid = $("#pernumber");
        var rpersonId = $("#rpernumber");
        if (personid.val() == '') {
            personid.focus();
            $("#callinfo").html(errico + "请输入您的身份证号");
            return false
        };
        if (personid.val().length != 18) {
            personid.focus();
            $("#callinfo").html(errico + "请正确输入您的身份证号");
            return false
        };
        if (rpersonId.val().length != 18) {
            rpersonId.focus();
            $("#callinfo").html(errico + "请确认您的身份证号");
            return false
        };
        if (rpersonId.val() !== personid.val()) {
            rpersonId.focus();
            $("#callinfo").html(errico + "两次身份证号输入不一致!");
            return false
        };
        $("#callinfo").css({
            color: "#666666"
        });
        $(this).attr("disabled", true);
        var r = Math.random();
        $.get("/Member/ajax/DPI.ashx?r=" + r.toString(), serializex($("#aspnetForm").serialize()),
        function(data, textStatus) {
            if (data == "true") {
                document.location.href = 'trna2.aspx';
            } else {
                $("#form_person button.green").removeAttr("disabled");
                $("#callinfo").css({
                    color: "red"
                });
                $("#callinfo").html(errico + "失败:" + data + "");
                return false
            }
        })
    });
    $("#form_person3 button.green").click(function() {
        $("#callinfo").css({
            color: "#666666"
        });
        $(this).attr("disabled", true);
        $("#callinfo").html(ldico + "正在提交请求");
        $.get("/Member/ajax/DPI2.ashx", serializex($("#aspnetForm").serialize()),
        function(data, textStatus) {
            if (data == "true") {
                document.location.href = 'trnaresult.aspx';
            } else {
                $("#form_person3 button.green").removeAttr("disabled");
                $("#callinfo").css({
                    color: "red"
                });
                $("#callinfo").html(errico + "失败:" + data + "");
                return false
            }
        })
    });
    $("#form_person3 button.goback").click(function() {
        history.go(-1)
    });
    $('#form_person button[type="reset"]').click(function() {
        $("#form_person button.green").removeAttr("disabled");
        $("#callinfo").html("")
    });
    $("#personinput a").click(function() {
        $("#personinput").fadeOut();
        $("#personbox").fadeIn();
        $("#action").val("modiname");
    });
    $("#personinput_close a").click(function() {
        $("#personbox").fadeOut();
        $("#personinput").fadeIn()
        $("#action").val("");
    });
    $("#form_askpay button.green").click(function() {
        var moneyzh = $("#balance").val() - $("#cashfee").val();
        if ($("#txtype").val() == '') {
            $("#txtype").focus();
            $("#callinfo").html(errico + "请选择提现方式");
            return false
        };
        if ($("#payMoney").val() == '') {
            $("#payMoney").focus();
            $("#callinfo").html(errico + "请输入您要提现的金额");
            return false
        };
        if ($("#safepass").val() == '') {
            $("#safepass").focus();
            $("#callinfo").html(errico + "请输入您的提现密码");
            return false
        };
        if ($("#payMoney").val() > moneyzh) {
            $("#payMoney").focus();
            $("#callinfo").html(errico + "余额不足,请修改提现金额");
            return false
        };
        $("#callinfo").css({
            color: "#666666"
        });
        $(this).attr("disabled", true);
        $("#callinfo").html(ldico + "正在提交请求");
        $.get("/Member/ajax/Askpay.ashx?r=" + Math.random(), serializex($("#aspnetForm").serialize()),
        function(data, textStatus) {
            if (data == "true") {
                $("#form_askpay button.green").removeAttr("disabled");
                $("#callinfo").css({
                    color: "green"
                });
                $(".required").val("");
                $("#callinfo").html(okico + "操作成功!")
            } else {
                $("#form_askpay button.green").removeAttr("disabled");
                $("#callinfo").css({
                    color: "red"
                });
                $("#callinfo").html(errico + "失败:" + data + "")
            }
        })
    });
    $('#form_askpay button[type="reset"]').click(function() {
        $("#form_askpay button.green").removeAttr("disabled");
        $('#box-daxie').fadeOut();
        $("#callinfo").html("")
    });
    $("#mylogs tbody tr,#m_orderlist tbody tr,#m_channellist tbody tr,#mywenda tbody tr,#mytables tbody tr").hover(function() {
        $(this).css("background", "#F2F2F2")
    },
    function() {
        $(this).css("background", "#ffffff")
    });
    $("#form_askpay #payMoney").blur(function() {
        if ($("#payMoney").val() !== '') {
            $('#box-daxie').fadeIn()
        };
        $("#Moneydaxie").html(ldico + "Loading..");
        var payMoneyvalue = $("#payMoney").val();
        $.get("/Member/ajax/getdaxie.ashx", { payMoney: payMoneyvalue },
        function(data, textStatus) {
            if (data == "true") { } else {
                $("#Moneydaxie").html(data)
            }
        })
    });
    $("a#sendmsg").click(function() {
        $("#callinfo").html(ldico + "正在发送验证码");
        $.get("/Userlogin/ajax/PhoneValid_new.ashx?t=" + Math.random(), serializex($("#aspnetForm").serialize()),
        function(data, textStatus) {
            if (data == "true") {
                $("#callinfo").html(okico + "验证码发送成功!")
            } else {
                $("#callinfo").css({
                    color: "red"
                });
                $("#callinfo").html(errico + data + "")
            }
        })
    });
    $.fn.numeral = function() {
        this.bind("keypress",
        function() {
            if (event.keyCode == 46) {
                if (this.value.indexOf(".") != -1) {
                    return false
                }
            } else {
                return event.keyCode >= 46 && event.keyCode <= 57
            }
        });
        this.bind("blur",
        function() {
            if (this.value.lastIndexOf(".") == (this.value.length - 1)) {
                this.value = this.value.substr(0, this.value.length - 1)
            } else if (isNaN(this.value)) {
                this.value = ""
            }
        });
        this.bind("paste",
        function() {
            var s = clipboardData.getData('text');
            if (!/\D/.test(s));
            value = s.replace(/^0*/, '');
            return false
        });
        this.bind("dragenter",
        function() {
            return false
        });
        this.bind("keyup",
        function() {
            if (/(^0+)/.test(this.value)) {
                this.value = this.value.replace(/^0*/, '')
            }
        })
    };
    $(".onlynumber").numeral();
    $("input.stype_szyz").click(function() {
        if ($(this).val() == 3) {
            $('#searchbox-n div:eq(0)').slideUp()
        } else {
            $('#searchbox-n div:eq(0)').slideDown()
        }
    });
    $("input.i_radio").click(function() {
        {
            $('#xmoney').html($(this).val());
            $('#mutixmoney').html($(this).val());
            $('#ctl00_ContentPlaceHolder1_xk_faceValue').val($(this).val());
        }
    })
})