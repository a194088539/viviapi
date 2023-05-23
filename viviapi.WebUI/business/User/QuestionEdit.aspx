<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.business.QuestionEdit" Codebehind="QuestionEdit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../../style/union.css" type="text/css" rel="stylesheet" />
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
        function backreturn() {
            history.go(-1);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hf_isupdate" runat="server" Value="0" />
    <table width="100%" border="0" cellspacing="1" cellpadding="3">
        <tr>
            <td colspan="4" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                color: teal; background-repeat: repeat-x; height: 24px">
               安全问题编辑
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="td2">
                问题：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtquestion" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                是否可见：
            </td>
            <td class="td1">
                <asp:CheckBox ID="chkrelease" Text="release" runat="server" Checked="False" />
            </td>
        </tr>
        <tr>
            <td class="td2">
                排序 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtsort" runat="server" Width="200px"></asp:TextBox>
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
