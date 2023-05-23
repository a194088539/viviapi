using System;

namespace viviapi.Model.Order
{
    [Serializable]
    public class CheckCardResult
    {
        public byte isRepeat { get; set; }

        public byte makeup { get; set; }

        public int supplierid { get; set; }

        public Decimal supprate { get; set; }

        public Decimal withhold { get; set; }

        public string cardpwd { get; set; }
    }
}
