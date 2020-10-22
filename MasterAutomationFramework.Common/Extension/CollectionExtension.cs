using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MasterAutomationFramework.Common.Extension
{
    public static class CollectionExtension
    {
        /// <summary>
        /// Determines whether the specified sequence is empty
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence</typeparam>
        /// <param name="source">The sequence</param>
        /// <returns><b>true</b> if the sequence is empty, otherwise <b>false</b></returns>
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return !source.Any();
        }

        /// <summary>
        /// Copies a portion of the array into a new array, given the start index and length
        /// </summary>
        /// <typeparam name="T">The type of the elements in the array</typeparam>
        /// <param name="arr">The original array</param>
        /// <param name="startIndex">The index of the first element to copy</param>
        /// <param name="length">The number of elements to copy</param>
        /// <returns>A new array containing only the specified elements</returns>
        public static T[] SubArray<T>(this T[] arr, int startIndex, int length)
        {
            var destArray = new T[length];
            Array.Copy(arr, startIndex, destArray, 0, length);
            return destArray;
        }

        /// <summary>
        /// Copies a portion of the array into a new array, starting at the specified index up to the end of the array
        /// </summary>
        /// <typeparam name="T">The type of the elements in the array</typeparam>
        /// <param name="arr">The original array</param>
        /// <param name="startIndex">The index of the first element to copy</param>
        /// <returns>A new array containing the last portion, starting at the specified index</returns>
        public static T[] SubArray<T>(this T[] arr, int startIndex)
        {
            return arr.SubArray(startIndex, arr.Length - startIndex);
        }

        /// <summary>
        /// Returns the single element in the specified sequence whose specified property has the specified value
        /// </summary>
        /// <typeparam name="T1">The type of the elements in the sequence</typeparam>
        /// <typeparam name="T2">The type of the property</typeparam>
        /// <param name="source">The sequence containing the element to look for</param>
        /// <param name="propertyAccessor">A lambda expression that returns the property to compare with <paramref name="expectedValue"/></param>
        /// <param name="expectedValue">The value of the property that the element should have</param>
        /// <returns>The element that its property matches the expected value</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null or <paramref name="propertyAccessor"/> is null</exception>
        /// <exception cref="InvalidOperationException">The sequence contains no matching element or it contains more than one matching element</exception>
        /// <remarks>
        /// This method is similar to <see cref="Enumerable.Single{TSource}(System.Collections.Generic.IEnumerable{TSource}, System.Func{TSource, System.Boolean})"/>
        /// but provide more detailed message if it fails.
        /// </remarks>
        public static T1 FindElement<T1, T2>(this IEnumerable<T1> source, Expression<Func<T1, T2>> propertyAccessor, T2 expectedValue)
        {
            if (propertyAccessor == null)
                throw new ArgumentNullException("propertyAccessor");

            var expressionText = string.Format("{0} == {1}", propertyAccessor, expectedValue);
            return FindInternal(source, x => propertyAccessor.Compile()(x).SafeEquals(expectedValue), expressionText);
        }

        /// <summary>
        /// Returns the single element in the specified sequence that matches the specified criteria
        /// </summary>
        /// <typeparam name="T">The type of the element in the sequence</typeparam>
        /// <param name="source">The sequence containing the element to look for</param>
        /// <param name="condition">A lambda expression of a condition that the matching element should pass</param>
        /// <returns>The element that matches the specified condition</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null or <paramref name="condition"/> is null</exception>
        /// <exception cref="InvalidOperationException">The sequence contains no matching element or it contains more than one matching element</exception>
        /// <remarks>
        /// This method is similar to <see cref="Enumerable.Single{TSource}(System.Collections.Generic.IEnumerable{TSource}, System.Func{TSource, System.Boolean})"/>
        /// but provide mor e detailed message if it fails.
        /// <br/>
        /// Because it's not possible to "override" a static method, and in order to prevent the inadvertent use of the original 
        /// <see cref="Enumerable.Single{TSource}(System.Collections.Generic.IEnumerable{TSource}, System.Func{TSource, System.Boolean})"/> method, this class hides the original
        /// method by declaring another method with the same name, and an [Obsolete] attribute.
        /// </remarks>
        public static T Find<T>(this IEnumerable<T> source, Expression<Func<T, bool>> condition)
        {
            if (condition == null)
                throw new ArgumentNullException("condition");

            var predicate = condition.Compile();
            var expressionText = condition.ToString();

            return FindInternal(source, predicate, expressionText);
        }

        private static T FindInternal<T>(IEnumerable<T> source, Func<T, bool> predicate, string expressionText)
        {
            string message;
            if (source == null)
            {
                message = string.Format("Sequence of '{0}' was expected, but was null", typeof(T).Name);
                throw new ArgumentNullException(null, message);
            }

            var enumerator = source.Where(predicate).GetEnumerator();
            if (!enumerator.MoveNext())
            {
                message = String.Format("Sequence of type '{0}' contains no element that matches the condition '{1}'",
                    typeof(T).Name, expressionText);
                throw new InvalidOperationException(message);
            }

            var first = enumerator.Current;

            if (!enumerator.MoveNext())
            {
                return first;
            }

            var second = enumerator.Current;
            message =
                string.Format(
                    "Sequence of type '{0}' contains more than element that matches the condition '{1}'. The first 2 are '{2}' and '{3}'",
                    typeof(T).Name, expressionText, first, second);
            throw new InvalidOperationException(message);
        }

        private static bool SafeEquals<T>(this T obj1, T obj2)
        {
            return obj1 == null && obj2 == null || obj1 != null && obj1.Equals(obj2);
        }
    }
}
