<%@ Page Language="C#" AutoEventWireup="true" Inherits="viviapi.WebUI.Business.ChangePwd" Codebehind="ChangePwd.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>后台管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="style/union.css" type="text/css" rel="stylesheet" />
    
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellspacing="1" cellpadding="1" style="width: 100%; height: 100%;border: #c9ddf0 1px solid; background-color: White;" id="table_zyads">
                <tr>
                    <td align="center" colspan="2" style="font-weight: bold; font-size: 14px; background-image: url(../style/images/topbg.gif);
                        color: teal; background-repeat: repeat-x; height: 28px">
                        修改密码</td>
                </tr>
                <tr>
                    <td align="center">
                        <table style="border-right: #c9ddf0 1px solid; border-top: #c9ddf0 1px solid; border-left: #c9ddf0 1px solid;border-bottom: #c9ddf0 1px solid" cellspacing="0" cellpadding="0" width="100%" bgcolor="#f3f9fe" border="0">
                            <tr>
                                <td>
                                    <table id="setpsd" style="margin-bottom: 5px" cellspacing="1" cellpadding="1" width="99%"
                                        align="center">
                                        <tr>
                                            <td align="right" width="19%">
                                                原密码：</td>
                                            <td width="81%" height="30" align="left">
                                                <input id="old_password" runat="server" class="reg"  type="password" name="old_password" style="width: 120px" /></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                新密码：</td>
                                            <td align="left" height="30">
                                                <input id="pas" runat="server" class="reg"  type="password" name="pas" style="width: 120px"/></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                确认新密码：</td>
                                            <td height="30" align="left">
                                                <input id="re_password" runat="server" class="reg"  type="password" name="re_password" style="width: 120px"></td>
                                        </tr>
                                        <tr>
                                            <td align="right" width="19%">
                                                原二级密码：</td>
                                            <td width="81%" height="30" align="left">
                                                <input id="oldsedpwd" runat="server" class="reg"  type="password" name="old_password" style="width: 120px" /></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                新二级密码：</td>
                                            <td align="left" height="30">
                                                <input id="newsedpwd" runat="server" class="reg"  type="password" name="pas" style="width: 120px"/></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                确认新二级密码：</td>
                                            <td height="30" align="left">
                                                <input id="newsedpwd2" runat="server" class="reg"  type="password" name="re_password" style="width: 120px"></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                            </td>
                                            <td height="30" align="left">
                                                <asp:Button ID="btnUpdate" runat="server" Text="确认修改" OnClick="btnUpdate_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                            </td>
                                            <td height="30" align="left">
                                                <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 40px">
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

<script type="text/javascript" language="javascript">
var table=document.getElementById("setpsd");
if (table){
for(i=0;i<table.rows.length;i++){
if(i%2==0){
table.rows[i].bgColor="ffffff";
}else{table.rows[i].bgColor="f3f9fe"}
}
}
</script>

