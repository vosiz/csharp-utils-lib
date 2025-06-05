using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vosiz.Utils;

namespace Vosiz.Helpers
{
    public static class EnumHelper
    {
        public static Dictionary<int, string> GetAll<TEnum>() where TEnum : Enum
        {
            return
                Enum.GetValues(typeof(TEnum))
                    .Cast<TEnum>()
                    .ToDictionary(e => Convert.ToInt32(e), e => e.ToString());
        }

        public static TEnum Random<TEnum>(params TEnum[] ignore) where TEnum : Enum
        {
            return Randomizer.Next<TEnum>(ignore);
        }

        public static string GetDescriptions(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }

    }
}
