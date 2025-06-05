using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Vosiz.Utils
{
    public static class Randomizer
    {
        private static Random RandGen;

        public static void Init()
        {

            RandGen = new Random((int)DateTime.Now.Ticks);
        }

        public static int Next(int max = int.MaxValue)
        {

            return RandGen.Next(max);
        }

        public static int Next(int min, int max)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(min), "Minimum value cannot be greater than maximum value.");
            }
            return RandGen.Next(min, max);
        }

        public static double NextDouble(double min = 0, double max = 1)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(min), "Minimum value cannot be greater than maximum value.");
            }
            return RandGen.NextDouble() * (max - min) + min;
        }

        public static float NextFloat(float min = 0, float max = 1)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(min), "Minimum value cannot be greater than maximum value.");
            }
            return (float)NextDouble(min, max);
        }

        public static bool NextBool()
        {
            return Next() < (int.MaxValue / 2);
        }

        public static byte NextByte()
        {
            return (byte)Next(0, 256);
        }

        public static char NextChar()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return chars[Next(chars.Length)];
        }

        public static string NextString(int length = 10)
        {
            return new string(Enumerable.Range(0, length).Select(_ => NextChar()).ToArray());
        }

        public static decimal NextDecimal(decimal min = 0, decimal max = 1)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(min), "Minimum value cannot be greater than maximum value.");
            }
            return (decimal)NextDouble((double)min, (double)max);
        }

        public static UInt16 NextUshort(ushort min = 0, ushort max = ushort.MaxValue)
        {

            return (UInt16)NextUint((UInt32)min, (UInt32)max);
        }

        public static uint NextUint(uint min = 0, uint max = int.MaxValue)
        {
            return (uint)Next((int)min, (int)max);
        }

        public static long NextLong(long min = 0, long max = long.MaxValue)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(min), "Minimum value cannot be greater than maximum value.");
            }
            var buffer = new byte[8];
            RandGen.NextBytes(buffer);
            long rand = BitConverter.ToInt64(buffer, 0);
            return min + Math.Abs(rand % (max - min));
        }

        public static ulong NextULong(ulong min = 0, ulong max = ulong.MaxValue)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(min), "Minimum value cannot be greater than maximum value.");
            }
            var buffer = new byte[8];
            RandGen.NextBytes(buffer);
            ulong rand = BitConverter.ToUInt64(buffer, 0);
            return min + (rand % (max - min));
        }

        public static DateTime NextDateTime(DateTime? min = null, DateTime? max = null)
        {
            if (min == null) min = new DateTime(2000, 1, 1);
            if (max == null) max = DateTime.Today;

            var range = (max.Value - min.Value).Days;
            return min.Value.AddDays(Next(range)).AddSeconds(Next(86400));
        }

        public static TEnum Next<TEnum>(params TEnum[] ignore) where TEnum : Enum
        {
            var values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            var filtered = values.Except(ignore).ToList();

            if (!filtered.Any())
                throw new InvalidOperationException("No values available after excluding ignored ones.");

            return filtered[Next(filtered.Count)];
        }

        public static T Next<T>()
        {
            return (T)GenerateRandom(typeof(T));
        }

        private static object GenerateRandom(Type type, int depth = 0)
        {
            if (depth > 10) return GetDefaultValue(type);
            if (type == typeof(bool)) return NextBool();
            if (type == typeof(byte)) return NextByte();
            if (type == typeof(char)) return NextChar();
            if (type == typeof(int)) return Next(1000);
            if (type == typeof(uint)) return NextUint();
            if (type == typeof(long)) return NextLong();
            if (type == typeof(ulong)) return NextULong();
            if (type == typeof(decimal)) return NextDecimal(0, 1000);
            if (type == typeof(float)) return NextFloat(0, 1000);
            if (type == typeof(double)) return NextDouble(0, 1000);
            if (type == typeof(string)) return NextString();
            if (type == typeof(DateTime)) return NextDateTime();

            if (type.IsEnum)
            {
                Array values = Enum.GetValues(type);
                int index = Next(values.Length);
                return values.GetValue(index);
            }

            if (type.IsArray)
            {
                Type elementType = type.GetElementType();
                int length = Next(5, 10);
                Array array = Array.CreateInstance(elementType, length);
                for (int i = 0; i < length; i++)
                {
                    array.SetValue(GenerateRandom(elementType, depth + 1), i);
                }
                return array;
            }

            if (type.IsGenericType)
            {
                var genericTypeDef = type.GetGenericTypeDefinition();
                var args = type.GetGenericArguments();

                if (genericTypeDef == typeof(List<>))
                {
                    var elementType = args[0];
                    var list = (IList)Activator.CreateInstance(type);
                    int count = Next(5, 10);
                    for (int i = 0; i < count; i++)
                    {
                        list.Add(GenerateRandom(elementType, depth + 1));
                    }
                    return list;
                }

                if (genericTypeDef == typeof(IEnumerable<>))
                {
                    var elementType = args[0];
                    var listType = typeof(List<>).MakeGenericType(elementType);
                    return GenerateRandom(listType, depth + 1);
                }

                if (genericTypeDef == typeof(Dictionary<,>))
                {
                    var keyType = args[0];
                    var valueType = args[1];
                    var dict = (IDictionary)Activator.CreateInstance(type);
                    int count = Next(5, 10);
                    for (int i = 0; i < count; i++)
                    {
                        var key = GenerateRandom(keyType, depth + 1);
                        var value = GenerateRandom(valueType, depth + 1);
                        if (!dict.Contains(key))
                            dict.Add(key, value);
                    }
                    return dict;
                }

                if (genericTypeDef == typeof(ObservableCollection<>))
                {
                    var elementType = args[0];
                    var collectionType = typeof(ObservableCollection<>).MakeGenericType(elementType);
                    var collection = (IList)Activator.CreateInstance(collectionType);
                    int count = Next(5, 10);
                    for (int i = 0; i < count; i++)
                    {
                        collection.Add(GenerateRandom(elementType, depth + 1));
                    }
                    return collection;
                }
            }

            if (type.IsClass || (type.IsValueType && !type.IsPrimitive))
            {
                var ctor = type.GetConstructor(Type.EmptyTypes);
                if (ctor == null)
                    throw new InvalidOperationException($"Type {type.Name} does not have a parameterless constructor.");

                var instance = Activator.CreateInstance(type);

                var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var prop in props)
                {
                    try
                    {
                        var value = GenerateRandom(prop.PropertyType, depth + 1);
                        if (prop.CanWrite && prop.SetMethod != null && prop.SetMethod.IsPublic)
                        {
                            prop.SetValue(instance, value);
                        }
                        else
                        {
                            SetInitOnlyProperty(instance, prop, value);
                        }
                    }
                    catch { }
                }

                return instance;
            }

            throw new NotSupportedException($"Generation of type {type.Name} is not supported.");
        }

        private static void SetInitOnlyProperty(object target, PropertyInfo property, object value)
        {
            var backingField = target.GetType().GetField($"<{property.Name}>k__BackingField",
                BindingFlags.Instance | BindingFlags.NonPublic);

            backingField?.SetValue(target, value);
        }

        private static object GetDefaultValue(Type type)
        {
            if (!type.IsValueType || Nullable.GetUnderlyingType(type) != null)
                return null;

            return Activator.CreateInstance(type);
        }
    }
}
