using System;
using Vosiz.Extends;

namespace Tests.Extends
{

    public static class StringExtTests
    {

        // ToByteArray encodes the string as UTF-8
        public static void ToByteArrayEncodesUtf8() {

            byte[] bytes = "abc".ToByteArray();

            Check.Equal(3, bytes.Length);
            Check.Equal((byte)'a', bytes[0]);
            Check.Equal((byte)'b', bytes[1]);
            Check.Equal((byte)'c', bytes[2]);
        }

        // Limit shortens a string longer than maxLength
        public static void LimitShortensLongString() {

            Check.Equal("abc", "abcdef".Limit(3));
        }

        // Limit leaves a shorter string untouched
        public static void LimitLeavesShortStringUntouched() {

            Check.Equal("ab", "ab".Limit(3));
        }

        // Limit returns null for a null input
        public static void LimitReturnsNullForNull() {

            string value = null;

            Check.Equal(null, value.Limit(3));
        }

        // RandomSubstring on an empty string returns empty
        public static void RandomSubstringOnEmptyStringReturnsEmpty() {

            Check.Equal(string.Empty, string.Empty.RandomSubstring());
        }

        // RandomSubstring with explicit index and length returns the exact slice
        public static void RandomSubstringWithExplicitIndexAndLength() {

            Check.Equal("cde", "abcdef".RandomSubstring(2, 3));
        }

        // RandomSubstring clamps the length so it does not run past the end of the string
        public static void RandomSubstringClampsLengthToStringEnd() {

            Check.Equal("ef", "abcdef".RandomSubstring(4, 10));
        }

        // RandomSubstring returns empty when the index is past the end of the string
        public static void RandomSubstringReturnsEmptyForIndexPastEnd() {

            Check.Equal(string.Empty, "abc".RandomSubstring(10));
        }

        // TryParseEnum parses a valid value
        public static void TryParseEnumParsesValidValue() {

            bool ok = "Monday".TryParseEnum(typeof(DayOfWeek), false, out object result);

            Check.True(ok, "Should successfully parse");
            Check.Equal(DayOfWeek.Monday, result);
        }

        // TryParseEnum honors ignore_case
        public static void TryParseEnumHonorsIgnoreCase() {

            bool ok = "monday".TryParseEnum(typeof(DayOfWeek), true, out object result);

            Check.True(ok, "Should successfully parse with ignored case");
            Check.Equal(DayOfWeek.Monday, result);
        }

        // TryParseEnum returns false for an invalid value
        public static void TryParseEnumReturnsFalseForInvalidValue() {

            bool ok = "NotADay".TryParseEnum(typeof(DayOfWeek), false, out object result);

            Check.False(ok, "Should fail to parse");
        }

        // TryParseEnum throws when the given type is not an enum
        public static void TryParseEnumThrowsForNonEnumType() {

            Check.Throws<ArgumentException>(() => "value".TryParseEnum(typeof(string), false, out object result));
        }

        // ToPascalCase converts a plain sentence
        public static void ToPascalCaseConvertsPlainSentence() {

            Check.Equal("NejakaVetaKterouJsemNapsal", "Nejaka veta, kterou jsem napsal.".ToPascalCase());
        }

        // ToPascalCase converts decorated text
        public static void ToPascalCaseConvertsDecoratedText() {

            Check.Equal("CiTrochuOdekorovanyText", "Ci trochu 'odekorovany' text".ToPascalCase());
        }

        // ToPascalCase leaves an already-PascalCase string unchanged
        public static void ToPascalCaseLeavesPascalCaseUnchanged() {

            Check.Equal("MePrevedNaSnakeCase", "MePrevedNaSnakeCase".ToPascalCase());
        }

        // ToPascalCase converts a dashed parameter name
        public static void ToPascalCaseConvertsDashedText() {

            Check.Equal("JaJsemParametr", "ja-jsem-parametr".ToPascalCase());
        }

        // ToPascalCase converts a snake_case header
        public static void ToPascalCaseConvertsSnakeCaseText() {

            Check.Equal("TotoJeHlavicka", "toto_je_hlavicka".ToPascalCase());
        }

