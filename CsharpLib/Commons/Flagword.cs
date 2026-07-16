using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vosiz.Extends;
using Vosiz.Helpers;

namespace Vosiz.Commons
{
    public class Flagword
    {
        private Dictionary<Enum, bool> Flags;

        // Constructor
        public Flagword()
        {

            Flags = new Dictionary<Enum, bool>();
        }

        // Returns the underlying flags as a comma separated list
        public override string ToString()
        {
            return Flags.Implode();
        }

        // Sets or clears the given flag
        public void Set(Enum flag, bool set = true)
        {

            Add(flag, set);
        }

        // Clears the given flag
        public void Unset(Enum flag)
        {

            Add(flag, false);
        }

        // Flips the given flag and returns its new state
        public bool Toggle(Enum flag)
        {

            var current = Get(flag);
            Set(flag, !current);

            return !current;
        }

        // Returns whether the given flag is set, false if it was never set
        public bool Get(Enum flag)
        {

            try
            {
                return Flags.TryGet(flag);
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Returns whether the given flag is set
        public bool IsSet(Enum flag)
        {

            return Get(flag);
        }

        // Returns whether any flag is currently set
        public bool AnySet()
        {

            return Flags.Where(x => x.Value == true).Count() > 0;
        }

        // Returns a bracketed, comma separated list of descriptions for all set flags
        public string Display()
        {

            if (!AnySet())
                return string.Empty;

            var sb = new StringBuilder();
            sb.Append("[");
            sb.Append(ListOfSetDescriptions().Implode());
            sb.Append("]");

            return sb.ToString();
        }

        private void Add(Enum flag, bool set)
        {

            if (!Flags.TryAdd(flag, set))
                Flags[flag] = set;
        }

        private IEnumerable<string> ListOfSetDescriptions()
        {

            return Flags
                .Where(kv => kv.Value == true)
                .Select(kv => EnumHelper.GetDescriptions(kv.Key));
        }
    }
}
