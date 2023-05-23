<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="transfer.aspx.cs" Inherits="viviapi.WebUI.Userlogin.settlement.transfer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Userlogin/static/style/master.css" rel="stylesheet" type="text/css" />
 <link rel="stylesheet" type="text/css" href="../css/mytablelist.css" />
    <script src="/Userlogin/static/js/lib/jquery-1.4.2.js" type="text/javascript"></script>

    <script type="text/javascript">
        jQuery(document).ready(function() {
            $("#txtToUser").focus(function() {
                $(this).val("");
                $("#touserid").val(0);
            });
            $("#txtToUser").blur(function() {
                if ($(this).val() == "")
                    return;
                $.getJSON("/Userlogin/ajax/GetUserInfo_new.ashx?t=" + Math.random(), { username: $(this).val() },
            function(data) {
                if (data.result == 0) {
                    alert(data.msg);
                }
                else {
                    $("#touserid").val(data.result);
                    $("#txtToUser").val(data.username + "(" + data.name + ")");
                }
            })
            });

            $("#ibtnSave").click(function() {
                var touser = $("#touserid").val();
                if (touser == '' || touser == "0") {
                    $("#callinfo").html(errico + "请输入对方账号");
                    return false;
                };
                var money = $("#txtTransferMoney").val();
                if (money == "") {
                    $("#callinfo").html(errico + "请转账金额");
                    return false;
                };
            });
        });

        function fixNumber(o, n) {
            if (o.value == "" || isNaN(o.value) || o.value == Infinity) {
                o.value = parseFloat("0").toFixed(n);
            } else {
                o.value = parseFloat(o.value).toFixed(n);
            }
        }; 



    </script>

    <script language="javascript" type="text/javascript">
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
            $("#txtTransferMoney").keypress(function(event) {
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
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="breadCrumb">
        <a href="" onclick="parent.location.href='/Userlogin/account/index.aspx'" style="cursor: pointer;">
            <i class="icon icon-home icon-orange"></i>首页 </a>&nbsp;&gt;&nbsp; <a href="" style="cursor: pointer;"
                onclick="parent.location.href='/Userlogin/settlement/index.aspx'">结算提现</a>
        &nbsp;&gt;&nbsp; <span>余额转账</span>
    </div>
    <input type="hidden" name="balance" id="balance" value="<%=balance.ToString("f2")%>" />
    <input type="hidden" name="cashfee" id="cashfee" value="<%=unpayment.ToString("f2")%>" />
    <!--右部表单开始-->
    <div id="list_content" style="padding-top: 0px;">
        <h2>
            余额转账</h2>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="2" colspan="3" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    可用金额:
                </td>
                <td align="left" class="line_01">
                    <span class="zi23">
                        <%=enableAmount.ToString("f2")%></span> 元
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    对方账号:
                </td>
                <td align="left" class="line_01">
                    <asp:TextBox ID="txtToUser" runat="server" Class="txt_01"></asp:TextBox>
                    <asp:HiddenField ID="touserid" runat="server" />
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    付款金额:
                </td>
                <td align="left" class="line_01">
                    <asp:TextBox ID="txtTransferMoney" runat="server" MaxLength="15" Class="txt_01"></asp:TextBox>
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    转账说明:
                </td>
                <td align="left" class="line_01">
                    <asp:TextBox ID="txtremark" runat="server" Class="txt_01"></asp:TextBox>
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    提现密码:
                </td>
                <td align="left" class="line_01">
                    <asp:TextBox ID="txttocashpwd" runat="server" Class="txt_01" TextMode="Password"></asp:TextBox>
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    手续费:
                </td>
                <td align="left" class="line_01">
                    <em class="font14"><b id="chargeshow" class="txtc">0</b> 元 </em>
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="10" colspan="2">
                </td>
            </tr>
            <tr>
                <td height="22" align="left" class="font8">
                    <asp:Button ID="btnSave" runat="server" Text="确认提交" CssClass="btn btn-primary" OnClick="btnSave_Click" />&nbsp;
                    &nbsp;<span class="txtr" id="callinfo" runat="server" style="color: Red; font-weight: bold"></span>
                    <td align="right">
                        &nbsp;
                    </td>
            </tr>
            <tr>
                <td height="10" colspan="2">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