        // ToPascalCase returns empty string for empty input
        public static void ToPascalCaseReturnsEmptyForEmptyString() {

            Check.Equal(string.Empty, string.Empty.ToPascalCase());
        }

        // ToPascalCase returns null for null input
        public static void ToPascalCaseReturnsNullForNull() {

            string value = null;

            Check.Equal(null, value.ToPascalCase());
        }

        // ToPascalCase keeps an acronym run as its own word
        public static void ToPascalCaseSplitsAcronymRun() {

            Check.Equal("HttpServer", "HTTPServer".ToPascalCase());
        }

        // ToPascalCase keeps digits attached to the preceding word
        public static void ToPascalCaseKeepsDigitsWithPrecedingWord() {

            Check.Equal("Item2Count", "item2_count".ToPascalCase());
        }

        // ToPascalCase capitalizes a single lowercase word
        public static void ToPascalCaseHandlesSingleWord() {

            Check.Equal("Word", "word".ToPascalCase());
        }

        // ToCamelCase converts a plain sentence
        public static void ToCamelCaseConvertsPlainSentence() {

            Check.Equal("nejakaVetaKterouJsemNapsal", "Nejaka veta, kterou jsem napsal.".ToCamelCase());
        }

        // ToCamelCase converts decorated text
        public static void ToCamelCaseConvertsDecoratedText() {

            Check.Equal("ciTrochuOdekorovanyText", "Ci trochu 'odekorovany' text".ToCamelCase());
        }

        // ToCamelCase converts an already-PascalCase string
        public static void ToCamelCaseConvertsPascalCaseText() {

            Check.Equal("mePrevedNaSnakeCase", "MePrevedNaSnakeCase".ToCamelCase());
        }

        // ToCamelCase converts a dashed parameter name
        public static void ToCamelCaseConvertsDashedText() {

            Check.Equal("jaJsemParametr", "ja-jsem-parametr".ToCamelCase());
        }

        // ToCamelCase converts a snake_case header
        public static void ToCamelCaseConvertsSnakeCaseText() {

            Check.Equal("totoJeHlavicka", "toto_je_hlavicka".ToCamelCase());
        }

        // ToCamelCase returns empty string for empty input
        public static void ToCamelCaseReturnsEmptyForEmptyString() {

            Check.Equal(string.Empty, string.Empty.ToCamelCase());
        }

        // ToCamelCase returns null for null input
        public static void ToCamelCaseReturnsNullForNull() {

            string value = null;

            Check.Equal(null, value.ToCamelCase());
        }

        // ToCamelCase leaves a single lowercase word unchanged
        public static void ToCamelCaseLeavesSingleWordUnchanged() {

            Check.Equal("word", "word".ToCamelCase());
        }

        // ToCamelCase keeps an acronym run as its own word
        public static void ToCamelCaseSplitsAcronymRun() {

            Check.Equal("httpServer", "HTTPServer".ToCamelCase());
        }

        // ToCamelCase keeps digits attached to the preceding word
        public static void ToCamelCaseKeepsDigitsWithPrecedingWord() {

            Check.Equal("item2Count", "Item2Count".ToCamelCase());
        }

        // ToSnakeCase converts a plain sentence
        public static void ToSnakeCaseConvertsPlainSentence() {

            Check.Equal("nejaka_veta_kterou_jsem_napsal", "Nejaka veta, kterou jsem napsal.".ToSnakeCase());
        }

        // ToSnakeCase converts decorated text
        public static void ToSnakeCaseConvertsDecoratedText() {

            Check.Equal("ci_trochu_odekorovany_text", "Ci trochu 'odekorovany' text".ToSnakeCase());
        }

        // ToSnakeCase converts a PascalCase identifier
        public static void ToSnakeCaseConvertsPascalCaseText() {

            Check.Equal("me_preved_na_snake_case", "MePrevedNaSnakeCase".ToSnakeCase());
        }

        // ToSnakeCase converts a dashed parameter name
        public static void ToSnakeCaseConvertsDashedText() {

            Check.Equal("ja_jsem_parametr", "ja-jsem-parametr".ToSnakeCase());
        }

