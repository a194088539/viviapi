using System;
using System.Text.RegularExpressions;

namespace viviLib.Utils
{
    public class TypeParse
    {
        public static bool IsDouble(object Expression)
        {
            return Expression != null && Regex.IsMatch(Expression.ToString(), "^([0-9])[0-9]*(\\.\\w*)?$");
        }

        public static bool IsNumeric(object Expression)
        {
            if (Expression != null)
            {
                string input = Expression.ToString();
                if (input.Length > 0 && input.Length <= 11 && Regex.IsMatch(input, "^[-]?[0-9]*[.]?[0-9]*$") && (input.Length < 10 || input.Length == 10 && (int)input[0] == 49 || input.Length == 11 && (int)input[0] == 45 && (int)input[1] == 49))
                    return true;
            }
            return false;
        }

        public static bool IsNumericArray(string[] strNumber)
        {
            if (strNumber == null || strNumber.Length < 1)
                return false;
            foreach (object Expression in strNumber)
            {
                if (!TypeParse.IsNumeric(Expression))
                    return false;
            }
            return true;
        }

        public static bool StrToBool(object Expression, bool defValue)
        {
            if (Expression != null)
            {
                if (string.Compare(Expression.ToString(), "true", true) == 0)
                    return true;
                if (string.Compare(Expression.ToString(), "false", true) == 0)
                    return false;
            }
            return defValue;
        }

        public static float StrToFloat(object strValue, float defValue)
        {
            if (strValue == null || strValue.ToString().Length > 10)
                return defValue;
            float num = defValue;
            if (strValue != null && Regex.IsMatch(strValue.ToString(), "^([-]|[0-9])[0-9]*(\\.\\w*)?$"))
                num = Convert.ToSingle(strValue);
            return num;
        }

        public static int StrToInt(object Expression, int defValue)
        {
            if (Expression != null)
            {
                string input = Expression.ToString();
                if (input.Length > 0 && input.Length <= 11 && Regex.IsMatch(input, "^[-]?[0-9]*$") && (input.Length < 10 || input.Length == 10 && (int)input[0] == 49 || input.Length == 11 && (int)input[0] == 45 && (int)input[1] == 49))
                    return Convert.ToInt32(input);
            }
            return defValue;
        }

        public static int[] StringToIntArray(string idList, int defValue)
        {
            if (string.IsNullOrEmpty(idList))
                return (int[])null;
            string[] strArray = Utils.SplitString(idList, ",");
            int[] numArray = new int[strArray.Length];
            for (int index = 0; index < strArray.Length; ++index)
                numArray[index] = TypeParse.StrToInt((object)strArray[index], defValue);
            return numArray;
        }

        public static int ObjectToInt(object expression)
        {
            return TypeParse.ObjectToInt(expression, 0);
        }

        public static int ObjectToInt(object expression, int defValue)
        {
            if (expression != null)
                return TypeParse.StrToInt((object)expression.ToString(), defValue);
            return defValue;
        }
    }
}
