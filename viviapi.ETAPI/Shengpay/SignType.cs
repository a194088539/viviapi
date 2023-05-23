namespace viviapi.ETAPI.Shengpay
{
    public class SignType
    {
        public static SignType MD5 = new SignType("2", "MD5");
        public static SignType RSA = new SignType("1", "RSA");
        private string code;
        private string name;

        public string Code
        {
            get
            {
                return this.code;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        private SignType(string code, string name)
        {
            this.code = code;
            this.name = name;
        }

        public static SignType getByCode(string code)
        {
            switch (code)
            {
                case "2":
                    return SignType.MD5;
                case "1":
                    return SignType.RSA;
                default:
                    return (SignType)null;
            }
        }
    }
}
