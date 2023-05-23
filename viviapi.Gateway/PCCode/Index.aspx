<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Index.aspx.cs" Inherits="viviapi.gateway.WeiXin.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ɨ��֧�� </title>
    <link id="linkWebCss" href="/App_Themes/Cashier/Web.min.css" rel="stylesheet" />
    <link id="linkWeixinCss" href="/App_Themes/Cashier/Weixin.css" rel="stylesheet" />
    <link id="linkPaymentDialogCss" href="/App_Themes/Cashier/PaymentDialog.css" rel="stylesheet" />
    <style type="text/css">
        
    </style>
    <script src="/js/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script>
        var times = 1;
        function clock() {
            var result = "error";
            $.ajax({
                type: 'get',
                url: "/tools/QueryOrder.ashx?wxorderid=<%=wxorderid%>", //ʵ���޸�Ϊ��ѯ�������ݿ�����֧�������¼
                success: function (result) {
                    checkResult(result);
                }
            });
        }

        function checkResult(result) {
            if (result == "0") {
                //ִ�ж�ʱ����
                if (times < 50) {
                    setTimeout("clock();", 1000);
                    times++;
                }

            } else if (result == "1") {
                $("#content").hide();
                $("#payok").show();
                setTimeout("jumpUrl();", 1000);
            } else if (result == "ERR1") {
                $("#res .ti").html("��������");
                $("#res").show();
            }
            else if (result == "ERR2") {
                $("#res .ti").html("֧����¼�����ڣ��뷵���̻�ҳ�������ύ��");
                $("#res").show();
            }
        }

        function jumpUrl() {
            document.location.href = "/PayOk.aspx?orderid=<%=wxorderid%>"; //�̻���תҳ��
        }

        $(document).ready(function () {
            setTimeout("clock();", 8000);
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divTitle" class="Header">
            <div class="Wrap1000">
                <div class="Logo" style="width: 340px; margin: auto;">
                    <span style="width: 340px;"></span>
                </div>
            </div>
        </div>
        <div id="divLine" style="border-bottom: 3px solid #A2AABB; margin-top: 10px;">
        </div>
        <div id="div1000" class="Wrap1000">
            <div>
                <div id="IsShowBillInfo" class="divShow" style="height: 180px; text-align: center;">
                    <div>
                        <span class="Hint">�������츶��Ա㶩����ʱ���� </span>
                        <br />
                        <span class="Sum">�������ύ������<span style="color: #f60; font-weight: bold;">5����</span>�����֧�������򶩵����Զ�ȡ����</span><br />
                        <span class="Hint">������:<asp:Label ID="Labelno" runat="server"></asp:Label></span><br />
                        <br />
                        <span class="Hint">Ӧ����<asp:Label ID="LabelAmt" runat="server"></asp:Label>Ԫ</span><br />
                        <br />
                        <span class="Hint">����ʱ��:<asp:Label ID="shijian" runat="server"></asp:Label></span><br />
                    </div>
                    <br />

                </div>
            </div>
            <div style="width: 100%;">
                <div id="divQRCode" class="divQRCode" style="margin: 0 auto;">
                    <p align="center" style="margin-top: -109; margin-left: -109">
                        <asp:Image ID="Image2" runat="server" Style="width: 245px; height: 245px;" />
                </div>
                <div id="imgQRCode" class="codeImg" style="margin: 0 auto;">
                    ��ʹ��<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>ɨһɨ��ɨ���ά��֧��
                </div>
            </div>
        </div>

    </form>
</body>
</html>
