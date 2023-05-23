<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="regedit.aspx.cs" Inherits="viviapi.WebUI.regedit" %>

<!DOCTYPE html>
<!-- saved from url=(0036)/register.aspx -->
<html lang="en">
<head id="Head1" runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="keywords" content="app支付,支付接口,支付API,支付宝接口,微信支付接口,微信支付,支付宝支付&quot;">
    <link rel="shortcut icon" type="image/x-icon" href="/favicon.ico" media="screen">
    <link rel="stylesheet" href="css/base.css">
    <title> 云上支付 |让支付更简单</title>
    <link href="css/polyPay.css" rel="stylesheet">
	<link rel="stylesheet" href="css/form.css">
	<link rel="stylesheet" href="css/layer.css" id="layui_layer_skinlayercss">

  <script src="js/jquery-1.8.2.min.js" type="text/javascript"></script>
  <script src="js/jquery.blockUI.js" type="text/javascript"></script>
  <script src="js/jquery.form.js" type="text/javascript"></script>
  <script type="text/javascript" src="js/AllCheckForm.js"></script>
  <script language="javascript">
      function secBoard(n) {
          for (i = 0; i < secTable.cells.length; i++)
              secTable.cells[i].className = "sec1";
          secTable.cells[n].className = "sec2";
          for (i = 0; i < mainTable.tBodies.length; i++)
              mainTable.tBodies[i].style.display = "none";
          mainTable.tBodies[n].style.display = "block";
      }
      function refreshValidateCode(_id, url) {
          document.getElementById(_id).src = url + "?date=" + new Date();
      }
      function mOvr(src) {
          if (!src.contains(event.fromElement)) {
              src.style.cursor = 'hand'; src.bgColor = "#ffffff";
          }
      }
      function mOut(src) {
          if (!src.contains(event.toElement)) {
              src.style.cursor = 'default'; src.bgColor = "#FAFAFA";
          }
      }
      function mClk(src) {
          if (event.srcElement.tagName == 'TD') {
              src.children.tags('A')[0].click();
          }
      }

           
    </script>
    <script>
        //商户类型选择
        function ChkRealityType(val) {
            $("#label1").removeClass();
            $("#label2").removeClass();
            if (val == 1) {
                document.getElementById("company").style.display = '';
                document.getElementById("idcard_1").style.display = '';
                document.getElementById("idcard_2").style.display = 'none';
                $("#label1").addClass("Vrabom Visio");
                $("#label2").addClass("Vrabom");
                $("#merchantType").val("company");
            } else {
                document.getElementById("company").style.display = 'none';
                document.getElementById("idcard_1").style.display = 'none';
                document.getElementById("idcard_2").style.display = '';
                $("#label1").addClass("Vrabom");
                $("#label2").addClass("Vrabom Visio");
                $("#merchantType").val("personal");
            }
        }

        $(document).ready(function () {
            $("#label1").addClass("Vrabom Visio");

            $("#area").ProvinceCity("province", "city", "country");
            $("#country").attr("validate", "{required:true,messages:{required:'请选择所属区域'}}");
            $("#province").addClass("VSelect");
            $("#city").addClass("VSelect pushZ1");
            $("#country").addClass("VSelect");

            // 校验form表单中的参数是否验证
            $("#thisFrm").validate({
                rules: {
                    userName: {
                        required: true,
                        rangelength: [4, 20],
                        isUsernameRegex: true,
                        isNickName: true
                    },
                    userPassword: {
                        required: true,
                        rangelength: [6, 20]
                    },
                    confirmPassword: {
                        required: true,
                        rangelength: [6, 20],
                        equalTo: '#userPassword'
                    },
                    companyName: {
                        required: '#realitytype1:checked',
                        rangelength: [2, 32]
                    },
                    contactName: {
                        required: true,
                        rangelength: [2, 32],
                        isContactNameRegex: true
                    },
                    companyLicenseNumber: {
                        required: '#realitytype1:checked',
                        rangelength: [2, 32]
                    },
                    identityCard: {
                        required: '#realitytype2:checked',
                        isIdCardNo: true
                    },
                    cellPhoneNumber: {
                        required: true,
                        isMobile: true
                    },
                    email: {
                        required: true,
                        email: true
                    },
                    captcha: {
                        required: true
                    },
                    country: {
                        required: true
                    }
                },
            });

            var options = {
                url: '/register',
                type: 'post',
                beforeSubmit: function () {
                    $('#loadingBox').html("注册中，请等待。。。");
                    if ($("#thisFrm").valid()) {
                        $.blockUI({ message: $('#loadingBox'), overlayCSS: { backgroundColor: '#2f4f4f'} });
                        return true;
                    } else {
                        return false;
                    }
                },
                dataType: 'json',
                success: function (data) {
                    if (data.result == 'success') {
                        //
                        setTimeout(function () {
                            $('#loadingBox').html("注册成功！");
                            setTimeout(function () {
                                window.location.href = '/login';
                            }, 3000);
                        }, 3000)
                    } else {
                        setTimeout(function () {
                            var emsg = data.errMsg;
                            $('#loadingBox').html(emsg);
                            $('#errMsg').html(emsg);

                            setTimeout(unblock, 2000);
                            refreshmvp();
                        }, 3000)
                    }
                } //edn success
            };
            $('#thisFrm').ajaxForm(options);

            $(".ShowXieyi").toggle(function () { $(".xieyiArea").show(); }, function () { $(".xieyiArea").hide(); });
        })
        var VerifyCodeTimes = 1;
        function refreshmvp() {
            $("#cimg").attr("src", "/captcha/image?" + (VerifyCodeTimes++));
        }
        jQuery.validator.addMethod("isNickName", function (value) {
            return valNickName(value);
        }, "该用户名已被使用");

        //验证用户名是否存在
        function valNickName(value) {
            var ret = $.ajax({
                url: "/register/checkusername",
                async: false,
                cache: false,
                type: 'post',
                data: "username=" + value
            }).responseText;

            var result = eval("(" + ret + ")");
            return !result.data;
        }
        jQuery.validator.addMethod("isMobile", function (value) {
            var length = value.length;
            return (length == 11 && /^(((13[0-9]{1})|(15[0-9]{1})|(147)|(18[0-9]{1}))+\d{8})$/.test(value));
        }, "手机号格式不正确");
        jQuery.validator.addMethod("isUsernameRegex", function (value) {
            return (/^[A-Za-z0-9@\u4e00-\u9fa5]+$/.test(value));
        }, "账户名必须是字母、中文、数字！");
        jQuery.validator.addMethod("isContactNameRegex", function (value) {
            return (/^[A-Za-z\u4e00-\u9fa5]+$/.test(value));
        }, "必须是中文，字母！");
        //验证身份证
        function isIdCardNo(num) {
            var qiyeChk = document.getElementById("realitytype1");
            if (qiyeChk.checked) {
                return true;
            } else {
                num = num.toUpperCase();
                //身份证号码为15位或者18位，15位时全为数字，18位前17位为数字，最后一位是校验位，可能为数字或字符X。
                if (!(/(^\d{15}$)|(^\d{17}([0-9]|X)$)/.test(num))) {
                    return false;
                }
                //校验位按照ISO 7064:1983.MOD 11-2的规定生成，X可以认为是数字10。
                //下面分别分析出生日期和校验位
                var len, re;
                len = num.length;
                if (len == 15) {
                    re = new RegExp(/^(\d{6})(\d{2})(\d{2})(\d{2})(\d{3})$/);
                    var arrSplit = num.match(re);

                    //检查生日日期是否正确
                    var dtmBirth = new Date('19' + arrSplit[2] + '/' + arrSplit[3] + '/' + arrSplit[4]);
                    var bGoodDay;
                    bGoodDay = (dtmBirth.getYear() == Number(arrSplit[2])) && ((dtmBirth.getMonth() + 1) == Number(arrSplit[3])) && (dtmBirth.getDate() == Number(arrSplit[4]));
                    if (!bGoodDay) {
                        return false;
                    } else {
                        //将15位身份证转成18位
                        //校验位按照ISO 7064:1983.MOD 11-2的规定生成，X可以认为是数字10。
                        var arrInt = new Array(7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2);
                        var arrCh = new Array('1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2');
                        var nTemp = 0, i;
                        num = num.substr(0, 6) + '19' + num.substr(6, num.length - 6);
                        for (i = 0; i < 17; i++) {
                            nTemp += num.substr(i, 1) * arrInt[i];
                        }
                        num += arrCh[nTemp % 11];
                        return num;
                    }
                }
                if (len == 18) {
                    re = new RegExp(/^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9]|X)$/);
                    var arrSplit = num.match(re);

                    //检查生日日期是否正确
                    var dtmBirth = new Date(arrSplit[2] + "/" + arrSplit[3] + "/" + arrSplit[4]);
                    var bGoodDay;
                    bGoodDay = (dtmBirth.getFullYear() == Number(arrSplit[2])) && ((dtmBirth.getMonth() + 1) == Number(arrSplit[3])) && (dtmBirth.getDate() == Number(arrSplit[4]));
                    if (!bGoodDay) {
                        return false;
                    } else {
                        //检验18位身份证的校验码是否正确。
                        //校验位按照ISO 7064:1983.MOD 11-2的规定生成，X可以认为是数字10。
                        var valnum;
                        var arrInt = new Array(7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2);
                        var arrCh = new Array('1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2');
                        var nTemp = 0, i;
                        for (i = 0; i < 17; i++) {
                            nTemp += num.substr(i, 1) * arrInt[i];
                        }
                        valnum = arrCh[nTemp % 11];
                        if (valnum != num.substr(17, 1)) {
                            return false;
                        }
                        return num;
                    }
                }
                return false;
            }
        }

        jQuery.validator.addMethod("isIdCardNo", function (value) {
            return isIdCardNo(value);
        }, "请正确输入您的身份证号码");

        function unblock() {
            $.unblockUI({ message: null });
        }
    </script>


   <script>
        $(document).ready(function () {
            $(".table3 input").focus(function () {
                $(this).css("border-color", "#fec157")
            });
            $(".table3 input").blur(function () {
                $(this).css("border-color", "#cfcecc")
            });
        });
        function SubmitForm() {
            if (!$("#Agreement").attr("checked")) {
                alert("您必须同意以下协议才能进行注册");
                $("#Agreement")[0].focus();
                return;
            }
            var LoginName = $("input[name='LoginName']").val();
            var LoginPassword = $("input[name='LoginPassword']").val();
            var ReLoginPassword = $("input[name='ReLoginPassword']").val();
            var MerchantName = $("input[name='MerchantName']").val();
            var Email = $("input[name='Email']").val();
            var ContactQQ = $("input[name='ContactQQ']").val();
            var SndCode = $("input[name='SndCode']").val();
			//新加
			var sfzh = $("input[name='sfzh']").val();
			var dhhm = $("input[name='dhhm']").val();
			//
            var postData = 'LoginName=' + LoginName + '&LoginPassword=' + LoginPassword + '&SndCode=' + SndCode + '&ReLoginPassword=' + ReLoginPassword + '&MerchantName=' + MerchantName + '&Email=' + Email + '&ContactQQ=' + ContactQQ + 'Sfzh'=Sfzh +'Dhhm'=Dhhm;
            $.ajax({
                dataType: "json",
                url: '/WebService/Register.ashx',
                type: 'POST',
                data: postData,
                success: function (json) {
                    if (json.result == 'ok') {
                        alert(json.msg);
                        location.href = json.url;
                    }
                    else {
                        alert(json.msg);

                    }
                }
            });
        }
        function ChangeRndImg() {
            $("#SndCode").val("");
            $("#YZMIMAGE").attr("src", "/CodeImage.aspx?r=" + Math.random());
            $("#SndCode")[0].focus();
        }
        var VerifyCodeTimes = 1;
        //刷新验证码
        function refreshValidateCode(_id, url) {
            document.getElementById(_id).src = url + "?date=" + new Date();
        }
  </script>
