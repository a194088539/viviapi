<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" Inherits="viviapi.WebUI.Managements.NewsEdit"
    Title="" CodeBehind="NewsEdit1.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <link href="../style/admin.css" type="text/css" rel="stylesheet" />

    <script src="../../js/common.js" type="text/javascript"></script>

    <script src="../../Js/ControlDate/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript" charset="utf-8" src="/controls/ueditor/ueditor.config.js"></script>

    <!--使用版-->
    <!--<script type="text/javascript" charset="utf-8" src="../ueditor.all.js"></script>-->
    <!--开发版-->

    <script type="text/javascript" charset="utf-8" src="/controls/ueditor/editor_api.js"> </script>

    <!--建议手动加在语言，避免在ie下有时因为加载语言失败导致编辑器加载失败-->
    <!--这里加载的语言文件会覆盖你在配置项目里添加的语言类型，比如你在配置项目里配置的是英文，这里加载的中文，那最后就是中文-->

    <script type="text/javascript" charset="utf-8" src="/controls/ueditor/lang/zh-cn/zh-cn.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="1" cellpadding="3">
        <tr>
            <td colspan="4" style="font-weight: bold; font-size: 14px; background: url(../style/images/topbg.gif) repeat-x;
                color: teal; height: 28px">
                新闻信息编辑
            </td>
        </tr>
        <tr>
            <td>
                <table style="border: 1px;" cellpadding="0" cellspacing="0" class="table01">
                    <tr>
                        <td align="center" style="width: 118px; height: 40px;">
                            标题：
                        </td>
                        <td align="left" style="width: 550px; height: 40px;">
                            <asp:TextBox ID="txtTitle" runat="server" Width="220px"></asp:TextBox>
                            <asp:CheckBox ID="cb_red" runat="server" Text="加红" CssClass="label" Visible="false">
                            </asp:CheckBox>
                            <asp:CheckBox ID="cb_bold" runat="server" Text="加粗" CssClass="label"></asp:CheckBox>
                            <asp:CheckBox ID="cb_top" runat="server" Text="置顶" CssClass="label"></asp:CheckBox>
                            <asp:CheckBox ID="cb_pop" runat="server" Text="弹出" CssClass="label"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 118px; height: 40px;">
                            标题颜色：
                        </td>
                        <td align="left" style="width: 550px; height: 40px;">
                            <asp:RadioButtonList ID="rbColor" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="FF0000">&lt;span style=&quot; color:#FF0000&quot;&gt;#FF0000&lt;/span&gt;</asp:ListItem>
                                <asp:ListItem Value="990000">&lt;span style=&quot; color:#990000&quot;&gt;#990000&lt;/span&gt;</asp:ListItem>
                                <asp:ListItem Value="FF0066">&lt;span style=&quot; color:#FF0066&quot;&gt;#FF0066&lt;/span&gt;</asp:ListItem>
                                <asp:ListItem Value="FF3300">&lt;span style=&quot; color:#FF3300&quot;&gt;#FF3300&lt;/span&gt;</asp:ListItem>
                            </asp:RadioButtonList>
                            <br />
                            颜色代码：<asp:TextBox ID="txtColorCode" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 118px; height: 40px;">
                            类型：
                        </td>
                        <td align="left" style="width: 550px; height: 40px;">
                            <asp:DropDownList ID="ddl_type" runat="server" Width="93px">
                            </asp:DropDownList>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            内容：
                        </td>
                        <td align="left" >

                            <script id="editor" type="text/plain" style="width: 550px; height: 227px">               
                            </script>

                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 118px; height: 40px;">
                            添加日期：
                        </td>
                        <td align="left" style="width: 550px; height: 40px;">
                            <input id="txtDate" runat="server" readonly="readonly" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 118px; height: 40px;">
                            是否发布：
                        </td>
                        <td align="left" style="width: 550px; height: 40px;">
                            <asp:RadioButtonList ID="rbl_Release" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                                <asp:ListItem Value="0">否</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 118px; height: 25px;">
                        </td>
                        <td align="left" style="width: 550px; height: 25px;">
                            <input type="button" value="返  回" onclick="javascript:location.href='NewsList.aspx'" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="content" runat="server" />
    </form>
</body>
</html>