        // ToSnakeCase leaves an already-snake_case header unchanged
        public static void ToSnakeCaseLeavesSnakeCaseUnchanged() {

            Check.Equal("toto_je_hlavicka", "toto_je_hlavicka".ToSnakeCase());
        }

        // ToSnakeCase returns empty string for empty input
        public static void ToSnakeCaseReturnsEmptyForEmptyString() {

            Check.Equal(string.Empty, string.Empty.ToSnakeCase());
        }

        // ToSnakeCase returns null for null input
        public static void ToSnakeCaseReturnsNullForNull() {

            string value = null;

            Check.Equal(null, value.ToSnakeCase());
        }

        // ToSnakeCase keeps an acronym run as its own word
        public static void ToSnakeCaseSplitsAcronymRun() {

            Check.Equal("http_server", "HTTPServer".ToSnakeCase());
        }

        // ToSnakeCase keeps digits attached to the preceding word
        public static void ToSnakeCaseKeepsDigitsWithPrecedingWord() {

            Check.Equal("item2_count", "Item2Count".ToSnakeCase());
        }

        // ToSnakeCase leaves a single lowercase word unchanged
        public static void ToSnakeCaseHandlesSingleWord() {

            Check.Equal("word", "word".ToSnakeCase());
        }

        // ToDashed converts a plain sentence
        public static void ToDashedConvertsPlainSentence() {

            Check.Equal("nejaka-veta-kterou-jsem-napsal", "Nejaka veta, kterou jsem napsal.".ToDashed());
        }

        // ToDashed converts decorated text
        public static void ToDashedConvertsDecoratedText() {

            Check.Equal("ci-trochu-odekorovany-text", "Ci trochu 'odekorovany' text".ToDashed());
        }

        // ToDashed converts a PascalCase identifier
        public static void ToDashedConvertsPascalCaseText() {

            Check.Equal("me-preved-na-snake-case", "MePrevedNaSnakeCase".ToDashed());
        }

        // ToDashed leaves an already-dashed parameter name unchanged
        public static void ToDashedLeavesDashedTextUnchanged() {

            Check.Equal("ja-jsem-parametr", "ja-jsem-parametr".ToDashed());
        }

        // ToDashed converts a snake_case header
        public static void ToDashedConvertsSnakeCaseText() {

            Check.Equal("toto-je-hlavicka", "toto_je_hlavicka".ToDashed());
        }

        // ToDashed returns empty string for empty input
        public static void ToDashedReturnsEmptyForEmptyString() {

            Check.Equal(string.Empty, string.Empty.ToDashed());
        }

        // ToDashed returns null for null input
        public static void ToDashedReturnsNullForNull() {

            string value = null;

            Check.Equal(null, value.ToDashed());
        }

        // ToDashed keeps an acronym run as its own word
        public static void ToDashedSplitsAcronymRun() {

            Check.Equal("http-server", "HTTPServer".ToDashed());
        }

        // ToDashed keeps digits attached to the preceding word
        public static void ToDashedKeepsDigitsWithPrecedingWord() {

            Check.Equal("item2-count", "Item2Count".ToDashed());
        }

        // ToDashed leaves a single lowercase word unchanged
        public static void ToDashedHandlesSingleWord() {

            Check.Equal("word", "word".ToDashed());
        }

        // ToScreamingSnakeCase converts a plain sentence
        public static void ToScreamingSnakeCaseConvertsPlainSentence() {

            Check.Equal("NEJAKA_VETA_KTEROU_JSEM_NAPSAL", "Nejaka veta, kterou jsem napsal.".ToScreamingSnakeCase());
        }

        // ToScreamingSnakeCase converts decorated text
        public static void ToScreamingSnakeCaseConvertsDecoratedText() {

            Check.Equal("CI_TROCHU_ODEKOROVANY_TEXT", "Ci trochu 'odekorovany' text".ToScreamingSnakeCase());
        }

        // ToScreamingSnakeCase converts a PascalCase identifier
        public static void ToScreamingSnakeCaseConvertsPascalCaseText() {

            Check.Equal("ME_PREVED_NA_SNAKE_CASE", "MePrevedNaSnakeCase".ToScreamingSnakeCase());
        }