</head>

<body id="a" ng-app="bcLoginControllers" ng-controller="bcRegisterCtrl">
<div class="header">
    <!--[if lt IE 9]>
<style type="text/css">
    .header .nav > .logo {
        background-image: url("img/logo-160x36.png");
    }

    .header .nav > .logo.christmas {
        background-image: url("img/logo-christmas-small.png");
    }
</style>
<![endif]-->
<div class="fast-enter">
    <ul class="common-container">
      <i class="separator"></i></li>
        <li><a href="#" target="_blank">帮助中心</a><i class="separator"></i>
        </li>
        <li><a href="regedit.aspx">免费注册</a> <i class="separator"></i></li>
        <li><a href="login.aspx">登录</a></li>
    </ul>
</div>
<div class="nav common-container">
    <a href="" class="logo christmas"></a>
    <ul>
        <li><a href="" class="on">首页</a></li>
        <!--<li class="more">-->
        <!--<a href="javascript:void(0);" class="">公司产品<i class="common-icon icon-arrow-down"></i></a>-->
        <!--<div class="sub-nav">-->
        <!--<ul class="group">-->
        <!--<li>-->
        <!--<span class="title">支付接入</span>-->
        <!--<div class="group-list">-->
        <!--<a href="/Home_Products_sdk.html">支付SDK</a>-->
        <!--<a href="/Home_Products_jsbtn.html">秒支付Button</a>-->
        <!--<a href="/Home_Products_plugin.html">H5APP插件</a>-->
        <!--</div>-->
        <!--</li>-->
        <!--<li>-->
        <!--<span class="title">解决方案</span>-->
        <!--<div class="group-list">-->
        <!--<a href="/Home_Products_transfer.html">企业打款</a>-->
        <!--<a href="/Home_Products_subscribe.html">订阅支付</a>-->
        <!--<a href="/Home_Products_huami.html">花蜜付</a>-->
        <!--<a href="/Home_Products_cross.html">跨境收款</a>-->
        <!--</div>-->
        <!--</li>-->
        <!--<li>-->
        <!--<span class="title">商业智能</span>-->
        <!--<div class="group-list">-->
        <!--<a href="/Home_Products_platform.html">客户与营销</a>-->
        <!--</div>-->
        <!--</li>-->
        <!--<li>-->
        <!--<span class="title">辅助功能</span>-->
        <!--<div class="group-list">-->
        <!--<a href="/Home_Products_auth.html">实名认证接口</a>-->
        <!--</div>-->
        <!--</li>-->
        <!--</ul>-->
        <!--</div>-->
        <!--</li>-->
        <li><a href="download.aspx" class="">SDK下载</a></li>
        <li>
            <a href="#" class="more">开发文档</a>
            <!--<div class="sub-nav">-->
            <!--<ul>-->
            <!--&lt;!&ndash;<li><a href="/Home_Apply_index.html">快速开始</a></li>&ndash;&gt;-->
            <!--<li><a href="#">开发文档</a></li>-->
            <!--&lt;!&ndash;<li><a href="https://blog.beecloud.cn/" target="_blank">博客</a></li>&ndash;&gt;-->
            <!--</ul>-->
            <!--</div>-->
        </li>
        <!--<li><a href="/Home_Price_index.html" class="">支付体验</a></li>-->
        <!-- <li><a href="http://ceshi.qujuhe.com/pay/demo" target="_blank" class="">支付体验</a></li> -->
        <li><a href="login.aspx">管理中心</a></li>
        <!-- <li><a href="/life.html">去生活</a></li> -->
    </ul>
