using System;
using viviapi.BLL;
using viviapi.Model;

namespace viviapi.ETAPI
{
    public class Withdraw
    {
        private static viviapi.BLL.Withdraw.channelwithdraw chalBLL = new viviapi.BLL.Withdraw.channelwithdraw();

        public static void InitDistribution(SettledInfo itemInfo)
        {
            viviapi.Model.distribution distribution1 = new viviapi.Model.distribution();
            distribution1.trade_no = viviapi.BLL.distribution.GenerateTradeNo(1);
            distribution1.mode = new int?(1);
            distribution1.settledId = new int?(itemInfo.id);
            distribution1.batchNo = 1;
            distribution1.userid = itemInfo.userid;
            distribution1.balance = new Decimal(0);
            distribution1.bankCode = itemInfo.PayeeBank;
            distribution1.suppid = itemInfo.suppid;
            distribution1.bankName = SettledFactory.GetSettleBankName(itemInfo.PayeeBank);
            distribution1.bankBranch = itemInfo.Payeeaddress;
            distribution1.bankAccountName = itemInfo.payeeName;
            distribution1.bankAccount = itemInfo.Account;
            Decimal? charges;
            if (itemInfo.charges.HasValue)
            {
                viviapi.Model.distribution distribution2 = distribution1;
                Decimal amount = itemInfo.amount;
                charges = itemInfo.charges;
                Decimal num1 = charges.Value;
                Decimal num2 = amount - num1;
                distribution2.amount = num2;
            }
            else
                distribution1.amount = itemInfo.amount;
            viviapi.Model.distribution distribution3 = distribution1;
            charges = itemInfo.charges;
            Decimal num = charges.Value;
            distribution3.charges = num;
            distribution1.balance2 = new Decimal?(new Decimal(0));
            if (viviapi.BLL.distribution.Add(distribution1) <= 0)
                return;
            SellFactory.ReqDistribution(distribution1);
        }

        public static void InitDistribution2(viviapi.Model.Withdraw.settledAgent itemInfo)
        {
            viviapi.Model.distribution distribution = new viviapi.Model.distribution();
            distribution.trade_no = viviapi.BLL.distribution.GenerateTradeNo(2);
            distribution.suppid = itemInfo.suppid;
            distribution.mode = new int?(2);
            distribution.settledId = new int?(itemInfo.id);
            distribution.batchNo = 1;
            distribution.userid = itemInfo.userid;
            distribution.balance = new Decimal(0);
            distribution.bankCode = itemInfo.bankCode;
            distribution.bankName = itemInfo.bankName;
            distribution.bankBranch = itemInfo.bankBranch;
            distribution.bankAccountName = itemInfo.bankAccountName;
            distribution.bankAccount = itemInfo.bankAccount;
            distribution.amount = itemInfo.amount;
            distribution.charges = itemInfo.charge;
            distribution.balance2 = new Decimal?(new Decimal(0));
            if (viviapi.BLL.distribution.Add(distribution) <= 0)
                return;
            SellFactory.ReqDistribution(distribution);
        }

        public static int Complete(int suppId, string trade_no, bool is_cancel, int status, string amount, string supp_trade_no, string message)
        {
            string bill_trade_no = string.Empty;
            int num = viviapi.BLL.distribution.Process(suppId, trade_no, is_cancel, status, amount, supp_trade_no, message, out bill_trade_no);
            if (num == 0 && trade_no.Substring(0, 1) == "2")
                new viviapi.BLL.Withdraw.settledAgent().DoNotify(bill_trade_no);
            return num;
        }
    }
}
