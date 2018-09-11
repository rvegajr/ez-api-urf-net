using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ez.Common.Extentions
{
    public static class ObjectExtensions
    {
        public static bool? AsNullableBoolean(this object obj)
        {
            bool? ret = null;
            if (obj != null)
            {
                var objAsStr = obj.ToString();
                ret = (objAsStr == "1") || (objAsStr.ToUpper().StartsWith("T")) || (objAsStr.ToUpper().StartsWith("Y"));
            }
            return ret;
        }

        public static bool AsBoolean(this object obj)
        {
            bool ret = false;
            if (obj != null)
            {
                var objAsStr = obj.ToString();
                ret = (objAsStr == "1") || (objAsStr.ToUpper().StartsWith("T")) || (objAsStr.ToUpper().StartsWith("Y"));
            }
            return ret;
        }

        public static int AsInt(this object obj, int ValueIfNull)
        {
            int ret = ValueIfNull;
            if (obj != null)
            {
                if (obj.ToString().Contains("."))
                {
                    Double temp;
                    Boolean isOk = Double.TryParse(obj.ToString(), out temp);
                    ret = isOk ? (int)temp : 0;
                }
                else
                {
                    int.TryParse(obj.ToString(), out ret);
                }
            }
            return ret;
        }

        public static int? AsIntNullable(this object obj, int? ValueIfNull)
        {
            int? ret = ValueIfNull;
            if (obj != null)
            {
                if (obj.ToString().Contains("."))
                {
                    Double temp;
                    Boolean isOk = Double.TryParse(obj.ToString(), out temp);
                    ret = isOk ? (int?)temp : 0;
                }
                else
                {
                    int i = 0;
                    int.TryParse(obj.ToString(), out i);
                    ret = (int?)i;
                }
            }
            return ret;
        }
        public static DateTime AsDateTime(this object obj, DateTime ValueIfNull)
        {
            DateTime ret = ValueIfNull;
            if (obj != null)
            {
                DateTime temp;
                Boolean isOk = DateTime.TryParse(obj.ToString(), out temp);
                ret = isOk ? temp : ValueIfNull;

            }
            return ret;
        }
        public static DateTime? AsDateTimeNullable(this object obj, DateTime? ValueIfNull)
        {
            DateTime? ret = ValueIfNull;
            if (obj != null)
            {
                DateTime temp;
                Boolean isOk = DateTime.TryParse(obj.ToString(), out temp);
                ret = isOk ? temp : ValueIfNull;

            }
            return ret;
        }
        public static double AsDouble(this object obj, double ValueIfNull)
        {
            double ret = ValueIfNull;
            if (obj != null)
            {
                Double temp;
                Boolean isOk = Double.TryParse(obj.ToString(), out temp);
                ret = isOk ? (double)temp : 0;
            }
            return ret;
        }
    }
}
