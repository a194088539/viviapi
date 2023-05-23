<%@ Page Title="" Language="C#" MasterPageFile="~/Merchant/Base.Master" AutoEventWireup="true" CodeBehind="import.aspx.cs" Inherits="viviapi.WebUI.Merchant.import" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="list_content">
        <div id="title">
            文件上传&nbsp;<img id="loading" width="0" height="0" src="/merchant/static/style/008.gif" /></div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td align="right" class="line_01">
                    代发需知：
                </td>
                <td class="line_01">
                    1、请参照代发模板文件，按格式填写本批次的代发明细信息。 <a href="download/templetfile.xlsx">下载</a>代发模板文件 
<br />2、目前支持的收款银行有16家，分别为：工商银行、农业银行、建设银行、交通银行、招商银行、中国银行、邮政储蓄银行、民生银行、华夏银
     行、兴业银行、广发银行、浦发银行、光大银行、中信银行、平安银行、杭州银行。
<br />3、关于单笔代发限额：工、农、建、交四家收款银行最高20万元，其他银行最高5万元。
<br />4、按照反洗钱规定，代发金额超过1万元的，请自行保存代发目标用户的真实有效身份信息，要做到随时可查。代发金额超过5万元的，请向贝付
     传真相关收款人的身份证复印件，并写明所属代发批次号，传真号码：0571-86584668。工作人员据此审核这些代发明细。
<br />5、账户余额应>=代发金额合计+代发手续费合计。 查看当前费率设置

                </td>
            </tr>
            <tr>
                <td z align="right" class="line_01">
                </td>
                <td align="left" class="line_01">
                    <asp:CheckBox ID="cbx_sure" runat="server" Text="以上内容我已知悉" AutoPostBack="true" OnCheckedChanged="cbx_sure_CheckedChanged" />
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width: 25%">
                </td>
                <td style="width: 75%" align="left" class="line_01">
                    <asp:FileUpload ID="file_data" runat="server" class="mutitxt_03" Width="80%" />
                </td>
            </tr>
            <tr>
                <td height="39" align="right" class="line_01" style="width: 25%">
                </td>
                <td style="width: 75%">
                    <asp:Button ID="btnupload" runat="server" Text="确定上传" CssClass="btn btn-primary" 
                        onclick="btnupload_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
