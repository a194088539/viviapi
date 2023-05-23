using System.Collections.Generic;

namespace viviapi.ETAPI
{
    public class OfSUPSupplyResult
    {
        public string status { get; set; }

        public string msg { get; set; }

        public List<OfSUPGetOrderdataList> data { get; set; }

        public string orderids { get; set; }
    }
}