        // ToScreamingSnakeCase converts a dashed parameter name
        public static void ToScreamingSnakeCaseConvertsDashedText() {

            Check.Equal("JA_JSEM_PARAMETR", "ja-jsem-parametr".ToScreamingSnakeCase());
        }

        // ToScreamingSnakeCase converts a snake_case header
        public static void ToScreamingSnakeCaseConvertsSnakeCaseText() {

            Check.Equal("TOTO_JE_HLAVICKA", "toto_je_hlavicka".ToScreamingSnakeCase());
        }

        // ToScreamingSnakeCase returns empty string for empty input
        public static void ToScreamingSnakeCaseReturnsEmptyForEmptyString() {

            Check.Equal(string.Empty, string.Empty.ToScreamingSnakeCase());
        }

        // ToScreamingSnakeCase returns null for null input
        public static void ToScreamingSnakeCaseReturnsNullForNull() {

            string value = null;

            Check.Equal(null, value.ToScreamingSnakeCase());
        }

        // ToScreamingSnakeCase keeps an acronym run as its own word
        public static void ToScreamingSnakeCaseSplitsAcronymRun() {

            Check.Equal("HTTP_SERVER", "HTTPServer".ToScreamingSnakeCase());
        }

        // ToScreamingSnakeCase keeps digits attached to the preceding word
        public static void ToScreamingSnakeCaseKeepsDigitsWithPrecedingWord() {

            Check.Equal("ITEM2_COUNT", "Item2Count".ToScreamingSnakeCase());
        }

        // ToScreamingSnakeCase uppercases a single lowercase word
        public static void ToScreamingSnakeCaseHandlesSingleWord() {

            Check.Equal("WORD", "word".ToScreamingSnakeCase());
        }

        // ToModuleCase converts a plain sentence
        public static void ToModuleCaseConvertsPlainSentence() {

            Check.Equal("NEJAKA_VetaKterouJsemNapsal", "Nejaka veta, kterou jsem napsal.".ToModuleCase());
        }

        // ToModuleCase converts decorated text
        public static void ToModuleCaseConvertsDecoratedText() {

            Check.Equal("CI_TrochuOdekorovanyText", "Ci trochu 'odekorovany' text".ToModuleCase());
        }

        // ToModuleCase converts a PascalCase identifier
        public static void ToModuleCaseConvertsPascalCaseText() {

            Check.Equal("ME_PrevedNaSnakeCase", "MePrevedNaSnakeCase".ToModuleCase());
        }

        // ToModuleCase converts a dashed parameter name
        public static void ToModuleCaseConvertsDashedText() {

            Check.Equal("JA_JsemParametr", "ja-jsem-parametr".ToModuleCase());
        }

        // ToModuleCase converts a snake_case header
        public static void ToModuleCaseConvertsSnakeCaseText() {

            Check.Equal("TOTO_JeHlavicka", "toto_je_hlavicka".ToModuleCase());
        }

        // ToModuleCase uppercases a single-word input without adding a separator
        public static void ToModuleCaseHandlesSingleWord() {

            Check.Equal("WORD", "word".ToModuleCase());
        }

        // ToModuleCase returns empty string for empty input
        public static void ToModuleCaseReturnsEmptyForEmptyString() {

            Check.Equal(string.Empty, string.Empty.ToModuleCase());
        }

        // ToModuleCase returns null for null input
        public static void ToModuleCaseReturnsNullForNull() {

            string value = null;

            Check.Equal(null, value.ToModuleCase());
        }

        // ToModuleCase keeps an acronym run as its own word
        public static void ToModuleCaseSplitsAcronymRun() {

            Check.Equal("HTTP_Server", "HTTPServer".ToModuleCase());
        }

        // ToModuleCase keeps digits attached to the preceding word
        public static void ToModuleCaseKeepsDigitsWithPrecedingWord() {

            Check.Equal("ITEM2_Count", "Item2Count".ToModuleCase());
        }

        // ToCapsLock converts a plain sentence
        public static void ToCapsLockConvertsPlainSentence() {

            Check.Equal("NEJAKA VETA KTEROU JSEM NAPSAL", "Nejaka veta, kterou jsem napsal.".ToCapsLock());
        }