</div>
</div>
<div class="common-wrap" style="padding-top: 90px;">
    <div class="common-banner login-banner"></div>
    <div class="login-wrap ">
        <div class="form-title">注册</div>

        <form id="form1" runat="server">
            <input type="hidden" name="merchantType" id="merchantType" value="company">

        <div class="login-main">
            <div id="msg" ng-style="{color: &#39;#FF6C2C&#39;}" class="ng-binding" style="color: rgb(255, 108, 44);"></div>
            <div class="form-box">
                <i class="form-icon icon-account"></i>
                <i class="form-separator"></i>
                <input type="text" runat="server" class="form-input" id="newusername" name="newusername" placeholder="请输入您的账号">
            </div>
            <div class="form-box">
                <i class="form-icon icon-password"></i>
                <i class="form-separator"></i>
                <input type="password" runat="server" class="form-input" id="password1" name="password2" placeholder="请输入您的密码">
            </div>
            <div class="form-box">
                <i class="form-icon icon-password"></i>
                <i class="form-separator"></i>
                <input type="password" server class="form-input" id="password2" name="password2" placeholder="请再次输入您的密码">
            </div>
            <div class="form-box">
                <i class="form-icon icon-newfullname"></i>
                <i class="form-separator"></i>
                <input type="text" runat="server" class="form-input" id="newfullname" name="newfullname" placeholder="请输入您的真实姓名">
            </div>
            <div class="form-box">
                <i class="form-icon icon-newqq"></i>
                <i class="form-separator"></i>
                <input type="text" runat="server" class="form-input" id="newqq" name="newqq" placeholder="请输入您的QQ号码">
            </div>
            <div class="form-box">
                <i class="form-icon icon-email"></i>
                <i class="form-separator"></i>
                <input type="text" runat="server" class="form-input" id="newemail" name="newemail" placeholder="请输入您的邮箱">
            </div>

            <div class="form-box">
                <i class="form-icon icon-password"></i>
                <i class="form-separator"></i>
                <input type="text" name="txtvcode" id="txtvcode"  class="form-input" size="20" maxlength="5" autocomplete="off" detail="请输入图片上的验证码" msg="验证码错误或过期,请刷新后重新输入！" onfocus="Validator.ShowDetail(this)" onblur="Validator.CheckField(this)">
            </div>

            <div class="form-box">
                <label class="userverification col-sm-8">
                    <a href="javascript:refreshValidateCode('imgValidateCode','/vercode.aspx');"><img id='img1' src="/vercode.aspx" width="101" height="33" title="看不清？点击更换另一个。"/></a>
                </label>
            </div>
