using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NunitTest.Extensions
{
    public static class StringExtensions
    {
        public static string GetSubstringIfStringLongerThan(string stringToCut, int maxLength)
        {
            if (stringToCut.Length > maxLength)
            {
                return stringToCut.Substring(0, maxLength);
            }

            return stringToCut;
        }
        public static bool IsEmpty(this string text)
            => string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text);

        public static bool HasValue(this string value) => !IsEmpty(value);

        public static string EmptyIfNull(this string value) => value ?? string.Empty;

        public static bool EqualsIgnoreCase(this string value, string compareValue)
            => value.EmptyIfNull().Equals(compareValue, StringComparison.InvariantCultureIgnoreCase);


        public static string SurroundWith(this string str, string surroundString)
        {
            if (!str.StartsWith(surroundString))
            {
                str = $@"{surroundString}{str}";
            }
            if (!str.EndsWith(surroundString))
            {
                str = $@"{str}{surroundString}";
            }

            return str;
        }

        public static IEnumerable<string> ApplyJsonPathExpression(this string value, string jsonFilter) => JObject.Parse(value.EmptyIfNull()).SelectTokens(jsonFilter).Select(s => Convert.ToString(s)).ToArray();
        public static IEnumerable<string> SplitAndTrim(this string value, string splitString) => value.Split(new[] { splitString }, StringSplitOptions.RemoveEmptyEntries)
                .Select(d => d.Trim())
                .Where(d => d.HasValue());
        public static IEnumerable<string> SplitByWithDistinct(this string value, string splitString) => SplitAndTrim(value, splitString)
                 .Distinct();


        public static bool Contains(this string haystack, string pin, StringComparison comparisonOptions) => haystack.IndexOf(pin, comparisonOptions) >= 0;
        public static bool ContainsIgnoreCase(this string data, string value) => data.Contains(value, StringComparison.InvariantCultureIgnoreCase);
        public static bool ContainsIgnoreCase(this string data, string[] value) => value.Any(str => data.ContainsIgnoreCase(str));

        public static string ConvertToDbFormatColumnName(this string value) => "_" + Regex.Replace(value, @"[^\w]+", "").ToLower();

        public static string StartWithCompareThenTrim(this string value, string[] checks)
            => checks.Any(value.StartsWith)
                ? checks.Where(value.StartsWith)
                    .Select(l => value.Substring(l.Length, value.Length - l.Length)).FirstOrDefault()
                : value;

        public static string EndWithCompareThenTrim(this string value, string[] checks)
            => checks.Any(value.EndsWith)
                ? checks.Where(value.EndsWith)
                    .Select(l => value.Substring(0, value.Length - l.Length)).FirstOrDefault()
                : value;


        public static string StripQuotes(this string value)
            => value
            .StartWithCompareThenTrim(new[] { "\"" })
            .EndWithCompareThenTrim(new[] { "\"" });

        public static string ExtractNumber(this string original)
            => new string(original.Where(c => Char.IsNumber(c)).ToArray());

        public static string ConvertToValidFileName(this string name, int length = 0)
        {
            StringBuilder result = new StringBuilder();
            foreach (var str in name)
            {
                if (Char.IsLetterOrDigit(str))
                    result.Append(str);
            }

            if (length > 0 && result.Length > length)
            {
                return result.ToString().Substring(0, length);
            }
            else
            {
                return result.ToString();
            }
        }
    }
}
