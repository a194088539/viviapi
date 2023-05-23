<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.User.BankForUser" Codebehind="BankForUser.aspx.cs" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="aspxc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../style/union.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .rptheadlink
        {
            color: White;
            font-family: 宋体;
            font-size: 12px;
        }
         ;</style>

    <script src="../../js/common.js" type="text/javascript"></script>
    <script src="../../js/ControlDate/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        $().ready(function() {
            $("#chkAll").click(function() {
                $("input[type='checkbox']").each(function() {
                    if ($("#chkAll").attr('checked') == true) {
                        $(this).attr("checked", true);
                    }
                    else
                        $(this).attr("checked", false);
                });
            });
        })
        function isselect() {
            var result = false;
            $("input[type='checkbox']").each(function() {            
                if ($(this).attr("id") != "chkAll") {
                    if ($(this).attr('checked') == true) {
                        result = true;
                    }
                }
            });
            return result;
        }
        function check() {
            if (!isselect()) {
                alert('至少选择一条记录');
                return false;
            }
            return true;
        }
        function checkAll() {
            if ($("#txtPassWord").val() =="") {
                alert('请输入二级密码');
                return false;
            }
            return true;
        }
        function sendMsg(uid) {
            window.showModelessDialog("SendMsg.aspx?uid=" + uid, window, "dialogWidth=800px;dialogHeight=500px;");
        }
    
    </script>

    <script type="text/javascript">
        function sendInfo(id) {
            window.open("../User/UserEdit.aspx?id=" + id, "查看用户信息", "'height=700, width=1000, top=0, left=0, toolbar=no, menubar=no, scrollbars=yes, resizable=no,location=n o, status=no");
        }
    </script>

