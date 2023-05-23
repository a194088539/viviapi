using System.Collections.Generic;

namespace viviapi.ETAPI.ShenZhouFu
{
    public class queryresult
    {
        public string version { get; set; }

        public string merId { get; set; }

        public string queryResult { get; set; }

        public List<orderitem> orders { get; set; }
    }
}
