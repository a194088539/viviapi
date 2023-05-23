<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BankSelect.aspx.cs" Inherits="viviapi.gateway.links.BankSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
      <title>银行充值</title>
    <script type="text/javascript" src="/js/common.js"></script>
    <style type="text/css">
        body
        {
            font-size: 12px;
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
        }
        .STYLE1
        {
            color: #2179DD;
        }
    </style>
      <script type="text/javascript">
          $(document).ready(function() {
              $("input:[name=pd_FrpId]:radio").each(function() {
                  if (this.value == $("#hftypeid").val()) {
                      this.checked = true;
                  }
              });
          });
</script>
</head>
<body>
    <form id="form1" runat="server">
     <asp:HiddenField ID="hftypeid" runat="server" />
    <table width="100%" height="34" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="34">
                <img src="../images/pic_1.gif" width="69" height="60" />
            </td>
            <td width="100%" background="img/pic_3.gif" bgcolor="#2179DD">
                <img src="../images/pic_4.gif" width="40" height="40" />充值第二步，银行选择
            </td>
            <td width="13" height="34">
                <img src="../images/pic_2.gif" width="69" height="60" />
            </td>
        </tr>
    </table>
    <br />
    <table width="864" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#5c9acf"
        class="mytable">
        <tr>
            <td width="100%" height="88" bgcolor="#FFFFFF">
                <br />
                    <table class="table_main" width="500" border="0" align="center" cellpadding="1" cellspacing="1">
                    <tr>
                        <td height="25" align="right" bgcolor="#FFFFFF">
                            <span class="STYLE1">系统订单：</span>
                        </td>
                        <td>
                            <asp:Literal ID="litSysOrderId" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td height="25" align="right" bgcolor="#FFFFFF">
                            <span class="STYLE1">商户订单：</span>
                        </td>
                        <td>
                            <asp:Literal ID="litUserOrderId" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td width="170" height="25" align="right" bgcolor="#FFFFFF">
                            <span class="STYLE1">商户ID：</span>
                        </td>
                        <td width="323" bgcolor="#FFFFFF">
                            <asp:Literal ID="litUserId" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" align="right" bgcolor="#FFFFFF">
                            <span class="STYLE1">充值类别：</span>
                        </td>
                        <td width="323" bgcolor="#FFFFFF">
                            <asp:Literal ID="litTypeViewName" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" align="right" bgcolor="#FFFFFF">
                            <span class="STYLE1">充值金额：</span>
                        </td>
                        <td>
                            <asp:Literal ID="litTratAmt" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" align="right" bgcolor="#FFFFFF">
                            <span class="STYLE1">银行选择：</span>
                        </td>
                        <td>
                            <table id="tb_bank" runat="server" width="100%" border="0" cellspacing="0" cellpadding="0" style="display:none">
                                <tr>
                                    <td align="center" width="30" height="35">
                                        <input type="radio" checked="checked"  name="pd_FrpId" value="967" />
                                    </td>
                                    <td>
                                        <img align="absmiddle" src="../images/bank/ICBC-NET.png" width="154" height="33" alt="中国工商银行" border="0" />
                                    </td>
                                    <td align="center">
                                        <input type="radio" name="pd_FrpId" value="964" />
                                    </td>
                                    <td>
                                        <img align="absmiddle" src="../images/bank/ABC-NET.png" width="154" height="33" alt="中国农业银行" border="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" width="30" height="35">
                                        <input type="radio" name="pd_FrpId" value="970" />
                                    </td>
                                    <td>
                                        <img align="absmiddle" src="../images/bank/CMBCHINA-NET.png" width="154" height="33" alt="中国招商银行" border="0" />
                                    </td>
                                    <td align="center" width="30" height="35">
                                        <input type="radio" name="pd_FrpId" value="963" />
                                    </td>
                                    <td>
                                        <img align="absmiddle" src="../images/bank/BOC-NET.png" width="154" height="33" alt="中国银行" border="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" width="30" height="35">
                                        <input type="radio" name="pd_FrpId" value="972" />
                                    </td>
                                    <td>
                                        <img align="absmiddle" src="../images/bank/CIB-NET.png" width="154" height="33" alt="兴业银行" border="0" />
                                    </td>
                                    <td align="center">
                                        <input type="radio" name="pd_FrpId" value="986" />
                                    </td>
                                    <td>
                                        <img align="absmiddle" src="../images/bank/CEB-NET.jpg" width="154" height="33" alt="光大银行" border="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" width="30" height="35">
                                        <input type="radio" name="pd_FrpId" value="974" />
                                    </td>
                                    <td>
                                        <img align="absmiddle" src="../images/bank/SDB-NET.png" width="154" height="33" alt="深圳发展银行" border="0" />
                                    </td>
                                    <td align="center">
                                        <input type="radio" name="pd_FrpId" value="990" />
                                    </td>
                                    <td>
                                        <img align="absmiddle" src="../images/bank/BJRCB-NET.jpg" width="154" height="33" alt="北京农村商业银行" border="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <input type="radio" name="pd_FrpId" value="965" />
                                    </td>
                                    <td>
                                        <img align="absmiddle" src="../images/bank/CCB-NET.png" width="154" height="33" alt="建设银行" border="0" />
                                    </td>
                                    <td align="center" width="30" height="35">
                                        <input type="radio" name="pd_FrpId" value="980" />
                                    </td>
                                    <td>
                                        <img align="absmiddle" src="../images/bank/1001009.png" width="154" height="33" alt="中国民生银行民生卡" border="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" width="30" height="35">
                                        <input type="radio" name="pd_FrpId" value="962" />
                                    </td>
                                    <td>
                                        <img align="absmiddle" src="../images/bank/ECITIC-NET.png" width="154" height="33"alt="中信银行" border="0" />
                                    </td>
                                    <td align="center">
                                        <input type="radio" name="pd_FrpId" value="981" />
                                    </td>
                                    <td>
                                        <img align="absmiddle" src="../images/bank/BOCO-NET.png" width="154" height="33" alt="交通银行" border="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <input type="radio" name="pd_FrpId" value="989" />
                                    </td>
                                    <td>
                                        <img align="absmiddle" src="../images/bank/BCCB-NET.png" width="154" height="33" alt="北京银行" border="0" />
                                    </td>
                                    <td align="center" width="30" height="35">
                                        <input type="radio" name="pd_FrpId" value="978" />
                                    </td>
                                    <td>
                                        <img align="absmiddle" src="../images/bank/PAB-NET.jpg" width="154" height="33" alt="平安银行" border="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" width="30" height="35">
                                        <input type="radio" name="pd_FrpId" value="977" />
                                    </td>
                                    <td>
                                        <img align="absmiddle" src="../images/bank/SPDB-NET.png" width="154" height="33" alt="上海浦东发展银行" border="0" />
                                    </td>
                                    <td align="center">
                                        <input type="radio" name="pd_FrpId" value="985" />
                                    </td>
                                    <td>
                                        <img align="absmiddle" src="../images/bank/GDB-NET.png" width="154" height="33" alt="广东发展银行" border="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <input type="radio" name="pd_FrpId" value="988" />
                                    </td>
                                    <td>
                                        <img align="absmiddle" src="../images/bank/CBHB-NET.jpg" width="154" height="33"alt="渤海银行 " border="0" />
                                    </td>
                                    <td align="center" width="30" height="35">
                                        <input type="radio" name="pd_FrpId" value="987" />
                                    </td>
                                    <td>
                                        <img align="absmiddle" src="../images/bank/HKBEA-NET.jpg" width="154" height="33" alt="东亚银行" border="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" width="30" height="35">
                                        <input type="radio" name="pd_FrpId" value="997" />
                                    </td>
                                    <td>
                                        <img align="absmiddle" src="../images/bank/NBCB-NET.jpg" width="154" height="33"
                                            alt="宁波银行" border="0" />
                                    </td>
                                    <td align="center">
                                        <input type="radio" name="pd_FrpId" value="971" />
                                    </td>
                                    <td>
                                        <img align="absmiddle" src="../images/bank/POST-NET.png" width="154" height="33"
                                            alt="中国邮政" border="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" width="30" height="35">
                                        <input type="radio" name="pd_FrpId" value="979" />
                                    </td>
                                    <td>
                                        <img align="absmiddle" src="../images/bank/NJCB-NET.jpg" width="154" height="33"
                                            alt="南京银行" border="0" />
                                    </td>                                   
                                </tr>                               
                                
                            </table>
                            <table id="tb_alipay" runat="server" width="100%" border="0" cellspacing="0" cellpadding="0" style="display:none">                                
                                <tr>
                                    <td align="center" width="30" height="35">
                                        <input type="radio" name="pd_FrpId" value="992" checked="checked" />
                                    </td>
                                    <td>
                                        <img align="absmiddle" src="../images/bank/ALIPAY.gif" width="154" height="33"
                                            alt="支付宝" border="0" />
                                    </td>                                     
                                </tr>                                
                            </table>
                             <table id="tb_tenpay" runat="server" width="100%" border="0" cellspacing="0" cellpadding="0" style="display:none">                                
                                <tr>
                                     <td align="center" width="30" height="35">
                                        <input type="radio" name="pd_FrpId" value="993" checked="checked" />
                                    </td>
                                    <td>
                                        <img align="absmiddle" src="../images/bank/TENPAY.gif" width="154" height="33"
                                            alt="财付通" border="0" />
                                    </td>                                  
                                </tr>                                
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" align="right" bgcolor="#FFFFFF">
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnCmmit" runat="server" Text="确 定" Height="30px" Width="160px" 
                                onclick="btnCmmit_Click"/>                            
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
