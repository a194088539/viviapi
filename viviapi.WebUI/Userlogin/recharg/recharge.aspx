<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="recharge.aspx.cs" Inherits="viviapi.WebUI.Userlogin.recharg.recharge" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Userlogin/static/style/master.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="../css/mytablelist.css" />
    <script src="/Userlogin/static/js/lib/jquery-1.4.2.js" type="text/javascript"></script>

    <script src="/js/jquery.zxxbox.3.0.js" type="text/javascript"></script>

    <script type="text/javascript">
        jQuery(document).ready(function() {
            $("#ibtnSave").click(function() {

                var money = $("#txtRechargeMoney").val();
                if (money == "") {
                    $("#callinfo").html(errico + "请输入充值金额");
                    return false;
                };
            });

            $("#txtRechargeMoney").blur(function() {
                getactualMoney();
            });

            $('input:radio[name="bank_list"]').change(function() {
                getactualMoney();
            });
        });
        function getactualMoney() {
            $("#txtactualMoney").val("0.00");
            var _val = $("#txtRechargeMoney").val();
            var _bank = $('input:radio[name="bank_list"]:checked').val();

            if (_val != "") {
                $.get("/Userlogin/ajax/GetActualMoney_new.ashx?t=" + Math.random(), { rechargemoney: _val, bank: _bank },
            function(data) {
                $("#txtactualMoney").val(data);
            })
            }
        }
    </script>

    <script language="javascript">
        $(document).ready(function() {
            //千分位处理函数   
            var departNum = function(textVal, the_other) {
                var the_array = [];
                var i = 0;
                the_array.push(textVal.slice(textVal.length - 2, textVal.length));
                for (i = textVal.length - 5; i >= 0; i -= 3) {
                    the_array.push(textVal.slice(i, i + 3));
                } if (0 - i < 3) { the_array.push(textVal.slice(0, 3 + i)); } for (var k = the_array.length - 1; k >= 0; k--) { the_other.push(the_array[k]); }
            }
            //输入限制
            $("#txtRechargeMoney").keypress(function(event) {
                var current = $(this).val();
                if (event.keyCode && (event.keyCode < 45 || (event.keyCode > 45 && event.keyCode < 48) || event.keyCode > 57)) {
                    if (event.keyCode == 46 && !/\./.test(current)) {
                        if (!isNaN(parseInt($(this).val().replace(/,/, "")))) {
                            $(this).val(current + ".");
                        }
                        else { $(this).val($(this).val() + "0."); }
                    }
                    event.preventDefault();
                } else {
                    if (event.keyCode == 45 && /-/.test(current)) { event.preventDefault(); }
                    else if (event.keyCode != 45) {
                        if (!/\./.test(current)) {
                            var the_new = $(this).val().replace(/,/g, "");
                            var theArray = [];
                            var theFlag = "";
                            if (/-/.test(current)) { theFlag = the_new.slice(0, 1); the_new = the_new.slice(1); }
                            if (parseInt(the_new) >= 100) {
                                departNum(the_new, theArray);
                                $(this).val(theFlag + theArray.join(","));
                            }
                        }
                    }
                }
            }).keyup(function(event) {
                if (event.keyCode == 109 && $(this).val().slice(0, 1) != "-") {
                    var the_Real = $(this).val(); $(this).val(the_Real.replace(/-/, ""));
                }
            }).blur(function() {
                var the_Val = $(this).val().replace(/,/g, "");
                if (!isNaN(parseFloat(the_Val))) {
                    if (!/\./.test(the_Val)) {
                        var theArray = []; var theFlag = "";
                        var the_one = the_Val.slice(-1);
                        var the_new = the_Val.replace(/\d$/, "");
                        if (/-/.test(the_Val)) { theFlag = the_new.slice(0, 1); the_new = the_new.slice(1); }
                        if (parseInt(the_new) >= 100) {
                            departNum(the_new, theArray);
                            $(this).val(theFlag + theArray.join(",") + the_one + ".00");
                        }
                        else { $(this).val(the_Val + ".00"); }
                    }
                    else {
                        var theArray = [];
                        var theFlag = "";
                        var the_now = parseFloat(the_Val).toFixed(2);
                        var the_nowStr = String(the_now).slice(-4);
                        var the_new = String(the_now).replace(/\d\.\d\d/, "");
                        if (/-/.test(the_Val)) { theFlag = the_new.slice(0, 1); the_new = the_new.slice(1); }
                        if (parseInt(the_new) >= 100) {
                            departNum(the_new, theArray);
                            $(this).val(theFlag + theArray.join(",") + the_nowStr);
                        } else { $(this).val(String(the_now)); }
                    }
                }
            });
        });

        function selectBank() {

        }
        
    </script>

    <script type="text/javascript">
        function paySubmit() {
            if (! +[1, ]) {
                $("#selBank").zxxbox();
            } else {
                alert("如果不能正常充值，请尝试使用IE浏览器进行充值！");
                $("#selBank").zxxbox();
            }
        }
        //查询网银订单
        function sel() {
            location.href = "rechargelist.aspx";
        }
        function twoSub() {
            $.zxxbox.hide();
        }
    </script>

    <style>
        .c_btn_bg1
        {
            width: 120px;
            height: 26px;
            border: none;
            background: url("/style/images/c_btn_bg1.gif") no-repeat;
        }
        .c_btn_bg
        {
            width: 89px;
            height: 26px;
            border: none;
            background: url("/style/images/c_btn_bg.gif") no-repeat;
        }
        .r_content
        {
            padding: 10px 10px 0px 10px;
            width: 847px;
            min-height: 413px;
            border-top: 1px #cdcdcd solid;
            border-left: 1px #cdcdcd solid;
            border-right: 1px #cdcdcd solid;
            border-bottom: 1px #cdcdcd solid;
            float: left;
        }
        .r_content .top_content
        {
            behavior: url(pie.htc);
            border-radius: 8px;
            position: relative;
            color: #585858;
            padding: 8px;
            margin: 0px auto;
            width: 831px;
            height: 47px;
            background-color: #E2E8F2;
            margin-bottom: 10px;
            float: left;
        }
        .rightborder
        {
            width: 28px;
            height: 453px;
            background-image: url(/style/images/rightborder.jpg);
            background-repeat: no-repeat;
            float: left;
        }
        .czk
        {
            width: 821px;
            float: left;
        }
        .czk dt
        {
            margin-bottom: 10px;
            width: 801px;
            border-bottom: 1px #cccccc solid;
            float: left;
            height: 25px;
            line-height: 25px;
            padding-left: 20px;
            font-family: "微软雅黑";
            font-size: 13px;
            color: #060606;
        }
        .czk dd
        {
            margin-bottom: 10px;
            margin-left: 47px;
            width: 214px;
            height: 38px;
            float: left;
            line-height: 38px;
        }
        .czk dd a
        {
            color: #333333;
            text-decoration: none;
            float: left;
        }
        .czk dd input
        {
            margin-top: 13px;
            margin-right: 9px;
            float: left;
        }
        .czk dd a:link img
        {
            border: 0px #dddddd solid;
            width: 190px;
            height: 36px;
            float: left;
        }
        .czk dd a:hover img
        {
            border: 1px #ffaa33 solid;
            width: 190px;
            height: 36px;
            float: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/Userlogin/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/Userlogin/recharg/index.aspx'">账户充值</a>
        &nbsp;&gt;&nbsp; <span>账户充值</span>
    </div>
    <!--右部表单开始-->
    <div id="list_content" style="padding-top: 0px;">
        <h2>
            在线充值</h2>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="2" colspan="3" align="left" >
                </td>
            </tr>
            <tr>           <td height="39" align="left" >
                    充值金额:
                </td>
                <td align="left" >
                   <asp:TextBox ID="txtRechargeMoney" runat="server" MaxLength="15" CssClass="txt_01"></asp:TextBox>
				   
                </td>
                <td height="39" align="left" >
                   
                </td>
            </tr>
            <tr>
                <td height="39" align="left" >
                    到账金额:
                </td>
                <td align="left" >
                   <em class="font14"><b class="txtc"><asp:TextBox ID="txtactualMoney" runat="server" MaxLength="15" CssClass="txt_02"></asp:TextBox></b></em>
                </td>
                <td height="39" align="left" >
                </td>
            </tr>
            <tr>
                <td height="39" colspan="10">
                    <div class="r_content">
                        <div class="top_content">
                            所有充值业务全及时生效(网络高峰期5-10分钟等待)，充值完成以后,请您在指定NPC处领取.无需向管理 员索取。使用神州行，点卡充值成功后，没领取到服务，请不要关闭充值页面，等待交易成功！
                        </div>
                        <dl class="czk">
                            <dt>选择充值方式</dt>
                            <dd>
                                <input type="radio" name="bank_list" id="bank_list" value="967" onclick="selectBank()"
                                    checked="checked"><img src="/style/images/paybank/pic08.jpg" align="absmiddle" alt="工商银行"
                                        title="工商银行" /></dd>
                            <dd>
                                <input nput type="radio" name="bank_list" id="Radio1" value="970" onclick="selectBank()">
                                    <img src="/style/images/paybank/pic09.jpg" align="absmiddle" alt="招商银行" title="招商银行" />
                            </dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio2" value="965" onclick="selectBank()"><img
                                    src="/style/images/paybank/pic10.jpg" align="absmiddle" alt="建设银行" title="建设银行" /></dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio3" value="964" onclick="selectBank()"><img
                                    src="/style/images/paybank/pic12.jpg" align="absmiddle" alt="农业银行" title="农业银行" />
                            </dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio4" value="980" onclick="selectBank()"><img
                                    src="/style/images/paybank/pic20.jpg" align="absmiddle" alt="民生银行" title="民生银行" />
                            </dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio5" value="978" onclick="selectBank()"><img
                                    src="/style/images/paybank/vpic23.jpg" align="absmiddle" alt="平安银行" title="平安银行" />
                            </dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio9" value="981" onclick="selectBank()"><img
                                    src="/style/images/paybank/pic19.jpg" align="absmiddle" alt="交通银行" title="交通银行" />
                            </dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio10" value="986" onclick="selectBank()"><img
                                    src="/style/images/paybank/pic18.jpg" align="absmiddle" alt="光大银行" title="光大银行" />
                            </dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio11" value="971" onclick="selectBank()"><img
                                    src="/style/images/paybank/vpic19.jpg" align="absmiddle" alt="邮政储蓄" title="邮政储蓄" />
                            </dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio6" value="972" onclick="selectBank()"><img
                                    src="/style/images/paybank/pic15.jpg" align="absmiddle" alt="兴业银行" title="兴业银行" />
                            </dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio7" value="963" onclick="selectBank()"><img
                                    src="/style/images/paybank/vpic01.jpg" align="absmiddle" alt="中国银行" title="中国银行" />
                            </dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio13" value="989" onclick="selectBank()"><img
                                    src="/style/images/paybank/pic16.jpg" align="absmiddle" alt="北京银行" title="北京银行" />
                            </dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio8" value="992" onclick="selectBank()"><img
                                    src="/style/images/paybank/vpic26.jpg" align="absmiddle" alt="支付宝支付" title="支付宝支付" />
                            </dd>
                            <dd>
                                <input type="radio" name="bank_list" id="Radio12" value="993" onclick="selectBank()"><img
                                    src="/style/images/paybank/pic100.gif" align="absmiddle" alt="财付通" title="财付通" />
                            </dd>
                            <dd>
                            </dd>
                        </dl>
                        <div class="content_bot">
                            <asp:Button ID="btnsubmit" runat="server" Text="下一步" CssClass="btn btn-primary" OnClick="btnsubmit_Click"
                                OnClientClick="paySubmit()" /><span class="txtr" id="callinfo" runat="server" style="color: Red;
                                    font-weight: bold"> </span>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="selBank" style="padding: 40px; display: none;">
        <input type="hidden" id="bankOrderId" name="oid">
        <div id="payResult">
            <div style="margin-bottom: 10px">
                · 请在新打开的页面完成支付，支付成功即完成充值。</div>
            <div style="margin-bottom: 20px">
            </div>
            <input type="button" id="resettijiao" onclick="twoSub()" class="c_btn_bg1" style="font-family: '微软雅黑';">
            <input type="button" id="selBankOrder" onclick="sel()" class="c_btn_bg" style="font-family: '微软雅黑';">
        </div>
    </div>
    </form>
</body>
</html>
