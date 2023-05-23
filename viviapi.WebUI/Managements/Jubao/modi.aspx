<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.Jubao.ItemModi" Codebehind="modi.aspx.cs" %>

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
            var usertype = $("input[name='rbluserType']:checked").val();
            if (usertype == "3") {
                $("#ddlmemvip").show();
                $("#ddlpromvip").hide();
            }
            else if (usertype == "4") {
                $("#ddlpromvip").show();
                $("#ddlmemvip").hide();
            }
            $("input[name='rbluserType']").click(function() {
                var usertype = $(this).val();
                alert(usertype);
                if (usertype == "3") {
                    $("#ddlmemvip").show();
                    $("#ddlpromvip").hide();
                }
                else if (usertype == "4") {
                    $("#ddlpromvip").show();
                    $("#ddlmemvip").hide();
                }
            });
        })
        function backreturn() {
            window.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="3">
        <tr>
            <td colspan="4" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                color: teal; background-repeat: repeat-x; height: 24px">
                举报投诉处理
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="td2">
                序号 ：
            </td>
            <td class="td1">
                <asp:Label ID="lblid" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td2">
                举报者 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtname" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                举报人邮件 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtemail" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                电话 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txttel" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                 信息所在详细网址 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txturl" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                被举报信息类型 ：
            </td>
            <td class="td1">
                <asp:DropDownList ID="ddltype" runat="server">
                    <asp:ListItem Value="0">-请选择-</asp:ListItem>
                    <asp:ListItem Value="1">淫秽色情</asp:ListItem>
                    <asp:ListItem Value="2">诈骗</asp:ListItem>
                    <asp:ListItem Value="3">病毒</asp:ListItem>
                    <asp:ListItem Value="4">其他违法和不良信息</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="td2">
                被举报详细内容 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtremark" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                添加时间 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtaddtime" runat="server" Width="70px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                状态 ：
            </td>
            <td class="td1">
                <asp:DropDownList ID="ddlstatus" runat="server">
                    <asp:ListItem Value="1">等待处理</asp:ListItem>
                    <asp:ListItem Value="2">已处理</asp:ListItem>
                </asp:DropDownList>
            </td>            
        </tr>
        <tr>
            <td class="td2">
                处理时间 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtchecktime" runat="server" Width="70px"  Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                处理人 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtcheck" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                处理意见 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtcheckremark" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                查询密码 ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtpwd" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td2">
                提交IP ：
            </td>
            <td class="td1">
                <asp:TextBox ID="txtfield1" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 20px">
                <div align="center">
                    <asp:Button ID="btnOK" runat="server" Text="确定处理" OnClick="btnOK_Click"></asp:Button>
                    <input type="button" value="关 闭" onclick="backreturn()" />
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
