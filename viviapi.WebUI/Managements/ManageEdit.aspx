<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.Managements.ManageEdit" Codebehind="ManageEdit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../style/union.css" type="text/css" rel="stylesheet" />

    <script src="../../js/common.js" type="text/javascript"></script>

    <style type="text/css">
        table
        {
            font-weight: normal;
            line-height: 170%;
            font-family: Arial;
        }
        A:link
        {
            color: #237C04;
            text-decoration: none;
        }
        td
        {
            height: 20px;
            line-height: 20px;
            font-size: 12px;
            padding: 0px;
        }
        .td_title, th
        {
            height: 20px;
            line-height: 22px;
            font-weight: bold;
            border: 0px solid #fff;
            text-align: left;
        }
        .td1
        {
            padding-right: 3px;
            padding-left: 3px;
            color: #999999;
            padding-bottom: 0px;
            padding-top: 5px;
            height: 25px;
            width: 35%;
        }
        .td2
        {
            padding-right: 3px;
            padding-left: 8px;
            padding-top: 5px;
            color: #083772;
            background: #EFF3FB;
            font-size: 12px;
            text-align: right;
            width: 15%;
        }
        .td3
        {
            padding: 1px 1px 0 0px;
            color: #083772;
            background: #EFF3FB;
            font-size: 12px;
            text-align: center;
        }
        .moban
        {
            padding-top: 0px;
            border: 0px;
        }
        input
        {
            border: 1px solid #999;
            padding: 3px;
            margin-left: 10px;
            font: 12px tahoma;
            ling-height: 16px;
        }
        .lable
        {
            border: 1px solid #999;
            padding: 3px;
            margin-left: 10px;
            font: 12px tahoma;
            ling-height: 16px;
        }
        select
        {
            border: 1px solid #999;
            padding: 3px;
            margin-left: 10px;
            font: 12px tahoma;
            ling-height: 16px;
        }
        .input4
        {
            border: 1px solid #999;
            padding: 3px;
            margin-left: 10px;
            font: 11px tahoma;
            ling-height: 16px;
            height: 45px;
        }
        .button
        {
            color: #135294;
            border: 1px solid #666;
            height: 21px;
            line-height: 21px;
        }
        .nrml
        {
            background-color: #eeeeee;
            font-weight: bold;
        }
        .radio
        {
            border: none;
        }
        .checkbox
        {
            border: none;
        }
        .addnew
        {
            font-size: 12px;
            color: #FF0000;
        }
        a.servername
        {
            height: 470px;
            width: 527px;
            color: #E54202;
            cursor: hand;
        }
        .current
        {
            border: #ff6600 1px solid;
        }
        a:hover
        {
            height: 470px;
            width: 527px;
            color: #E54202;
            cursor: hand;
        }
        #nav LI A.noncurrent
        {
            /*border:#DC171E 3px solid;*/
        }
        #nav UL
        {
            padding-bottom: 0px;
            padding-left: 5px;
            padding-right: 5px;
            padding-top: 0px;
        }
        #nav LI
        {
            display: inline;
            padding-left: 10px;
        }
        #nav LI a:hover
        {
            border: #B6E000 1px solid;
        }
        #nav li A:visited
        {
            border: #ff0000 1px solid;
        }
        img
        {
            border: #CCCCCC 1px solid;
            padding: 0 5px;
        }
        #tplPreview
        {
            position: absolute;
            top: 0px;
            left: 0px;
            background: #ffffff;
            border: 1px solid #333;
            font-size: 12px;
            color: #4B4B4B;
            padding: 12px 15px 15px 15px;
        }
    </style>

    <script type="text/javascript">
        $().ready(function() {
            var isUpdate = $("input[name='hf_isupdate']").val();
            if (isUpdate == "0") {
                $("#tr_lastloginip").hide();
                $("#tr_lastlogintime").hide();
                $("#tr_balance").hide();
            }
            else if (isUpdate == "1") {                
                $("#tr_lastloginip").show();
                $("#tr_lastlogintime").show();
                $("#tr_balance").show();
            }
        })     
function backreturn(){
    history.go(-1);
}
    </script>

</head>
<body>    
    <form id="form1" runat="server">
    <asp:HiddenField ID="hf_isupdate" runat="server" Value="0" />
    <table width="100%" border="0" cellspacing="1" cellpadding="3">
        <tr>
            <td colspan="4" style="font-weight: bold; font-size: 14px; background-image: url(style/images/topbg.gif);
                color: teal; background-repeat: repeat-x; height: 24px">
                后台操作员信息编辑 
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="td2">
                用户名：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtusername" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                密码：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtpassword" runat="server" Width="200px" TextMode="Password"></asp:TextBox>（不修改请留空）
            </td>
        </tr>
        <tr>
            <td class="td2">
                二级密码 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtsecondpwd" runat="server" Width="200px" TextMode="Password"></asp:TextBox>（不修改请留空）
            </td>
        </tr>
        <tr>
            <td class="td2">
                属性：
            </td>
            <td class="td1">
                <asp:CheckBox ID="ckb_SuperAdmin" runat="server" Text="超级管理员" />
                <asp:CheckBox ID="ckb_Agent" runat="server" Text="代理" Visible="false" />
            </td>
        </tr>        
        <tr>
            <td class="td2">
                权限 ：
            </td>
            <td class="td1" style="width:80%">
                <asp:CheckBoxList ID="cbl_roles" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="td2">
                状态 ：
            </td>
            <td class="td1">
                <asp:DropDownList ID="ddlStus" runat="server">
                    <asp:ListItem Value="1">正常</asp:ListItem>
                    <asp:ListItem Value="0">锁定</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="td2">
                姓名 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtrelname" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr_lastloginip" style="display: none">
            <td class="td2">
                最近登录IP ：
            </td>
            <td class="td1">
                <asp:Label ID="lbllastloginip" runat="server" CssClass="lable" Width="160px" ></asp:Label>
            </td>
        </tr>
        <tr id="tr_lastlogintime" style="display: none">
            <td class="td2">
                最近登录时间 ：
            </td>
            <td class="td1">
                <asp:Label ID="lbllastlogintime" runat="server" CssClass="lable" Width="160px" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                提成类型 ：
            </td>
            <td class="td1">
                <asp:DropDownList ID="ddlCommissionType" runat="server">
                    <asp:ListItem Value="1">按条固定提成</asp:ListItem>
                    <asp:ListItem Selected="True" Value="2">按支付金额%</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="td2">
                网银提成 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtCommission" runat="server">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                卡类提成 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtCardCommission" runat="server">0</asp:TextBox>
            </td>
        </tr>
        <tr id="tr_balance" style="display: none">
            <td class="td2">
                账号余额 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="lblbalance" runat="server" Enabled="false">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 20px">
                <div align="center">
                    <asp:Button ID="btnAdd" runat="server" Text="提 交" OnClick="btnAdd_Click"></asp:Button>
                    <input type="button" value="返 回" onclick="backreturn()" />
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
