using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vosiz.Extends
{
    public static class ObjectExt
    {

        // Attempts to convert the object to the target type, without throwing
        public static bool TryConvert(this object obj, Type target_type, out object result)
        {

            try
            {
                if (obj == null)
                {
                    if (!target_type.IsValueType || Nullable.GetUnderlyingType(target_type) != null)
                    {
                        result = null;
                        return true;
                    }

                    result = default;
                    return false;
                }

                if (target_type.IsInstanceOfType(obj))
                {
                    result = obj;
                    return true;
                }

                var underlying_type = Nullable.GetUnderlyingType(target_type) ?? target_type;

                if (underlying_type.IsEnum)
                {
                    if (obj is string str && str.TryParseEnum(underlying_type, true, out var enum_value))
                    {
                        result = enum_value;
                        return true;
                    }

                    if (obj is IConvertible)
                    {
                        result = Enum.ToObject(underlying_type, Convert.ChangeType(obj, Enum.GetUnderlyingType(underlying_type)));
                        return true;
                    }
                }

                if (obj is IConvertible)
                {
                    result = Convert.ChangeType(obj, underlying_type);
                    return true;
                }
            }
            catch { }

            result = default;
            return false;
        }
    }
}
