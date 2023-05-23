<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true"
    CodeBehind="costinfo.aspx.cs" Inherits="viviapi.WebUI.Merchant.costinfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--右部表单开始-->
    <div id="list_content">
        <div id="title">
            结算信息&nbsp;<img id="loading" width="0" height="0" src="/merchant/static/style/008.gif" /></div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="42" colspan="3" align="left" class="line_01">
                    通过实名认证后 可以申请变更。
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    结算模式:
                </td>
                <td align="left" class="line_01">
                    <asp:Literal ID="litpmode" runat="server"></asp:Literal>
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    开户姓名:
                </td>
                <td align="left" class="line_01">
                   <asp:Literal ID="litPayeeName" runat="server"></asp:Literal>
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
             <tr>
                <td height="39" align="left" class="line_01">
                    结算银行:
                </td>
                <td align="left" class="line_01">
                   <asp:Literal ID="litPayeeBank" runat="server"></asp:Literal>
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
             <tr>
                <td height="39" align="left" class="line_01">
                    银行卡号:
                </td>
                <td align="left" class="line_01">
                   <asp:Literal ID="litUserViewBankAccout" runat="server"></asp:Literal>
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
             <tr>
                <td height="39" align="left" class="line_01">
                    开户支行:
                </td>
                <td align="left" class="line_01">
                    <asp:Literal ID="litProvince" runat="server"></asp:Literal><asp:Literal ID="litBankAddress" runat="server"></asp:Literal>
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
        </table>
        
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="42" colspan="3" align="left" class="line_01">
                   申请变更结算方式
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    开户姓名:
                </td>
                <td align="left" class="line_01">
                    <asp:Literal ID="lit_username" runat="server"></asp:Literal>
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    收款方式:
                </td>
                <td align="left" class="line_01">
                    <asp:RadioButton ID="rb_bank" runat="server" GroupName="pmode" Text="银行账户" 
                        Checked="true" AutoPostBack="True" oncheckedchanged="rb_bank_CheckedChanged"/>
                    <asp:RadioButton ID="rb_alipay" AutoPostBack="True" runat="server" GroupName="pmode" Text="支付宝" 
                        oncheckedchanged="rb_alipay_CheckedChanged"/>
                    <asp:RadioButton ID="rb_tenpay" AutoPostBack="True" runat="server" GroupName="pmode" Text="财付通" 
                        oncheckedchanged="rb_tenpay_CheckedChanged"/>
                </td>
                <td height="39" align="left" class="line_01">
                    
                </td>
            </tr>
            <tr id="tr_accoutType" runat="server">
                <td height="39" align="left" class="line_01">
                    账号类型:
                </td>
                <td align="left" class="line_01">
                     <asp:RadioButton ID="rb_accoutType0" runat="server" GroupName="accoutType" Text="私人账户" Checked="true" />
                     <asp:RadioButton ID="rb_accoutType1"  runat="server" GroupName="accoutType" Text="公司账户" />
                </td>
                <td height="39" align="left" class="line_01">
                    
                </td>
            </tr>
            <tr id="tr_bankselect" runat="server">
                <td height="39" align="left" class="line_01">
                    开户银行:
                </td>
                <td align="left" class="line_01">
                      <select  id="ddlbankName" runat="server">
                        <option value="1002">中国工商银行</option>
                        <option value="1005">中国农业银行</option>
                        <option value="1003">中国建设银行</option>
                        <option value="1026">中国银行</option>
                        <option value="1001">招商银行</option>
                        <option value="1006">民生银行</option>
                        <option value="1020">交通银行</option>
                        <option value="1066">中国邮政储蓄银行</option>
                     </select>
                </td>
                <td height="39" align="left" class="line_01">
                    
                </td>
            </tr>
            <tr id="tr_province" runat="server">
                <td height="39" align="left" class="line_01">
                    开户省市:
                </td>
                <td align="left" class="line_01">
                     <asp:DropDownList ID="ddlprovince" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlprovince_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlcity" runat="server">
                            <asp:ListItem Value="">--市区--</asp:ListItem>
                        </asp:DropDownList>
                </td>
                <td height="39" align="left" class="line_01">
                    
                </td>
            </tr>
            <tr id="tr_address" runat="server">
                <td height="39" align="left" class="line_01">
                    支行名称:
                </td>
                <td align="left" class="line_01">
                   <input id="txtbankAddress" runat="server" class="txt_02" maxlength="50" title="支行名称格式：某某省分行某某市支行某某分理处请认真填写，以免造成提款延误" size="80"/>
                                                                                         
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr id="tr_oldcard" runat="server" visible="false">
                <td height="39" align="left" class="line_01">
                    原银行卡号:
                </td>
                <td align="left" class="line_01">
                    <input  id="txtoldaccount" runat="server" class="txt_02" maxlength="200" title="更新结算账户前需要先确认原银行帐号" size="80" />
                                                             
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    新银行卡号:
                </td>
                <td align="left" class="line_01">
                    <input id="txtaccount"  runat="server" class="txt_02" maxlength="200" title="更新结算账户前需要先确认原银行帐号" size="80"/>
                </td>
                <td height="39" align="left" class="line_01">
                </td>
            </tr>
            <tr>
                <td height="39" align="left" class="line_01">
                    确认新卡号:
                </td>
                <td align="left" class="line_01">
                    <input id="txtreaccount"  runat="server" class="txt_02" maxlength="200" title="更新结算账户前需要先确认原银行帐号" size="80"/>
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
                    <asp:HiddenField ID="hfaction" runat="server" Value="0" />&nbsp;
                    <asp:Button ID="btnSave" runat="server" Text="提交申请" CssClass="btn btn-primary"
                        OnClick="btnSave_Click" />
                     &nbsp;  <span class="txtr" id="callinfo" runat="server" style="color:Red; font-weight:bold"></span>
                </td>
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
</asp:Content>
