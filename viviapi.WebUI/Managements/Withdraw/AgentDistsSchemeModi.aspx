<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="viviapi.WebUI.Managements.Withdraw.AgentDistsSchemeModi" Codebehind="AgentDistsSchemeModi.aspx.cs" %>

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
            width: 70%;
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
            width: 30%;
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
            height: 26px;
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
        function backreturn() {
            history.go(-1);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="3">
        <tr>
            <td colspan="4" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                color: teal; background-repeat: repeat-x; height: 24px">
                代发规则编辑
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="td2">
                方案名称 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtschemename" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                网银T+几 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtbankdetentiondays" runat="server" Width="200px">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                点卡T+几 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtcarddetentiondays" runat="server" Width="200px">0</asp:TextBox>
            </td>
        </tr>
        <tr >
            <td class="td2">
                其它T+几 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtotherdetentiondays" runat="server" Width="200px">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                最低提现金额限制(每笔) ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtminamtlimitofeach" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                最大提现金额限制(每笔) ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtmaxamtlimitofeach" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                每天最多可提现次数 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtdailymaxtimes" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                每天最多提现 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtdailymaxamt" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                提现手续费 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtchargerate" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                提现手续费最少每笔 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtchargeleastofeach" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                提现手续费最高每笔 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtchargemostofeach" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr >
            <td class="td2">
                是否走接口 ：
            </td>
            <td class="td1">
                 <asp:RadioButtonList ID="rblVaiInterface" runat="server" 
                    RepeatDirection="Horizontal">
                <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                <asp:ListItem Value="0" >否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="td2">
                是否要审核：
            </td>
            <td class="td1">
                <asp:RadioButtonList ID="rbltranRequiredAudit" runat="server" 
                    RepeatDirection="Horizontal">
                <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                <asp:ListItem Value="0" >否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="td2">
                是否默认 ：
            </td>
            <td class="td1">
                <asp:RadioButtonList ID="rblisdefault" runat="server" 
                    RepeatDirection="Horizontal">
                <asp:ListItem Value="1">是</asp:ListItem>
                <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="td2">
                <asp:Button ID="btnAdd" runat="server" Text="提 交" OnClick="btnAdd_Click"></asp:Button>
            </td>
            <td class="td1">
                <input type="button" value="返 回" onclick="backreturn()" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
