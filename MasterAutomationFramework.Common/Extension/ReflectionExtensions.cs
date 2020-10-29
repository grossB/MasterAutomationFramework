using MasterAutomationFramework.Common.ConfigLoader;
using System;
using System.Reflection;

namespace MasterAutomationFramework.Common.Extension
{
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Determines whether the specified member has the specified attribute
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute to search for</typeparam>
        /// <param name="member">The member to inspect</param>
        /// <returns><b>true</b> if the member has the specified attribute, otherwise <b>false</b></returns>
        public static bool HasAttribute<TAttribute>(this MemberInfo member)
            where TAttribute : Attribute
        {
            return member.GetCustomAttribute<TAttribute>(false) != null;
        }

        /// <summary>
        /// Returns the default value of the specified type
        /// </summary>
        /// <param name="t">The type to get its default value</param>
        /// <returns>The default value of the specified type</returns>
        /// <remarks>
        /// This method is similiar to the expression: <code>default(T)</code> that is useful mainly in Generic methods.
        /// However, with <code>default(T)</code> the type must be known at compile time (or be a generic type), while with this '
        /// method, the type can be specified at run-time.
        /// </remarks>
        public static object GetDefaultValue(this Type t)
        {
            return t.IsValueType ? Activator.CreateInstance(t) : null;
        }

        // Example of Activator.CreateInstance mechanism 
        // var someObject = TypeObjectInitialize<SimpleConfig>(typeof(SimpleConfig));
        public static T TypeObjectInitialize<T>(Type testClass)
        {
            var instance = (T)Activator.CreateInstance(testClass);
            return instance;
        }
    }
}