</head>
<body class="yui-skin-sam">
    <form id="form1" runat="server">
    <div id="modelPanel" style="background-color: #F2F2F2">
    </div>
    <input id="selectedUsers" runat="server" type="hidden" />
    <table width="100%" border="0" cellspacing="1" cellpadding="1" class="table1">
        <tr>
            <td align="center" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                color: teal; background-repeat: repeat-x; height: 24px">
                商户结算 
            </td>
        </tr>
        <tr>
            <td>
                用户ID
                <asp:TextBox ID="txtuserId" runat="server"></asp:TextBox>
                余额：<asp:TextBox ID="txtbalance" runat="server" Text="100"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text=" 查 询 " OnClick="btnSearch_Click">
                </asp:Button>                
                <asp:Button ID="btnBatchSettle" runat="server" CssClass="button" Text="批量结算" 
                    onclick="btnBatchSettle_Click" OnClientClick="return check();">
                </asp:Button>
                二级密码：<asp:TextBox ID="txtPassWord" runat="server" TextMode="Password"></asp:TextBox>         
                <asp:Button ID="btnAllSettle" runat="server" CssClass="button" Text="全部结算" 
                    onclick="btnAllSettle_Click" OnClientClick="return checkAll();">
                </asp:Button>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table width="100%" id="tab" border="0" align="center" cellpadding="2" cellspacing="1">
                    <asp:Repeater ID="rptUsers" runat="server" 
                        OnItemDataBound="rptUsersItemDataBound" onitemcommand="rptUsers_ItemCommand">
                        <HeaderTemplate>
                            <tr style="background-color: #507CD1; color: #fff; height: 22;">
                                <td>
                                    <input id="chkAll" type="checkbox">
                                </td>
                                <td>
                                    商户ID
                                </td>  
                                <td>
                                    登录用户
                                </td>                                
                                <td>
                                    余额
                                </td>
                                <td>
                                    提现中
                                </td>
                                <td>
                                    冻结
                                </td>
                                <td>
                                    收款人
                                </td>
                                <td>
                                    开户行
                                </td>
                                <td>
                                    银行卡号
                                </td>
                                <td>
                                    开户地址
                                </td>
                                <td>
                                    结算模式
                                </td>
                                <td>
                                    扣押金额
                                </td>
                                <td>
                                    结算金额
                                </td>
                                <td>
                                    操作
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr style="background-color: #EFF3FB">
                                <asp:HiddenField ID="hfuserid" runat="server" Value='<%# Eval("id")%>' />
                                <td>
                                    <input id="chkItem" type="checkbox" runat="server" value='<%#Eval("id")%>' name="chkItem" />                                    
                                </td>
                                <td>
                                    <%# Eval("id")%>
                                    
                                </td> 
                                <td>
                                    <%# Eval("username")%>
                                </td>                              
                                <td>
                                    <%# Eval("balance", "{0:f2}")%>
                                </td>
                                <td>
                                    <%# Eval("unpayment", "{0:f2}")%>
                                </td>
                                <td>
                                    <%# Eval("Freeze", "{0:f2}")%>
                                </td>
                                <td>
                                    <%# Eval("payeeName")%>
                                </td>
                                 <td>
                                    <%#viviapi.BLL.SettledFactory.GetSettleBankName(Eval("payeeBank").ToString())%>
                                </td>
                                <td>
                                    <%# Eval("account")%>
                                </td>
                                <td>
                                    <%# Eval("bankAddress")%>
                                </td>
                                <td>
                                    T+<%# Eval("settles")%>
                                </td>
                                <td>
                                    <asp:Literal ID="litTodayIncome" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtpayAmt" runat="server" Width="80px"></asp:TextBox>
                                </td>                               
                                <td>
                                    <asp:Button ID="btnSettled" runat="server" CommandName="Settled" CommandArgument='<%#Eval("id")%>' Text="结算" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="background-color: #ffffff">
                                <asp:HiddenField ID="hfuserid" runat="server" Value='<%# Eval("id")%>' />
                                <td>
                                    <input id="chkItem" type="checkbox" runat="server" value='<%#Eval("id")%>' name="chkItem" />
                                </td>
                                <td>
                                    <%# Eval("id")%>                                   
                                </td> 
                                <td>
                                    <%# Eval("username")%>
                                </td>                              
                                <td>
                                    <%# Eval("balance", "{0:f2}")%>
                                </td>
                                <td>
                                    <%# Eval("unpayment", "{0:f2}")%>
                                </td>
                                <td>
                                    <%# Eval("Freeze", "{0:f2}")%>
                                </td>
                                <td>
                                    <%# Eval("payeeName")%>
                                </td>
                                 <td>
                                    <%#viviapi.BLL.SettledFactory.GetSettleBankName(Eval("payeeBank").ToString())%>
                                </td>
                                <td>
                                    <%# Eval("account")%>
                                </td>
                                 <td>
                                    <%# Eval("bankAddress")%>
                                </td>
                                <td>
                                    T+<%# Eval("settles")%>
                                </td>
                                <td>
                                    <asp:Literal ID="litTodayIncome" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtpayAmt" runat="server" Width="80px"></asp:TextBox>
                                </td>                               
                                <td>
                                    <asp:Button ID="btnSettled" runat="server" CommandName="Settled" CommandArgument='<%#Eval("id")%>' Text="结算" />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr style="background-color: #EFEFEF">
                        <td style="height: 16px;">
                            <aspxc:AspNetPager ID="Pager1" runat="server" 
                                onpagechanged="Pager1_PageChanged"
                                
                                AlwaysShow="True" CustomInfoHTML="总记录数：%RecordCount%&nbsp;总页数：%PageCount%&nbsp;当前页：%CurrentPageIndex%&nbsp;"
                                CustomInfoTextAlign="Left" FirstPageText="首页" HorizontalAlign="Right" LastPageText="末页"
                                NavigationToolTipTextFormatString="跳转{0}页" NextPageText="下一页" PageIndexBoxType="DropDownList"
                                PageSize="20" PrevPageText="上一页" ShowBoxThreshold="50" ShowCustomInfoSection="Left"
                                ShowPageIndex="False" ShowPageIndexBox="Always" SubmitButtonText="GO&gt;&gt;"
                                TextAfterPageIndexBox="页" TextBeforePageIndexBox="跳到" Width="100%" 
                                Height="30px" 
                                >
                            </aspxc:AspNetPager>
                        </td>
                    </tr>
                    <tr>
                    <td>
                        说明：这是按照平台规定的正常流程给商户打款 <br />查询条件中的余额是【商户实际余额】减去【提现中还未审核】和【被冻结的金额】
                    </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>

    <script type="text/javascript">
        function handler(tp) {
        }

        var mytr = document.getElementById("tab").getElementsByTagName("tr");
        for (var i = 1; i < mytr.length; i++) {
            mytr[i].onmouseover = function() {
                var rows = this.childNodes.length;
                for (var row = 0; row < rows; row++) {
                    this.childNodes[row].style.backgroundColor = '#E6EEFF';
                }
            };
            mytr[i].onmouseout = function() {
                var rows = this.childNodes.length;
                for (var row = 0; row < rows; row++) {
                    this.childNodes[row].style.backgroundColor = '';
                }
            };
        }

    </script>

</body>
</html>
