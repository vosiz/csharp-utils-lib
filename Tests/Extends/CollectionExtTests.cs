using System;
using System.Collections.Generic;
using Vosiz.Extends;

namespace Tests.Extends
{

    public static class CollectionExtTests
    {

        // RandomValue returns an item that is actually part of the collection
        public static void RandomValueReturnsItemFromCollection() {

            List<int> values = new List<int> { 1, 2, 3 };
            int picked = values.RandomValue();

            Check.True(values.Contains(picked), "Picked value should belong to the collection");
        }

        // RandomValue throws for a null collection
        public static void RandomValueThrowsForNullCollection() {

            List<int> values = null;

            Check.Throws<ArgumentNullException>(() => values.RandomValue());
        }

        // RandomValue throws for an empty collection
        public static void RandomValueThrowsForEmptyCollection() {

            List<int> values = new List<int>();

            Check.Throws<InvalidOperationException>(() => values.RandomValue());
        }

        // GetItemType returns the generic element type
        public static void GetItemTypeReturnsElementType() {

            List<int> values = new List<int> { 1, 2, 3 };

            Check.Equal(typeof(int), values.GetItemType());
        }

        // AsArray appends the extra items to the returned array
        public static void AsArrayIncludesExtraItems() {

            List<int> values = new List<int> { 1, 2 };
            int[] result = values.AsArray(3, 4);

            Check.Equal(4, result.Length);
            Check.True(result[2] == 3 && result[3] == 4, "Extra items should be appended in order");
        }

        // AsArray without extra items behaves like a plain ToArray
        public static void AsArrayWithoutExtraItemsReturnsSameValues() {

            List<int> values = new List<int> { 1, 2 };
            int[] result = values.AsArray();

            Check.Equal(2, result.Length);
        }

        // Implode joins values with the given separator
        public static void ImplodeJoinsValuesWithSeparator() {

            List<int> values = new List<int> { 1, 2, 3 };

            Check.Equal("1-2-3", values.Implode("-"));
        }

        // Implode defaults to a comma-space separator
        public static void ImplodeDefaultsToCommaSpace() {

            List<int> values = new List<int> { 1, 2 };

            Check.Equal("1, 2", values.Implode());
        }

        // TryAdd adds a new key and returns true
        public static void TryAddAddsNewKey() {

            Dictionary<string, int> dict = new Dictionary<string, int>();

            bool added = dict.TryAdd("a", 1);

            Check.True(added, "Should report the key was added");
            Check.Equal(1, dict["a"]);
        }

        // TryAdd without update returns false and keeps the old value
        public static void TryAddWithoutUpdateKeepsOldValue() {

            Dictionary<string, int> dict = new Dictionary<string, int> { { "a", 1 } };

            bool added = dict.TryAdd("a", 2);

            Check.False(added, "Should report the key was not added");
            Check.Equal(1, dict["a"]);
        }

        // TryAdd with update overwrites the existing value
        public static void TryAddWithUpdateOverwritesValue() {

            Dictionary<string, int> dict = new Dictionary<string, int> { { "a", 1 } };

            bool added = dict.TryAdd("a", 2, update: true);

            Check.True(added, "Should report the value was updated");
            Check.Equal(2, dict["a"]);
        }

        // TryGet returns the value for an existing key
        public static void TryGetReturnsExistingValue() {

            Dictionary<string, int> dict = new Dictionary<string, int> { { "a", 1 } };

            Check.Equal(1, dict.TryGet("a"));
        }

        // TryGet throws for a missing key
        public static void TryGetThrowsForMissingKey() {

            Dictionary<string, int> dict = new Dictionary<string, int>();

            Check.Throws<IndexOutOfRangeException>(() => dict.TryGet("missing"));
        }

        // GetValue returns the value for an existing key
        public static void GetValueReturnsExistingValue() {

            Dictionary<string, int> dict = new Dictionary<string, int> { { "a", 1 } };

            Check.Equal(1, dict.GetValue("a"));
        }

        // GetValue throws for a missing key by default
        public static void GetValueThrowsForMissingKeyByDefault() {

            Dictionary<string, int> dict = new Dictionary<string, int>();

            Check.Throws<KeyNotFoundException>(() => dict.GetValue("missing"));
        }

        // GetValue returns the default value when told not to throw
        public static void GetValueReturnsDefaultWhenAllowed() {

            Dictionary<string, int> dict = new Dictionary<string, int>();

            Check.Equal(0, dict.GetValue("missing", if_notf_def: true));
        }

        // Implode on a dictionary joins the values, not the keys
        public static void DictionaryImplodeJoinsValues() {

            Dictionary<string, int> dict = new Dictionary<string, int> { { "a", 1 }, { "b", 2 } };

            Check.Equal("1, 2", dict.Implode());
        }

    }
}
