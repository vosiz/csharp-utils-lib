using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Vosiz.Commons;

namespace Vosiz.Extends
{
    public static class EnumExt
    {
        public static string GetDescription(this Enum value)
        {
            Assert.OnNull(value);

            try
            {
                FieldInfo field = value.GetType().GetField(value.ToString());

                if (field != null)
                {
                    DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();

                    if (attribute != null)
                    {
                        return attribute.Description;
                    }
                }

                // default
                return value.ToString();
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }

        public static IEnumerable<T> GetAll<T>(this T instance) where T : Enum
        {
            return (T[])Enum.GetValues(typeof(T));
        }

        // Formats the enum's underlying integer value as a hex string, e.g. "0xFF"
        public static string ToHexString(this Enum value)
        {
            Assert.OnNull(value);

            return "0x" + Convert.ToInt32(value).ToString("X");
        }
    }
}