        // ToCapsLock converts decorated text
        public static void ToCapsLockConvertsDecoratedText() {

            Check.Equal("CI TROCHU ODEKOROVANY TEXT", "Ci trochu 'odekorovany' text".ToCapsLock());
        }

        // ToCapsLock converts a PascalCase identifier
        public static void ToCapsLockConvertsPascalCaseText() {

            Check.Equal("ME PREVED NA SNAKE CASE", "MePrevedNaSnakeCase".ToCapsLock());
        }

        // ToCapsLock converts a dashed parameter name
        public static void ToCapsLockConvertsDashedText() {

            Check.Equal("JA JSEM PARAMETR", "ja-jsem-parametr".ToCapsLock());
        }

        // ToCapsLock converts a snake_case header
        public static void ToCapsLockConvertsSnakeCaseText() {

            Check.Equal("TOTO JE HLAVICKA", "toto_je_hlavicka".ToCapsLock());
        }

        // ToCapsLock returns empty string for empty input
        public static void ToCapsLockReturnsEmptyForEmptyString() {

            Check.Equal(string.Empty, string.Empty.ToCapsLock());
        }

        // ToCapsLock returns null for null input
        public static void ToCapsLockReturnsNullForNull() {

            string value = null;

            Check.Equal(null, value.ToCapsLock());
        }

        // ToCapsLock keeps an acronym run as its own word
        public static void ToCapsLockSplitsAcronymRun() {

            Check.Equal("HTTP SERVER", "HTTPServer".ToCapsLock());
        }

        // ToCapsLock keeps digits attached to the preceding word
        public static void ToCapsLockKeepsDigitsWithPrecedingWord() {

            Check.Equal("ITEM2 COUNT", "Item2Count".ToCapsLock());
        }

        // ToCapsLock uppercases a single lowercase word
        public static void ToCapsLockHandlesSingleWord() {

            Check.Equal("WORD", "word".ToCapsLock());
        }

        // ToCapital uppercases the whole string, same as ToUpper
        public static void ToCapitalUppercasesWholeString() {

            Check.Equal("NEJAKA VETA, KTEROU JSEM NAPSAL.", "Nejaka veta, kterou jsem napsal.".ToCapital());
        }

        // ToCapital matches string.ToUpper for arbitrary text
        public static void ToCapitalMatchesToUpper() {

            string text = "Ci trochu 'odekorovany' text";

            Check.Equal(text.ToUpper(), text.ToCapital());
        }

        // ToCapital uppercases a PascalCase identifier without splitting words
        public static void ToCapitalConvertsPascalCaseText() {

            Check.Equal("MEPREVEDNASNAKECASE", "MePrevedNaSnakeCase".ToCapital());
        }

        // ToCapital uppercases a dashed parameter name, keeping the dashes
        public static void ToCapitalConvertsDashedText() {

            Check.Equal("JA-JSEM-PARAMETR", "ja-jsem-parametr".ToCapital());
        }

        // ToCapital uppercases a snake_case header, keeping the underscores
        public static void ToCapitalConvertsSnakeCaseText() {

            Check.Equal("TOTO_JE_HLAVICKA", "toto_je_hlavicka".ToCapital());
        }

        // ToCapital uppercases an acronym run without any splitting
        public static void ToCapitalHandlesAcronymText() {

            Check.Equal("HTTPSERVER", "HTTPServer".ToCapital());
        }

        // ToCapital uppercases text containing digits, leaving the digits untouched
        public static void ToCapitalHandlesDigitsText() {

            Check.Equal("ITEM2COUNT", "Item2Count".ToCapital());
        }

        // ToCapital uppercases a single lowercase word
        public static void ToCapitalHandlesSingleWord() {

            Check.Equal("WORD", "word".ToCapital());
        }

        // ToCapital returns empty string for empty input
        public static void ToCapitalReturnsEmptyForEmptyString() {

            Check.Equal(string.Empty, string.Empty.ToCapital());
        }

        // ToCapital returns null for null input
        public static void ToCapitalReturnsNullForNull() {

            string value = null;

            Check.Equal(null, value.ToCapital());
        }

    }
}
