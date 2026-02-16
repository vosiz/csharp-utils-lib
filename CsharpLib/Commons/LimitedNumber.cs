using System;
using System.Collections.Generic;
using System.Text;
using Vosiz.Utils;

namespace Vosiz.Commons {

    public class LimitedNumber<T> : Limited<T> where T : struct, IComparable {

        public LimitedNumber(string id, T min, T max) : base(id, min, max) { }

        public T RandomNumber() {

            Type t = typeof(T);

            if (t == typeof(int)) {

                int a = (int)(object)Min;
                int b = (int)(object)Max;
                int v = Randomizer.Next(a, b);
                return (T)(object)v;
            }

            if (t == typeof(long)) {

                long a = (long)(object)Min;
                long b = (long)(object)Max;
                long v = Randomizer.NextLong(a, b);
                return (T)(object)v;
            }

            if (t == typeof(short)) {

                short a = (short)(object)Min;
                short b = (short)(object)Max;
                int v = Randomizer.Next(a, b);
                return (T)(object)(short)v;
            }

            if (t == typeof(byte)) {

                byte a = (byte)(object)Min;
                byte b = (byte)(object)Max;
                int v = Randomizer.Next(a, b);
                return (T)(object)(byte)v;
            }

            decimal min = Convert.ToDecimal(Min);
            decimal max = Convert.ToDecimal(Max);

            decimal d = Randomizer.NextDecimal(min, max);

            if (t == typeof(decimal))
                return (T)(object)d;

            if (t == typeof(double))
                return (T)(object)(double)d;

            if (t == typeof(float))
                return (T)(object)(float)d;

            throw new NotSupportedException(
                string.Format("Unsupported number type {0}.", t.FullName));
        }
    }
}
