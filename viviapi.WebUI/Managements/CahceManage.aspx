<%@ Page Language="C#" AutoEventWireup="True" Inherits="viviapi.WebUI.Managements.CahceManage" Codebehind="CahceManage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>��̨����</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="style/union.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellspacing="1" cellpadding="0" style="width: 100%; height: 100%;border: #c9ddf0 1px solid; background-color: White;" id="table_zyads">
                <tr>
                    <td align="center" colspan="2" style="font-weight: bold; font-size: 14px; background-image: url(style/images/topbg.gif);color: teal; background-repeat: repeat-x; height: 28px">
                        �������
                </tr>
                <tr>
                    <td align="center">
                        <table style="border-right: #c9ddf0 1px solid; border-top: #c9ddf0 1px solid; border-left: #c9ddf0 1px solid;
                            border-bottom: #c9ddf0 1px solid" cellspacing="0" cellpadding="0" width="100%"
                            bgcolor="#f3f9fe" border="0">
                            <tr>
                                <td>
                                    <table id="setpsd" style="margin-bottom: 5px" cellspacing="1" cellpadding="3" width="100%">
                                        <tr>
                                            <td height="30" style="font-size: 14px;">
                                                ���ػ���������<asp:Label ID="CacheCountLabel" runat="server" Text="û�л���"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td height="30">
                                                <asp:Button ID="btnDelAll" runat="server" Text="���ȫ������" 
                                                    OnClick="btnDelAll_Click" /></td>
                                        </tr>
                                        <tr>
                                            <td height="30">
                                                <asp:Label ID="Label1" runat="server" ForeColor="Red">ϵͳ���л������������Ч�ʡ����������ջ��棡</asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td height="30">
                                            �����������                                             
                                                <asp:CheckBoxList ID="cbl_cacheTypeList" runat="server" Width="100%">
                                                    <asp:ListItem Value="CHANNELS">֧��ͨ��</asp:ListItem>
                                                    <asp:ListItem Value="CHANNEL_TYPE">֧��ͨ�����</asp:ListItem>
                                                    <asp:ListItem Value="CHANNEL_TYPE_USER_">�û�֧��ͨ������</asp:ListItem>
                                                    <asp:ListItem Value="NEWS">����</asp:ListItem>
                                                    <asp:ListItem Value="SUPPLIER_">�ӿ���</asp:ListItem>
                                                    <asp:ListItem Value="SUPPPAYRATE">��Ӧ�̷���</asp:ListItem>
                                                    <asp:ListItem Value="USER_">�û�����</asp:ListItem>
                                                    <asp:ListItem Value="USERHOST_">�û���·��վ�б�</asp:ListItem>
                                                    <asp:ListItem Value="Question">��ȫ�����б�</asp:ListItem>
                                                    <asp:ListItem Value="WEBINFO_">��վ����</asp:ListItem>
                                                    <asp:ListItem Value="SYSCONFIG">������Ϣ</asp:ListItem>
                                                </asp:CheckBoxList>
                                                <asp:Button ID="btnBigClass" runat="server" Text="�����ѡ��𻺴�" 
                                                    onclick="btnBigClass_Click" />
                                            </td>
                                        </tr>
                                        <tr><td>MemCached����:
                                            <asp:GridView ID="gv_cache" runat="server" AutoGenerateColumns="False" Width="100%" DataKeyNames="cacheKey">
                                                <Columns>
                                                     <asp:TemplateField>
                                                     <ItemTemplate>
                                                            <asp:CheckBox ID="item" runat="server" />
                                                     </ItemTemplate>
                                                     </asp:TemplateField>
                                                      <asp:BoundField HeaderText="�������" DataField="cacheType" />
                                                      <asp:BoundField HeaderText="�������" DataField="cacheTypeName" />
                                                    <asp:BoundField HeaderText="����Key" DataField="cacheKey" />                                                                                                   
                                                </Columns>
                                            </asp:GridView>
                                            <asp:Button ID="btnClear" runat="server" Text="��С�����" 
                                                onclick="btnClear_Click"  />
                                            </td>
                                        </tr>
                                    </table>
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

