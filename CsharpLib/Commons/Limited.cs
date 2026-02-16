using System;
using System.Collections.Generic;
using System.Text;
using Vosiz.Utils;

namespace Vosiz.Commons {
    
    public class Limited<T> where T : struct, IComparable {

        public string Id { private set; get; }
        public T Min { private set; get; }
        public T Max { private set; get; }

        public Limited(string id, T min, T max) {

            Id = id;
            Min = min;
            Max = max;
        }

    }
}
