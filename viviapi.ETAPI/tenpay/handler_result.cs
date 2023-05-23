using System;

namespace viviapi.ETAPI.tenpay
{
    public class handler_result
    {
        public int type { get; set; }

        public int serial { get; set; }

        public string rec_acc { get; set; }

        public string rec_name { get; set; }

        public string cur_type { get; set; }

        public Decimal pay_amt { get; set; }

        public string trans_id { get; set; }

        public string desc { get; set; }
    }
}
