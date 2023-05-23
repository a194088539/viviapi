using System.Text;

namespace viviLib.Text
{
    public sealed class Regular
    {
        public static string ImageRegularString
        {
            get
            {
                return Regular.GetFileExtRegularString(new string[5]
                {
          "jpg",
          "gif",
          "jpeg",
          "bmp",
          "png"
                });
            }
        }

        private Regular()
        {
        }

        public static string GetFileExtRegularString(string[] exts)
        {
            if (exts.Length <= 0)
                return string.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(".*(\\.(");
            for (int index = 0; index < exts.Length; ++index)
            {
                if (index > 0)
                    stringBuilder.Append("|");
                if (exts.Length > 1)
                    stringBuilder.Append("(");
                string str = exts[index].Trim().ToLower();
                for (int startIndex = 0; startIndex < str.Length; ++startIndex)
                    stringBuilder.AppendFormat("({0}|{1})", (object)str.Substring(startIndex, 1).ToLower(), (object)str.Substring(startIndex, 1).ToUpper());
                if (exts.Length > 1)
                    stringBuilder.Append(")");
            }
            stringBuilder.Append("))$");
            return stringBuilder.ToString();
        }

        public static string GetRegularString(RegularType type)
        {
            return Regular.GetRegularString(type, 0);
        }

        public static string GetRegularString(RegularType type, int length)
        {
            switch (type)
            {
                case RegularType.Word:
                    if (length > 0)
                        return "^[\\w]{0," + string.Format("{0:d}", (object)length) + "}$";
                    return "^[\\w]*$";
                case RegularType.Email:
                    return "\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                case RegularType.Url:
                    return "^((h|H)(t|T)(t|T)(p|P)|(f|F)(t|T)(p|P)|(f|F)(i|I)(l|L)(e|E)|(t|T)(e|E)(l|L)(n|N)(e|E)(t|T)|(g|G)(o|O)(p|P)(h|H)(e|E)(r|R)|(h|H)(t|T)(t|T)(p|P)(s|S)|(m|M)(a|A)(i|I)(l|L)(t|T)(o|O)|(n|N)(e|E)(w|W)(s|S)|(w|W)(a|A)(i|I)(s|S))://([\\w-]+(\\.)?)+[\\w-]+(:\\d+)?(/[\\w- ./?%&=]*)?$";
                case RegularType.Number:
                    return "^-{0,1}\\d{1,}\\.{0,1}\\d{0,}$";
                case RegularType.Int:
                    return "^-{0,1}\\d{1,}$";
                case RegularType.Date:
                    return "^((((1[6-9]|[2-9]\\d)\\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\\d|3[01]))|(((1[6-9]|[2-9]\\d)\\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\\d|30))|(((1[6-9]|[2-9]\\d)\\d{2})-0?2-(0?[1-9]|1\\d|2[0-8]))|(((1[6-9]|[2-9]\\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$";
                case RegularType.DateTime:
                    return "^((((1[6-9]|[2-9]\\d)\\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\\d|3[01]))|(((1[6-9]|[2-9]\\d)\\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\\d|30))|(((1[6-9]|[2-9]\\d)\\d{2})-0?2-(0?[1-9]|1\\d|2[0-8]))|(((1[6-9]|[2-9]\\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\\d):[0-5]?\\d:[0-5]?\\d$";
                case RegularType.Time:
                    return "^(20|21|22|23|[0-1]?\\d):[0-5]?\\d:[0-5]?\\d$";
                case RegularType.ChinesePostalCode:
                    return "\\d{6}";
                case RegularType.ChineseIDCard:
                    return "(^\\d{17}[xX\\d]{1}$)|(^\\d{15}$)";
                case RegularType.Domain:
                    return "^\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$";
                default:
                    return string.Empty;
            }
        }
    }
}