<!--
            <div class="form-box">
                <i class="form-icon icon-password"></i>
                <i class="form-separator"></i>
                <input type="text" id="invitecode" name="invitecode" class="form-input" placeholder="必须有邀请码才能注册" value="">
            </div>
-->
            <div class="form-box">
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <asp:ImageButton ID="Button1" class="form-btn register" runat="server" ImageUrl="~/img/dl_tjsuc.png" onclick="Submit_Click" />
            </div>
            <p class="other-enter center clearfix">
                <span>已注册，点此</span>
                <a href="login.aspx" class="link-go">立即登录</a>
            </p>
        </div>
        </form>
    </div>
</div>

<div class="footer">

    <div class="con clearfix">
        <dl>
            <dt><i class="common-icon icon-about-us"></i><span>关于我们</span></dt>
            <!--<dd><a href="/Home_About_index.html">创始人</a></dd>-->
            <dd><a>创始人</a></dd>
            <!--<dd><a href="/Home_About_index.html#intro">公司简介</a></dd>-->
            <dd><a>公司简介</a></dd>
            <!--<dd><a href="/Home_About_index.html#honor">公司荣誉</a></dd>-->
            <dd><a>公司荣誉</a></dd>
            <!--<dd><a href="/Home_About_contact.html">联系我们</a></dd>-->
            <dd><a>联系我们</a></dd>
        </dl>
        <dl>
            <dt><i class="common-icon icon-doc"></i><span>文档</span></dt>
            <dd><a>快速开始</a></dd>
            <dd><a href="#">开发文档</a></dd>
            <!--<dd><a href="/Home_Video_index.html">视频教程</a></dd>-->
            <dd><a>视频教程</a></dd>
            <!--<dd><a href="https://blog.beecloud.cn">博客</a></dd>-->
        </dl>
        <!--        <dl>-->
        <!--            <dt><i class="common-icon icon-job"></i><span>招聘</span></dt>-->
        <!--            <dd><a href="/job">福利待遇</a></dd>-->
        <!--            <dd><a href="/job">招聘职位</a></dd>-->
        <!--        </dl>-->
        <dl>
            <dt><i class="common-icon icon-coor"></i><span>合作</span></dt>
            <!--            <dd><a href="/partner/">合作信息</a></dd>-->
            <dd><a>合作伙伴</a></dd>
        </dl>
        <dl>
            <dt><i class="common-icon icon-media"></i><span>媒体</span></dt>
            <!--<dd><a href="/Home_Media_index.html">媒体报道</a></dd>-->
            <!--<dd><a href="/Home_Media_index.html#download">媒体资源</a></dd>-->
        </dl>
        <dl>
            <dt><i class="common-icon icon-agreement"></i><span>合作协议</span></dt>
            <dd><a href="dashboard.html">用户协议</a></dd>
        </dl>
        <dl class="last">
            <dd>
                如果，你觉得<br>
                世界辣么大，你想换个工作环境看看<br>
                那么，就来 云上支付吧<br>
                这里是互联网支付领域里的先锋<br>
                更是极客与大牛的集中营
            </dd>
            <p>
                <!--<a href="/Home_Job_index.html" class="common-btn join-us-btn">加入我们</a>-->
                <a class="common-btn join-us-btn">加入我们</a>
            </p>
        </dl>
    </div>
    <div class="copyright-box common-container" style="color: black">
        <p><b>一站式支付解决方案提供商</b></p>
        <p>Copyright©2014-2017 @云上支付 yunsonpay.com </p>
      </div>
</div>
</body>
</html>
